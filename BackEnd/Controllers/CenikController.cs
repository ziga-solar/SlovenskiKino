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
    public class CenikController : ControllerBase
    {
        private readonly KinoContext _context;

        public CenikController(KinoContext context)
        {
            _context = context;
        }

        // GET: api/Cenik
        [HttpGet]
        public IEnumerable<CenikVstopnic> GetCenikVstopnic()
        {
            return _context.CenikVstopnic;
        }

        // GET: api/Cenik/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCenikVstopnic([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cenikVstopnic = await _context.CenikVstopnic.FindAsync(id);

            if (cenikVstopnic == null)
            {
                return NotFound();
            }

            return Ok(cenikVstopnic);
        }

        // PUT: api/Cenik/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCenikVstopnic([FromRoute] int id, [FromBody] CenikVstopnic cenikVstopnic)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cenikVstopnic.IdCenik)
            {
                return BadRequest();
            }

            _context.Entry(cenikVstopnic).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CenikVstopnicExists(id))
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

        // POST: api/Cenik
        [HttpPost]
        public async Task<IActionResult> PostCenikVstopnic([FromBody] CenikVstopnic cenikVstopnic)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.CenikVstopnic.Add(cenikVstopnic);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCenikVstopnic", new { id = cenikVstopnic.IdCenik }, cenikVstopnic);
        }

        // DELETE: api/Cenik/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCenikVstopnic([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cenikVstopnic = await _context.CenikVstopnic.FindAsync(id);
            if (cenikVstopnic == null)
            {
                return NotFound();
            }

            _context.CenikVstopnic.Remove(cenikVstopnic);
            await _context.SaveChangesAsync();

            return Ok(cenikVstopnic);
        }

        private bool CenikVstopnicExists(int id)
        {
            return _context.CenikVstopnic.Any(e => e.IdCenik == id);
        }
    }
}