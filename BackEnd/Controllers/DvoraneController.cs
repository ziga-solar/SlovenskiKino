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
    public class DvoraneController : ControllerBase
    {
        private readonly KinoContext _context;

        public DvoraneController(KinoContext context)
        {
            _context = context;
        }

        // GET: api/Dvorane
        [HttpGet]
        public IEnumerable<InfoOdvoranah> GetInfoOdvoranah()
        {
            return _context.InfoOdvoranah;
        }

        // GET: api/Dvorane/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInfoOdvoranah([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var infoOdvoranah = await _context.InfoOdvoranah.FindAsync(id);

            if (infoOdvoranah == null)
            {
                return NotFound();
            }

            return Ok(infoOdvoranah);
        }

        // PUT: api/Dvorane/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInfoOdvoranah([FromRoute] int id, [FromBody] InfoOdvoranah infoOdvoranah)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != infoOdvoranah.IdInfo)
            {
                return BadRequest();
            }

            _context.Entry(infoOdvoranah).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InfoOdvoranahExists(id))
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

        // POST: api/Dvorane
        [HttpPost]
        public async Task<IActionResult> PostInfoOdvoranah([FromBody] InfoOdvoranah infoOdvoranah)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.InfoOdvoranah.Add(infoOdvoranah);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInfoOdvoranah", new { id = infoOdvoranah.IdInfo }, infoOdvoranah);
        }

        // DELETE: api/Dvorane/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInfoOdvoranah([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var infoOdvoranah = await _context.InfoOdvoranah.FindAsync(id);
            if (infoOdvoranah == null)
            {
                return NotFound();
            }

            _context.InfoOdvoranah.Remove(infoOdvoranah);
            await _context.SaveChangesAsync();

            return Ok(infoOdvoranah);
        }

        private bool InfoOdvoranahExists(int id)
        {
            return _context.InfoOdvoranah.Any(e => e.IdInfo == id);
        }
    }
}