using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SlovenskiKino.Models;

namespace SlovenskiKino.Controllers
{
    public class KinoController : Controller
    {
        private readonly KinoContext _context;

        public KinoController(KinoContext context)
        {
            _context = context;
        }

        // GET: Kino
        public async Task<IActionResult> Index()
        {
            var kinoContext = _context.Kinematografi.Include(k => k.IdPodjetjaNavigation);
            return View(await kinoContext.ToListAsync());
        }

        // GET: Kino/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kinematografi = await _context.Kinematografi
                .Include(k => k.IdPodjetjaNavigation)
                .FirstOrDefaultAsync(m => m.IdKinematograf == id);
            if (kinematografi == null)
            {
                return NotFound();
            }

            return View(kinematografi);
        }

        // GET: Kino/Create
        public IActionResult Create()
        {
            ViewData["IdPodjetja"] = new SelectList(_context.Podjetja, "IdPodjetja", "IdPodjetja");
            return View();
        }

        // POST: Kino/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdKinematograf,IdPodjetja,Kinematograf")] Kinematografi kinematografi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kinematografi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPodjetja"] = new SelectList(_context.Podjetja, "IdPodjetja", "IdPodjetja", kinematografi.IdPodjetja);
            return View(kinematografi);
        }

        // GET: Kino/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kinematografi = await _context.Kinematografi.FindAsync(id);
            if (kinematografi == null)
            {
                return NotFound();
            }
            ViewData["IdPodjetja"] = new SelectList(_context.Podjetja, "IdPodjetja", "IdPodjetja", kinematografi.IdPodjetja);
            return View(kinematografi);
        }

        // POST: Kino/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdKinematograf,IdPodjetja,Kinematograf")] Kinematografi kinematografi)
        {
            if (id != kinematografi.IdKinematograf)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kinematografi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KinematografiExists(kinematografi.IdKinematograf))
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
            ViewData["IdPodjetja"] = new SelectList(_context.Podjetja, "IdPodjetja", "IdPodjetja", kinematografi.IdPodjetja);
            return View(kinematografi);
        }

        // GET: Kino/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kinematografi = await _context.Kinematografi
                .Include(k => k.IdPodjetjaNavigation)
                .FirstOrDefaultAsync(m => m.IdKinematograf == id);
            if (kinematografi == null)
            {
                return NotFound();
            }

            return View(kinematografi);
        }

        // POST: Kino/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kinematografi = await _context.Kinematografi.FindAsync(id);
            _context.Kinematografi.Remove(kinematografi);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KinematografiExists(int id)
        {
            return _context.Kinematografi.Any(e => e.IdKinematograf == id);
        }
    }
}
