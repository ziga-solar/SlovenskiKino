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
    public class PodjetjaController : ControllerBase
    {
        private readonly KinoContext _context;

        public PodjetjaController(KinoContext context)
        {
            _context = context;
        }

        // GET: api/Podjetja
        [HttpGet]
        public IEnumerable<Podjetja> GetPodjetja()
        {
            return _context.Podjetja;
        }

        // GET: api/Podjetja/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPodjetja([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var podjetja = await _context.Podjetja.FindAsync(id);

            if (podjetja == null)
            {
                return NotFound();
            }

            return Ok(podjetja);
        }

        // PUT: api/Podjetja/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPodjetja([FromRoute] int id, [FromBody] Podjetja podjetja)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != podjetja.IdPodjetja)
            {
                return BadRequest();
            }

            _context.Entry(podjetja).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PodjetjaExists(id))
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

        // POST: api/Podjetja
        [HttpPost]
        public async Task<IActionResult> PostPodjetja([FromBody] Podjetja podjetja)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Podjetja.Add(podjetja);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPodjetja", new { id = podjetja.IdPodjetja }, podjetja);
        }

        // DELETE: api/Podjetja/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePodjetja([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var podjetja = await _context.Podjetja.FindAsync(id);
            if (podjetja == null)
            {
                return NotFound();
            }

            _context.Podjetja.Remove(podjetja);
            await _context.SaveChangesAsync();

            return Ok(podjetja);
        }

        private bool PodjetjaExists(int id)
        {
            return _context.Podjetja.Any(e => e.IdPodjetja == id);
        }
    }
}