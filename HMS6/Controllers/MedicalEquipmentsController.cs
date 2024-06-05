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
    public class MedicalEquipmentsController : Controller
    {
        private readonly HMSContext _context;

        public MedicalEquipmentsController(HMSContext context)
        {
            _context = context;
        }

        // GET: MedicalEquipments
        public async Task<IActionResult> Index()
        {
            var hMSContext = _context.MedicalEquipments.Include(m => m.Dep);
            return View(await hMSContext.ToListAsync());
        }

        // GET: MedicalEquipments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MedicalEquipments == null)
            {
                return NotFound();
            }

            var medicalEquipment = await _context.MedicalEquipments
                .Include(m => m.Dep)
                .FirstOrDefaultAsync(m => m.EquipmentId == id);
            if (medicalEquipment == null)
            {
                return NotFound();
            }

            return View(medicalEquipment);
        }

        // GET: MedicalEquipments/Create
        public IActionResult Create()
        {
            ViewData["DepId"] = new SelectList(_context.Departemments, "DepartementId", "DepartementId");
            return View();
        }

        // POST: MedicalEquipments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EquipmentName,EquipmentId,DepId")] MedicalEquipment medicalEquipment)
        {
            medicalEquipment.EquipmentId = _context.MedicalEquipments.Max(m => m.EquipmentId) + 1;
            if (ModelState.IsValid)
            {
                _context.Add(medicalEquipment);
                await _context.SaveChangesAsync();
                TempData["successData"] = "Medical Equipment has been added successfully";
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepId"] = new SelectList(_context.Departemments, "DepartementId", "DepartementId", medicalEquipment.DepId);
            return View(medicalEquipment);
        }

        // GET: MedicalEquipments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MedicalEquipments == null)
            {
                return NotFound();
            }

            var medicalEquipment = await _context.MedicalEquipments.FindAsync(id);
            if (medicalEquipment == null)
            {
                return NotFound();
            }
            ViewData["DepId"] = new SelectList(_context.Departemments, "DepartementId", "DepartementId", medicalEquipment.DepId);
            return View(medicalEquipment);
        }

        // POST: MedicalEquipments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EquipmentName,EquipmentId,DepId")] MedicalEquipment medicalEquipment)
        {
            if (id != medicalEquipment.EquipmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medicalEquipment);
                    TempData["successData"] = "Medical Equipment has been updated successfully";
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicalEquipmentExists(medicalEquipment.EquipmentId))
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
            ViewData["DepId"] = new SelectList(_context.Departemments, "DepartementId", "DepartementId", medicalEquipment.DepId);
            return View(medicalEquipment);
        }

        // GET: MedicalEquipments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MedicalEquipments == null)
            {
                return NotFound();
            }

            var medicalEquipment = await _context.MedicalEquipments
                .Include(m => m.Dep)
                .FirstOrDefaultAsync(m => m.EquipmentId == id);
            if (medicalEquipment == null)
            {
                return NotFound();
            }

            return View(medicalEquipment);
        }

        // POST: MedicalEquipments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MedicalEquipments == null)
            {
                return Problem("Entity set 'HMSContext.MedicalEquipments'  is null.");
            }
            var medicalEquipment = await _context.MedicalEquipments.FindAsync(id);
            if (medicalEquipment != null)
            {
                _context.MedicalEquipments.Remove(medicalEquipment);
            }
            
            await _context.SaveChangesAsync();
            TempData["successData"] = "Medical Equipment has been deleted successfully";
            return RedirectToAction(nameof(Index));
        }

        private bool MedicalEquipmentExists(int id)
        {
          return (_context.MedicalEquipments?.Any(e => e.EquipmentId == id)).GetValueOrDefault();
        }
    }
}
