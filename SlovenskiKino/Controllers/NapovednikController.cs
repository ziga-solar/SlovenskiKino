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
    public class NapovednikController : Controller
    {
        private readonly KinoContext _context;

        public NapovednikController(KinoContext context)
        {
            _context = context;
        }

        // GET: Napovednik
        public async Task<IActionResult> Index()
        {
            var kinoContext = _context.Napovedi.Include(n => n.IdPodjetjaNavigation);
            return View(await kinoContext.ToListAsync());
        }

        // GET: Napovednik/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var napovedi = await _context.Napovedi
                .Include(n => n.IdPodjetjaNavigation)
                .FirstOrDefaultAsync(m => m.IdNapoved == id);
            if (napovedi == null)
            {
                return NotFound();
            }

            return View(napovedi);
        }

        // GET: Napovednik/Create
        public IActionResult Create()
        {
            ViewData["IdPodjetja"] = new SelectList(_context.Podjetja, "IdPodjetja", "IdPodjetja");
            return View();
        }

        // POST: Napovednik/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdNapoved,IdPodjetja,SlovenskiNaslov,AngleskiNaslov,Zanr,Dolzina,NaSporeduOd")] Napovedi napovedi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(napovedi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPodjetja"] = new SelectList(_context.Podjetja, "IdPodjetja", "IdPodjetja", napovedi.IdPodjetja);
            return View(napovedi);
        }

        // GET: Napovednik/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var napovedi = await _context.Napovedi.FindAsync(id);
            if (napovedi == null)
            {
                return NotFound();
            }
            ViewData["IdPodjetja"] = new SelectList(_context.Podjetja, "IdPodjetja", "IdPodjetja", napovedi.IdPodjetja);
            return View(napovedi);
        }

        // POST: Napovednik/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdNapoved,IdPodjetja,SlovenskiNaslov,AngleskiNaslov,Zanr,Dolzina,NaSporeduOd")] Napovedi napovedi)
        {
            if (id != napovedi.IdNapoved)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(napovedi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NapovediExists(napovedi.IdNapoved))
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
            ViewData["IdPodjetja"] = new SelectList(_context.Podjetja, "IdPodjetja", "IdPodjetja", napovedi.IdPodjetja);
            return View(napovedi);
        }

        // GET: Napovednik/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var napovedi = await _context.Napovedi
                .Include(n => n.IdPodjetjaNavigation)
                .FirstOrDefaultAsync(m => m.IdNapoved == id);
            if (napovedi == null)
            {
                return NotFound();
            }

            return View(napovedi);
        }

        // POST: Napovednik/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var napovedi = await _context.Napovedi.FindAsync(id);
            _context.Napovedi.Remove(napovedi);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NapovediExists(int id)
        {
            return _context.Napovedi.Any(e => e.IdNapoved == id);
        }
    }
}
