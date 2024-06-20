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
    public class CementoesController : Controller
    {
        private readonly BreakageTestContext _context;

        public CementoesController(BreakageTestContext context)
        {
            _context = context;
        }

        // GET: Cementoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cementos.ToListAsync());
        }

        // GET: Cementoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cemento = await _context.Cementos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cemento == null)
            {
                return NotFound();
            }

            return View(cemento);
        }

        // GET: Cementoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cementoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Código,Nombre")] Cemento cemento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cemento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cemento);
        }

        // GET: Cementoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cemento = await _context.Cementos.FindAsync(id);
            if (cemento == null)
            {
                return NotFound();
            }
            return View(cemento);
        }

        // POST: Cementoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Código,Nombre")] Cemento cemento)
        {
            if (id != cemento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cemento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CementoExists(cemento.Id))
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
            return View(cemento);
        }

        // GET: Cementoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cemento = await _context.Cementos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cemento == null)
            {
                return NotFound();
            }

            return View(cemento);
        }

        // POST: Cementoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cemento = await _context.Cementos.FindAsync(id);
            if (cemento != null)
            {
                _context.Cementos.Remove(cemento);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CementoExists(int id)
        {
            return _context.Cementos.Any(e => e.Id == id);
        }
    }
}
