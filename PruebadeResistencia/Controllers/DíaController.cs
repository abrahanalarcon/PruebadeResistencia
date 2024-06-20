using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PruebadeResistencia.Models;

namespace PruebadeResistencia.Controllers
{
    public class DíaController : Controller
    {
        private readonly BreakageTestContext _context;

        public DíaController(BreakageTestContext context)
        {
            _context = context;
        }

        // GET: Día
        public async Task<IActionResult> Index()
        {
            return View(await _context.Días.ToListAsync());
        }

        // GET: Día/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var día = await _context.Días
                .FirstOrDefaultAsync(m => m.Id == id);
            if (día == null)
            {
                return NotFound();
            }

            return View(día);
        }

        // GET: Día/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Día/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TipoDeDia")] Día día)
        {
            if (ModelState.IsValid)
            {
                _context.Add(día);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(día);
        }

        // GET: Día/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var día = await _context.Días.FindAsync(id);
            if (día == null)
            {
                return NotFound();
            }
            return View(día);
        }

        // POST: Día/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TipoDeDia")] Día día)
        {
            if (id != día.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(día);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DíaExists(día.Id))
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
            return View(día);
        }

        // GET: Día/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var día = await _context.Días
                .FirstOrDefaultAsync(m => m.Id == id);
            if (día == null)
            {
                return NotFound();
            }

            return View(día);
        }

        // POST: Día/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var día = await _context.Días.FindAsync(id);
            if (día != null)
            {
                _context.Días.Remove(día);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DíaExists(int id)
        {
            return _context.Días.Any(e => e.Id == id);
        }
    }
}
