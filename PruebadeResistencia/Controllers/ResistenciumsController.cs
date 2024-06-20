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
    public class ResistenciumsController : Controller
    {
        private readonly BreakageTestContext _context;

        public ResistenciumsController(BreakageTestContext context)
        {
            _context = context;
        }

        // GET: Resistenciums
        public async Task<IActionResult> Index()
        {
            var breakageTestContext = _context.Resistencia.Include(r => r.Dia).Include(r => r.Preparacion);
            return View(await breakageTestContext.ToListAsync());
        }

        // GET: Resistenciums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resistencium = await _context.Resistencia
                .Include(r => r.Dia)
                .Include(r => r.Preparacion)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (resistencium == null)
            {
                return NotFound();
            }

            return View(resistencium);
        }

        // GET: Resistenciums/Create
        public IActionResult Create()
        {
            ViewData["DiaId"] = new SelectList(_context.Días, "Id", "Id");
            ViewData["PreparacionId"] = new SelectList(_context.Preparacions, "Id", "Id");
            return View();
        }

        // POST: Resistenciums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Cubo1,Cubo2,Cubo3,Prom,PreparacionId,DiaId")] Resistencium resistencium)
        {
            if (ModelState.IsValid)
            {
                _context.Add(resistencium);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DiaId"] = new SelectList(_context.Días, "Id", "Id", resistencium.DiaId);
            ViewData["PreparacionId"] = new SelectList(_context.Preparacions, "Id", "Id", resistencium.PreparacionId);
            return View(resistencium);
        }

        // GET: Resistenciums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resistencium = await _context.Resistencia.FindAsync(id);
            if (resistencium == null)
            {
                return NotFound();
            }
            ViewData["DiaId"] = new SelectList(_context.Días, "Id", "Id", resistencium.DiaId);
            ViewData["PreparacionId"] = new SelectList(_context.Preparacions, "Id", "Id", resistencium.PreparacionId);
            return View(resistencium);
        }

        // POST: Resistenciums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Cubo1,Cubo2,Cubo3,Prom,PreparacionId,DiaId")] Resistencium resistencium)
        {
            if (id != resistencium.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(resistencium);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResistenciumExists(resistencium.Id))
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
            ViewData["DiaId"] = new SelectList(_context.Días, "Id", "Id", resistencium.DiaId);
            ViewData["PreparacionId"] = new SelectList(_context.Preparacions, "Id", "Id", resistencium.PreparacionId);
            return View(resistencium);
        }

        // GET: Resistenciums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resistencium = await _context.Resistencia
                .Include(r => r.Dia)
                .Include(r => r.Preparacion)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (resistencium == null)
            {
                return NotFound();
            }

            return View(resistencium);
        }

        // POST: Resistenciums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var resistencium = await _context.Resistencia.FindAsync(id);
            if (resistencium != null)
            {
                _context.Resistencia.Remove(resistencium);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResistenciumExists(int id)
        {
            return _context.Resistencia.Any(e => e.Id == id);
        }
    }
}
