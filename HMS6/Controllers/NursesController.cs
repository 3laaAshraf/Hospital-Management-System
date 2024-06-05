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
    public class NursesController : Controller
    {
        private readonly HMSContext _context;

        public NursesController(HMSContext context)
        {
            _context = context;
        }

        // GET: Nurses
        public async Task<IActionResult> Index()
        {
            var hMSContext = _context.Nurses.Include(n => n.Dep);
            return View(await hMSContext.ToListAsync());
        }

        // GET: Nurses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Nurses == null)
            {
                return NotFound();
            }

            var nurse = await _context.Nurses
                .Include(n => n.Dep)
                .FirstOrDefaultAsync(m => m.NurseId == id);
            if (nurse == null)
            {
                return NotFound();
            }

            return View(nurse);
        }

        // GET: Nurses/Create
        public IActionResult Create()
        {
            ViewData["DepId"] = new SelectList(_context.Departemments, "DepartementId", "DepartementId");
            return View();
        }

        // POST: Nurses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,NurseId,DepId")] Nurse nurse)
        {
            nurse.NurseId = _context.Nurses.Max(n => n.NurseId) + 1;
            if (ModelState.IsValid)
            {
                _context.Add(nurse);
                await _context.SaveChangesAsync();
                TempData["successData"] = "nurse has been added successfully";
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepId"] = new SelectList(_context.Departemments, "DepartementId", "DepartementId", nurse.DepId);
            return View(nurse);
        }

        // GET: Nurses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Nurses == null)
            {
                return NotFound();
            }

            var nurse = await _context.Nurses.FindAsync(id);
            if (nurse == null)
            {
                return NotFound();
            }
            ViewData["DepId"] = new SelectList(_context.Departemments, "DepartementId", "DepartementId", nurse.DepId);
            return View(nurse);
        }

        // POST: Nurses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,NurseId,DepId")] Nurse nurse)
        {
            if (id != nurse.NurseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nurse);
                    TempData["successData"] = "nurse has been updated successfully";
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NurseExists(nurse.NurseId))
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
            ViewData["DepId"] = new SelectList(_context.Departemments, "DepartementId", "DepartementId", nurse.DepId);
            return View(nurse);
        }

        // GET: Nurses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Nurses == null)
            {
                return NotFound();
            }

            var nurse = await _context.Nurses
                .Include(n => n.Dep)
                .FirstOrDefaultAsync(m => m.NurseId == id);
            if (nurse == null)
            {
                return NotFound();
            }

            return View(nurse);
        }

        // POST: Nurses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Nurses == null)
            {
                return Problem("Entity set 'HMSContext.Nurses'  is null.");
            }
            var nurse = await _context.Nurses.FindAsync(id);
            if (nurse != null)
            {
                _context.Nurses.Remove(nurse);
            }
            
            await _context.SaveChangesAsync();
            TempData["successData"] = "nurse has been deleted successfully";
            return RedirectToAction(nameof(Index));
        }

        private bool NurseExists(int id)
        {
          return (_context.Nurses?.Any(e => e.NurseId == id)).GetValueOrDefault();
        }
    }
}
