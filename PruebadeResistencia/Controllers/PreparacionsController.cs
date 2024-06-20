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
    public class PreparacionsController : Controller
    {
        private readonly BreakageTestContext _context;

        public PreparacionsController(BreakageTestContext context)
        {
            _context = context;
        }

        // GET: Preparacions
        public async Task<IActionResult> Index()
        {
            var breakageTestContext = _context.Preparacions.Include(p => p.Cemento).Include(p => p.Molino);
            return View(await breakageTestContext.ToListAsync());
        }

        // GET: Preparacions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var preparacion = await _context.Preparacions
                .Include(p => p.Cemento)
                .Include(p => p.Molino)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (preparacion == null)
            {
                return NotFound();
            }

            return View(preparacion);
        }

        // GET: Preparacions/Create
        public IActionResult Create()
        {
            ViewData["CementoId"] = new SelectList(_context.Cementos, "Id", "Id");
            ViewData["MolinoId"] = new SelectList(_context.Molinos, "Id", "Id");
            return View();
        }

        // POST: Preparacions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FechaDePreparación,MolinoId,CementoId")] Preparacion preparacion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(preparacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CementoId"] = new SelectList(_context.Cementos, "Id", "Id", preparacion.CementoId);
            ViewData["MolinoId"] = new SelectList(_context.Molinos, "Id", "Id", preparacion.MolinoId);
            return View(preparacion);
        }

        // GET: Preparacions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var preparacion = await _context.Preparacions.FindAsync(id);
            if (preparacion == null)
            {
                return NotFound();
            }
            ViewData["CementoId"] = new SelectList(_context.Cementos, "Id", "Id", preparacion.CementoId);
            ViewData["MolinoId"] = new SelectList(_context.Molinos, "Id", "Id", preparacion.MolinoId);
            return View(preparacion);
        }

        // POST: Preparacions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FechaDePreparación,MolinoId,CementoId")] Preparacion preparacion)
        {
            if (id != preparacion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(preparacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PreparacionExists(preparacion.Id))
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
            ViewData["CementoId"] = new SelectList(_context.Cementos, "Id", "Id", preparacion.CementoId);
            ViewData["MolinoId"] = new SelectList(_context.Molinos, "Id", "Id", preparacion.MolinoId);
            return View(preparacion);
        }

        // GET: Preparacions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var preparacion = await _context.Preparacions
                .Include(p => p.Cemento)
                .Include(p => p.Molino)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (preparacion == null)
            {
                return NotFound();
            }

            return View(preparacion);
        }

        // POST: Preparacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var preparacion = await _context.Preparacions.FindAsync(id);
            if (preparacion != null)
            {
                _context.Preparacions.Remove(preparacion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PreparacionExists(int id)
        {
            return _context.Preparacions.Any(e => e.Id == id);
        }
    }
}
