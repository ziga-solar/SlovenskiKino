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
    public class CenikController : Controller
    {
        private readonly KinoContext _context;

        public CenikController(KinoContext context)
        {
            _context = context;
        }

        // GET: Cenik
        public async Task<IActionResult> Index()
        {
            var kinoContext = _context.CenikVstopnic.Include(c => c.IdPodjetjaNavigation);
            return View(await kinoContext.ToListAsync());
        }

        // GET: Cenik/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cenikVstopnic = await _context.CenikVstopnic
                .Include(c => c.IdPodjetjaNavigation)
                .FirstOrDefaultAsync(m => m.IdCenik == id);
            if (cenikVstopnic == null)
            {
                return NotFound();
            }

            return View(cenikVstopnic);
        }

        // GET: Cenik/Create
        public IActionResult Create()
        {
            ViewData["IdPodjetja"] = new SelectList(_context.Podjetja, "IdPodjetja", "IdPodjetja");
            return View();
        }

        // POST: Cenik/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCenik,IdPodjetja,RednaCena,CenaSpopustom,StudentskaCena,Doplacilo3D,DoplaciloZa120")] CenikVstopnic cenikVstopnic)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cenikVstopnic);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPodjetja"] = new SelectList(_context.Podjetja, "IdPodjetja", "IdPodjetja", cenikVstopnic.IdPodjetja);
            return View(cenikVstopnic);
        }

        // GET: Cenik/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cenikVstopnic = await _context.CenikVstopnic.FindAsync(id);
            if (cenikVstopnic == null)
            {
                return NotFound();
            }
            ViewData["IdPodjetja"] = new SelectList(_context.Podjetja, "IdPodjetja", "IdPodjetja", cenikVstopnic.IdPodjetja);
            return View(cenikVstopnic);
        }

        // POST: Cenik/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCenik,IdPodjetja,RednaCena,CenaSpopustom,StudentskaCena,Doplacilo3D,DoplaciloZa120")] CenikVstopnic cenikVstopnic)
        {
            if (id != cenikVstopnic.IdCenik)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cenikVstopnic);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CenikVstopnicExists(cenikVstopnic.IdCenik))
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
            ViewData["IdPodjetja"] = new SelectList(_context.Podjetja, "IdPodjetja", "IdPodjetja", cenikVstopnic.IdPodjetja);
            return View(cenikVstopnic);
        }

        // GET: Cenik/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cenikVstopnic = await _context.CenikVstopnic
                .Include(c => c.IdPodjetjaNavigation)
                .FirstOrDefaultAsync(m => m.IdCenik == id);
            if (cenikVstopnic == null)
            {
                return NotFound();
            }

            return View(cenikVstopnic);
        }

        // POST: Cenik/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cenikVstopnic = await _context.CenikVstopnic.FindAsync(id);
            _context.CenikVstopnic.Remove(cenikVstopnic);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CenikVstopnicExists(int id)
        {
            return _context.CenikVstopnic.Any(e => e.IdCenik == id);
        }
    }
}
