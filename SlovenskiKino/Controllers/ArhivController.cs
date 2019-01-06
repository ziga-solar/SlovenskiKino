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
    public class ArhivController : Controller
    {
        private readonly KinoContext _context;

        public ArhivController(KinoContext context)
        {
            _context = context;
        }

        // GET: Arhiv
        public async Task<IActionResult> Index()
        {
            return View(await _context.ArhivFilmov.ToListAsync());
        }

        // GET: Arhiv/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var arhivFilmov = await _context.ArhivFilmov
                .FirstOrDefaultAsync(m => m.IdFilma == id);
            if (arhivFilmov == null)
            {
                return NotFound();
            }

            return View(arhivFilmov);
        }

        // GET: Arhiv/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Arhiv/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdFilma,SlovenskiNaslov,AngleskiNaslov,Zanr,Dolzina,Datum")] ArhivFilmov arhivFilmov)
        {
            if (ModelState.IsValid)
            {
                _context.Add(arhivFilmov);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(arhivFilmov);
        }

        // GET: Arhiv/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var arhivFilmov = await _context.ArhivFilmov.FindAsync(id);
            if (arhivFilmov == null)
            {
                return NotFound();
            }
            return View(arhivFilmov);
        }

        // POST: Arhiv/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdFilma,SlovenskiNaslov,AngleskiNaslov,Zanr,Dolzina,Datum")] ArhivFilmov arhivFilmov)
        {
            if (id != arhivFilmov.IdFilma)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(arhivFilmov);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArhivFilmovExists(arhivFilmov.IdFilma))
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
            return View(arhivFilmov);
        }

        // GET: Arhiv/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var arhivFilmov = await _context.ArhivFilmov
                .FirstOrDefaultAsync(m => m.IdFilma == id);
            if (arhivFilmov == null)
            {
                return NotFound();
            }

            return View(arhivFilmov);
        }

        // POST: Arhiv/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var arhivFilmov = await _context.ArhivFilmov.FindAsync(id);
            _context.ArhivFilmov.Remove(arhivFilmov);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArhivFilmovExists(int id)
        {
            return _context.ArhivFilmov.Any(e => e.IdFilma == id);
        }
    }
}
