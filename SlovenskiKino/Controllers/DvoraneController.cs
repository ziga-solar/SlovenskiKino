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
    public class DvoraneController : Controller
    {
        private readonly KinoContext _context;

        public DvoraneController(KinoContext context)
        {
            _context = context;
        }

        // GET: Dvorane
        public async Task<IActionResult> Index()
        {
            var kinoContext = _context.InfoOdvoranah.Include(i => i.IdKinematografNavigation);
            return View(await kinoContext.ToListAsync());
        }

        // GET: Dvorane/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var infoOdvoranah = await _context.InfoOdvoranah
                .Include(i => i.IdKinematografNavigation)
                .FirstOrDefaultAsync(m => m.IdInfo == id);
            if (infoOdvoranah == null)
            {
                return NotFound();
            }

            return View(infoOdvoranah);
        }

        // GET: Dvorane/Create
        public IActionResult Create()
        {
            ViewData["IdKinematograf"] = new SelectList(_context.Kinematografi, "IdKinematograf", "IdKinematograf");
            return View();
        }

        // POST: Dvorane/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdInfo,IdKinematograf,Dvorana,SteviloSedezov,SteviloVrst,InvalidskiSedezi,Podpora3D")] InfoOdvoranah infoOdvoranah)
        {
            if (ModelState.IsValid)
            {
                _context.Add(infoOdvoranah);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdKinematograf"] = new SelectList(_context.Kinematografi, "IdKinematograf", "IdKinematograf", infoOdvoranah.IdKinematograf);
            return View(infoOdvoranah);
        }

        // GET: Dvorane/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var infoOdvoranah = await _context.InfoOdvoranah.FindAsync(id);
            if (infoOdvoranah == null)
            {
                return NotFound();
            }
            ViewData["IdKinematograf"] = new SelectList(_context.Kinematografi, "IdKinematograf", "IdKinematograf", infoOdvoranah.IdKinematograf);
            return View(infoOdvoranah);
        }

        // POST: Dvorane/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdInfo,IdKinematograf,Dvorana,SteviloSedezov,SteviloVrst,InvalidskiSedezi,Podpora3D")] InfoOdvoranah infoOdvoranah)
        {
            if (id != infoOdvoranah.IdInfo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(infoOdvoranah);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InfoOdvoranahExists(infoOdvoranah.IdInfo))
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
            ViewData["IdKinematograf"] = new SelectList(_context.Kinematografi, "IdKinematograf", "IdKinematograf", infoOdvoranah.IdKinematograf);
            return View(infoOdvoranah);
        }

        // GET: Dvorane/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var infoOdvoranah = await _context.InfoOdvoranah
                .Include(i => i.IdKinematografNavigation)
                .FirstOrDefaultAsync(m => m.IdInfo == id);
            if (infoOdvoranah == null)
            {
                return NotFound();
            }

            return View(infoOdvoranah);
        }

        // POST: Dvorane/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var infoOdvoranah = await _context.InfoOdvoranah.FindAsync(id);
            _context.InfoOdvoranah.Remove(infoOdvoranah);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InfoOdvoranahExists(int id)
        {
            return _context.InfoOdvoranah.Any(e => e.IdInfo == id);
        }
    }
}
