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
    public class NapovednikController : ControllerBase
    {
        private readonly KinoContext _context;

        public NapovednikController(KinoContext context)
        {
            _context = context;
        }

        // GET: api/Napovednik
        [HttpGet]
        public IEnumerable<Napovedi> GetNapovedi()
        {
            return _context.Napovedi;
        }

        // GET: api/Napovednik/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNapovedi([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var napovedi = await _context.Napovedi.FindAsync(id);

            if (napovedi == null)
            {
                return NotFound();
            }

            return Ok(napovedi);
        }

        // PUT: api/Napovednik/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNapovedi([FromRoute] int id, [FromBody] Napovedi napovedi)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != napovedi.IdNapoved)
            {
                return BadRequest();
            }

            _context.Entry(napovedi).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NapovediExists(id))
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

        // POST: api/Napovednik
        [HttpPost]
        public async Task<IActionResult> PostNapovedi([FromBody] Napovedi napovedi)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Napovedi.Add(napovedi);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNapovedi", new { id = napovedi.IdNapoved }, napovedi);
        }

        // DELETE: api/Napovednik/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNapovedi([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var napovedi = await _context.Napovedi.FindAsync(id);
            if (napovedi == null)
            {
                return NotFound();
            }

            _context.Napovedi.Remove(napovedi);
            await _context.SaveChangesAsync();

            return Ok(napovedi);
        }

        private bool NapovediExists(int id)
        {
            return _context.Napovedi.Any(e => e.IdNapoved == id);
        }
    }
}