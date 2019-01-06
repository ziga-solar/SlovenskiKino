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
    public class ArhivController : ControllerBase
    {
        private readonly KinoContext _context;

        public ArhivController(KinoContext context)
        {
            _context = context;
        }

        // GET: api/Arhiv
        [HttpGet]
        public IEnumerable<ArhivFilmov> GetArhivFilmov()
        {
            return _context.ArhivFilmov;
        }

        // GET: api/Arhiv/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetArhivFilmov([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var arhivFilmov = await _context.ArhivFilmov.FindAsync(id);

            if (arhivFilmov == null)
            {
                return NotFound();
            }

            return Ok(arhivFilmov);
        }

        // PUT: api/Arhiv/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArhivFilmov([FromRoute] int id, [FromBody] ArhivFilmov arhivFilmov)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != arhivFilmov.IdFilma)
            {
                return BadRequest();
            }

            _context.Entry(arhivFilmov).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArhivFilmovExists(id))
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

        // POST: api/Arhiv
        [HttpPost]
        public async Task<IActionResult> PostArhivFilmov([FromBody] ArhivFilmov arhivFilmov)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ArhivFilmov.Add(arhivFilmov);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArhivFilmov", new { id = arhivFilmov.IdFilma }, arhivFilmov);
        }

        // DELETE: api/Arhiv/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArhivFilmov([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var arhivFilmov = await _context.ArhivFilmov.FindAsync(id);
            if (arhivFilmov == null)
            {
                return NotFound();
            }

            _context.ArhivFilmov.Remove(arhivFilmov);
            await _context.SaveChangesAsync();

            return Ok(arhivFilmov);
        }

        private bool ArhivFilmovExists(int id)
        {
            return _context.ArhivFilmov.Any(e => e.IdFilma == id);
        }
    }
}