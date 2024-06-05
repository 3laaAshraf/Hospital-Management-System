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
    public class DepartemmentsController : Controller
    {
        private readonly HMSContext _context;

        public DepartemmentsController(HMSContext context)
        {
            _context = context;
        }

        // GET: Departemments
        public async Task<IActionResult> Index()
        {
              return _context.Departemments != null ? 
                          View(await _context.Departemments.ToListAsync()) :
                          Problem("Entity set 'HMSContext.Departemments'  is null.");
        }

        // GET: Departemments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Departemments == null)
            {
                return NotFound();
            }

            var departemment = await _context.Departemments
                .FirstOrDefaultAsync(m => m.DepartementId == id);
            if (departemment == null)
            {
                return NotFound();
            }
            List<Doctor> doctors= _context.Doctors.Where(d=>d.DoctorId == id).ToList(); 
            List<Patient>patients=_context.Patients.Where(p=>p.PatientId==id).ToList();
            List<Nurse> nurse = _context.Nurses.Where(n => n.NurseId == id).ToList();
            List<MedicalEquipment> MedicalEquipments = _context.MedicalEquipments.Where(e => e.DepId == id).ToList();

            return View(departemment);
        }

        // GET: Departemments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Departemments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DepartementName,DepartementId")] Departemment departemment)
        {
            departemment.DepartementId = _context.Departemments.Max(d => d.DepartementId) + 1;
            if (ModelState.IsValid)
            {
                _context.Add(departemment);
                await _context.SaveChangesAsync();
                TempData["successData"] = "Departement has been added successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(departemment);
        }

        // GET: Departemments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Departemments == null)
            {
                return NotFound();
            }

            var departemment = await _context.Departemments.FindAsync(id);
            if (departemment == null)
            {
                return NotFound();
            }
            return View(departemment);
        }

        // POST: Departemments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DepartementName,DepartementId")] Departemment departemment)
        {
            if (id != departemment.DepartementId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(departemment);
                    TempData["successData"] = "Departement has been updated successfully";
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartemmentExists(departemment.DepartementId))
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
            return View(departemment);
        }

        // GET: Departemments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Departemments == null)
            {
                return NotFound();
            }

            var departemment = await _context.Departemments
                .FirstOrDefaultAsync(m => m.DepartementId == id);
            if (departemment == null)
            {
                return NotFound();
            }

            return View(departemment);
        }

        // POST: Departemments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Departemments == null)
            {
                return Problem("Entity set 'HMSContext.Departemments'  is null.");
            }
            var departemment = await _context.Departemments.FindAsync(id);
            if (departemment != null)
            {
                _context.Departemments.Remove(departemment);
            }
            
            await _context.SaveChangesAsync();
            TempData["successData"] = "Departement has been deleted successfully";
            return RedirectToAction(nameof(Index));
        }

        private bool DepartemmentExists(int id)
        {
          return (_context.Departemments?.Any(e => e.DepartementId == id)).GetValueOrDefault();
        }
    }
}
