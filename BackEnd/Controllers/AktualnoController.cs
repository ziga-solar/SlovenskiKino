using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SlovenskiKino.Models;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AktualnoController : ControllerBase
    {
        private readonly KinoContext _context;

        public AktualnoController(KinoContext context)
        {
            _context = context;
        }

        // GET: api/Aktualno
        [HttpGet]
        public IEnumerable<AktualniFilmi> GetAktualniFilmi()
        {
            return _context.AktualniFilmi;
        }

        // GET: api/Aktualno/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAktualniFilmi([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var aktualniFilmi = await _context.AktualniFilmi.FindAsync(id);

            if (aktualniFilmi == null)
            {
                return NotFound();
            }

            return Ok(aktualniFilmi);
        }

        // PUT: api/Aktualno/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAktualniFilmi([FromRoute] int id, [FromBody] AktualniFilmi aktualniFilmi)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != aktualniFilmi.IdAktualenFilm)
            {
                return BadRequest();
            }

            _context.Entry(aktualniFilmi).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AktualniFilmiExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Aktualno
        [HttpPost]
        public async Task<IActionResult> PostAktualniFilmi([FromBody] AktualniFilmi aktualniFilmi)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.AktualniFilmi.Add(aktualniFilmi);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAktualniFilmi", new { id = aktualniFilmi.IdAktualenFilm }, aktualniFilmi);
        }

        // DELETE: api/Aktualno/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAktualniFilmi([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var aktualniFilmi = await _context.AktualniFilmi.FindAsync(id);
            if (aktualniFilmi == null)
            {
                return NotFound();
            }

            _context.AktualniFilmi.Remove(aktualniFilmi);
            await _context.SaveChangesAsync();

            return Ok(aktualniFilmi);
        }

        private bool AktualniFilmiExists(int id)
        {
            return _context.AktualniFilmi.Any(e => e.IdAktualenFilm == id);
        }
    }
}