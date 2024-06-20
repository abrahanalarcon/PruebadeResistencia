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
    public class MolinoesController : Controller
    {
        private readonly BreakageTestContext _context;

        public MolinoesController(BreakageTestContext context)
        {
            _context = context;
        }

        // GET: Molinoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Molinos.ToListAsync());
        }

        // GET: Molinoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var molino = await _context.Molinos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (molino == null)
            {
                return NotFound();
            }

            return View(molino);
        }

        // GET: Molinoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Molinoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Código,Nombre")] Molino molino)
        {
            if (ModelState.IsValid)
            {
                _context.Add(molino);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(molino);
        }

        // GET: Molinoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var molino = await _context.Molinos.FindAsync(id);
            if (molino == null)
            {
                return NotFound();
            }
            return View(molino);
        }

        // POST: Molinoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Código,Nombre")] Molino molino)
        {
            if (id != molino.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(molino);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MolinoExists(molino.Id))
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
            return View(molino);
        }

        // GET: Molinoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var molino = await _context.Molinos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (molino == null)
            {
                return NotFound();
            }

            return View(molino);
        }

        // POST: Molinoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var molino = await _context.Molinos.FindAsync(id);
            if (molino != null)
            {
                _context.Molinos.Remove(molino);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MolinoExists(int id)
        {
            return _context.Molinos.Any(e => e.Id == id);
        }
    }
}
