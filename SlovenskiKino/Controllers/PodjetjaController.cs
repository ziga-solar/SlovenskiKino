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
    public class PodjetjaController : Controller
    {
        private readonly KinoContext _context;

        public PodjetjaController(KinoContext context)
        {
            _context = context;
        }

        // GET: Podjetja
        public async Task<IActionResult> Index()
        {
            return View(await _context.Podjetja.ToListAsync());
        }

        // GET: Podjetja/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var podjetja = await _context.Podjetja
                .FirstOrDefaultAsync(m => m.IdPodjetja == id);
            if (podjetja == null)
            {
                return NotFound();
            }

            return View(podjetja);
        }

        // GET: Podjetja/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Podjetja/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPodjetja,Podjetje")] Podjetja podjetja)
        {
            if (ModelState.IsValid)
            {
                _context.Add(podjetja);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(podjetja);
        }

        // GET: Podjetja/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var podjetja = await _context.Podjetja.FindAsync(id);
            if (podjetja == null)
            {
                return NotFound();
            }
            return View(podjetja);
        }

        // POST: Podjetja/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPodjetja,Podjetje")] Podjetja podjetja)
        {
            if (id != podjetja.IdPodjetja)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(podjetja);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PodjetjaExists(podjetja.IdPodjetja))
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
            return View(podjetja);
        }

        // GET: Podjetja/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var podjetja = await _context.Podjetja
                .FirstOrDefaultAsync(m => m.IdPodjetja == id);
            if (podjetja == null)
            {
                return NotFound();
            }

            return View(podjetja);
        }

        // POST: Podjetja/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var podjetja = await _context.Podjetja.FindAsync(id);
            _context.Podjetja.Remove(podjetja);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PodjetjaExists(int id)
        {
            return _context.Podjetja.Any(e => e.IdPodjetja == id);
        }
    }
}
