using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HMS6.Models;

namespace HMS6.Controllers
{
    public class PatientsController : Controller
    {
        private readonly HMSContext _context;

        public PatientsController(HMSContext context)
        {
            _context = context;
        }

        // GET: Patients
        public async Task<IActionResult> Index()
        {
            var hMSContext = _context.Patients.Include(p => p.Dep).Include(p => p.Doc).Include(p => p.Room);
            return View(await hMSContext.ToListAsync());
        }

        // GET: Patients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Patients == null)
            {
                return NotFound();
            }

            var patient = await _context.Patients
                .Include(p => p.Dep)
                .Include(p => p.Doc)
                .Include(p => p.Room)
                .FirstOrDefaultAsync(m => m.PatientId == id);
            if (patient == null)
            {
                return NotFound();
            }
            List<Appointment> Appointments = _context.Appointments.Where(a => a.PatId==id).ToList();
            List<MedicalHistory>MedicalHistories=_context.MedicalHistories.Where(m=>m.PatientId==id).ToList();
            return View(patient);
        }

        // GET: Patients/Create
        public IActionResult Create()
        {
            ViewData["DepId"] = new SelectList(_context.Departemments, "DepartementId", "DepartementId");
            ViewData["DocId"] = new SelectList(_context.Doctors, "DoctorId", "DoctorId");
            ViewData["RoomId"] = new SelectList(_context.Rooms, "RoomId", "RoomId");
            return View();
        }

        // POST: Patients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PatientName,PatientId,Gender,DateOfBirth,ContactNumber,BloodType,Address,PatientCase,DepId,DocId,TimeIn,TimeOut,RoomId")] Patient patient)
        {
            patient.PatientId = _context.Patients.Max(p => p.PatientId) + 1;
            if (ModelState.IsValid)
            {
                _context.Add(patient);
                await _context.SaveChangesAsync();
                TempData["successData"] = "patient has been added successfully";
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepId"] = new SelectList(_context.Departemments, "DepartementId", "DepartementId", patient.DepId);
            ViewData["DocId"] = new SelectList(_context.Doctors, "DoctorId", "DoctorId", patient.DocId);
            ViewData["RoomId"] = new SelectList(_context.Rooms, "RoomId", "RoomId", patient.RoomId);
            return View(patient);
        }

        // GET: Patients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Patients == null)
            {
                return NotFound();
            }

            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }
            ViewData["DepId"] = new SelectList(_context.Departemments, "DepartementId", "DepartementId", patient.DepId);
            ViewData["DocId"] = new SelectList(_context.Doctors, "DoctorId", "DoctorId", patient.DocId);
            ViewData["RoomId"] = new SelectList(_context.Rooms, "RoomId", "RoomId", patient.RoomId);
            return View(patient);
        }

        // POST: Patients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PatientName,PatientId,Gender,DateOfBirth,ContactNumber,BloodType,Address,PatientCase,DepId,DocId,TimeIn,TimeOut,RoomId")] Patient patient)
        {
            if (id != patient.PatientId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patient);
                    TempData["successData"] = "patient has been updated successfully";
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientExists(patient.PatientId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepId"] = new SelectList(_context.Departemments, "DepartementId", "DepartementId", patient.DepId);
            ViewData["DocId"] = new SelectList(_context.Doctors, "DoctorId", "DoctorId", patient.DocId);
            ViewData["RoomId"] = new SelectList(_context.Rooms, "RoomId", "RoomId", patient.RoomId);
            return View(patient);
        }

        // GET: Patients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Patients == null)
            {
                return NotFound();
            }

            var patient = await _context.Patients
                .Include(p => p.Dep)
                .Include(p => p.Doc)
                .Include(p => p.Room)
                .FirstOrDefaultAsync(m => m.PatientId == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Patients == null)
            {
                return Problem("Entity set 'HMSContext.Patients'  is null.");
            }
            var patient = await _context.Patients.FindAsync(id);
            if (patient != null)
            {
                _context.Patients.Remove(patient);
            }
            
            await _context.SaveChangesAsync();
            TempData["successData"] = "patient has been deleted successfully";
            return RedirectToAction(nameof(Index));
        }

        private bool PatientExists(int id)
        {
          return (_context.Patients?.Any(e => e.PatientId == id)).GetValueOrDefault();
        }
    }
}
