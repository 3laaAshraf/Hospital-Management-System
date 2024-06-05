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
    public class MedicalHistoriesController : Controller
    {
        private readonly HMSContext _context;

        public MedicalHistoriesController(HMSContext context)
        {
            _context = context;
        }

        // GET: MedicalHistories
        public async Task<IActionResult> Index()
        {
            var hMSContext = _context.MedicalHistories.Include(m => m.Doctor).Include(m => m.Patient);
            return View(await hMSContext.ToListAsync());
        }

        // GET: MedicalHistories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MedicalHistories == null)
            {
                return NotFound();
            }

            var medicalHistory = await _context.MedicalHistories
                .Include(m => m.Doctor)
                .Include(m => m.Patient)
                .FirstOrDefaultAsync(m => m.MedicalHistoryId == id);
            if (medicalHistory == null)
            {
                return NotFound();
            }

            return View(medicalHistory);
        }

        // GET: MedicalHistories/Create
        public IActionResult Create()
        {
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "DoctorId");
            ViewData["PatientId"] = new SelectList(_context.Patients, "PatientId", "PatientId");
            return View();
        }

        // POST: MedicalHistories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MedicalHistoryId,PatientId,DoctorId,DateOfVisitPatToDoc,Diagnosis,FollowUpDate")] MedicalHistory medicalHistory)
        {
            medicalHistory.MedicalHistoryId = _context.MedicalHistories.Max(m => m.MedicalHistoryId) + 1;
            if (ModelState.IsValid)
            {
                _context.Add(medicalHistory);
                await _context.SaveChangesAsync();
                TempData["successData"] = "Medical History has been added successfully";
                return RedirectToAction(nameof(Index));
            }
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "DoctorId", medicalHistory.DoctorId);
            ViewData["PatientId"] = new SelectList(_context.Patients, "PatientId", "PatientId", medicalHistory.PatientId);
            return View(medicalHistory);
        }

        // GET: MedicalHistories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MedicalHistories == null)
            {
                return NotFound();
            }

            var medicalHistory = await _context.MedicalHistories.FindAsync(id);
            if (medicalHistory == null)
            {
                return NotFound();
            }
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "DoctorId", medicalHistory.DoctorId);
            ViewData["PatientId"] = new SelectList(_context.Patients, "PatientId", "PatientId", medicalHistory.PatientId);
            return View(medicalHistory);
        }

        // POST: MedicalHistories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MedicalHistoryId,PatientId,DoctorId,DateOfVisitPatToDoc,Diagnosis,FollowUpDate")] MedicalHistory medicalHistory)
        {
            if (id != medicalHistory.MedicalHistoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medicalHistory);
                    TempData["successData"] = "Medical History has been updated successfully";
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicalHistoryExists(medicalHistory.MedicalHistoryId))
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
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "DoctorId", medicalHistory.DoctorId);
            ViewData["PatientId"] = new SelectList(_context.Patients, "PatientId", "PatientId", medicalHistory.PatientId);
            return View(medicalHistory);
        }

        // GET: MedicalHistories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MedicalHistories == null)
            {
                return NotFound();
            }

            var medicalHistory = await _context.MedicalHistories
                .Include(m => m.Doctor)
                .Include(m => m.Patient)
                .FirstOrDefaultAsync(m => m.MedicalHistoryId == id);
            if (medicalHistory == null)
            {
                return NotFound();
            }

            return View(medicalHistory);
        }

        // POST: MedicalHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MedicalHistories == null)
            {
                return Problem("Entity set 'HMSContext.MedicalHistories'  is null.");
            }
            var medicalHistory = await _context.MedicalHistories.FindAsync(id);
            if (medicalHistory != null)
            {
                _context.MedicalHistories.Remove(medicalHistory);
            }
            
            await _context.SaveChangesAsync();
            TempData["successData"] = "Medical History has been deleted successfully";
            return RedirectToAction(nameof(Index));
        }

        private bool MedicalHistoryExists(int id)
        {
          return (_context.MedicalHistories?.Any(e => e.MedicalHistoryId == id)).GetValueOrDefault();
        }
    }
}
