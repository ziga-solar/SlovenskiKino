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
    public class AktualnoController : Controller
    {
        private readonly KinoContext _context;

        public AktualnoController(KinoContext context)
        {
            _context = context;
        }

        // GET: Aktualno
        public async Task<IActionResult> Index()
        {
            var kinoContext = _context.AktualniFilmi.Include(a => a.IdPodjetjaNavigation);
            return View(await kinoContext.ToListAsync());
        }

        // GET: Aktualno/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aktualniFilmi = await _context.AktualniFilmi
                .Include(a => a.IdPodjetjaNavigation)
                .FirstOrDefaultAsync(m => m.IdAktualenFilm == id);
            if (aktualniFilmi == null)
            {
                return NotFound();
            }

            return View(aktualniFilmi);
        }

        // GET: Aktualno/Create
        public IActionResult Create()
        {
            ViewData["IdPodjetja"] = new SelectList(_context.Podjetja, "IdPodjetja", "IdPodjetja");
            return View();
        }

        // POST: Aktualno/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAktualenFilm,IdPodjetja,SlovenskiNaslov,AngleskiNaslov,Zanr,Dolzina,NaSporeduOd")] AktualniFilmi aktualniFilmi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aktualniFilmi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPodjetja"] = new SelectList(_context.Podjetja, "IdPodjetja", "IdPodjetja", aktualniFilmi.IdPodjetja);
            return View(aktualniFilmi);
        }

        // GET: Aktualno/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aktualniFilmi = await _context.AktualniFilmi.FindAsync(id);
            if (aktualniFilmi == null)
            {
                return NotFound();
            }
            ViewData["IdPodjetja"] = new SelectList(_context.Podjetja, "IdPodjetja", "IdPodjetja", aktualniFilmi.IdPodjetja);
            return View(aktualniFilmi);
        }

        // POST: Aktualno/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdAktualenFilm,IdPodjetja,SlovenskiNaslov,AngleskiNaslov,Zanr,Dolzina,NaSporeduOd")] AktualniFilmi aktualniFilmi)
        {
            if (id != aktualniFilmi.IdAktualenFilm)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aktualniFilmi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AktualniFilmiExists(aktualniFilmi.IdAktualenFilm))
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
            ViewData["IdPodjetja"] = new SelectList(_context.Podjetja, "IdPodjetja", "IdPodjetja", aktualniFilmi.IdPodjetja);
            return View(aktualniFilmi);
        }

        // GET: Aktualno/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aktualniFilmi = await _context.AktualniFilmi
                .Include(a => a.IdPodjetjaNavigation)
                .FirstOrDefaultAsync(m => m.IdAktualenFilm == id);
            if (aktualniFilmi == null)
            {
                return NotFound();
            }

            return View(aktualniFilmi);
        }

        // POST: Aktualno/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aktualniFilmi = await _context.AktualniFilmi.FindAsync(id);
            _context.AktualniFilmi.Remove(aktualniFilmi);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AktualniFilmiExists(int id)
        {
            return _context.AktualniFilmi.Any(e => e.IdAktualenFilm == id);
        }
    }
}
