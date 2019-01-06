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
    public class KinoController : ControllerBase
    {
        private readonly KinoContext _context;

        public KinoController(KinoContext context)
        {
            _context = context;
        }

        // GET: api/Kino
        [HttpGet]
        public IEnumerable<Kinematografi> GetKinematografi()
        {
            return _context.Kinematografi;
        }

        // GET: api/Kino/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetKinematografi([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var kinematografi = await _context.Kinematografi.FindAsync(id);

            if (kinematografi == null)
            {
                return NotFound();
            }

            return Ok(kinematografi);
        }

        // PUT: api/Kino/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKinematografi([FromRoute] int id, [FromBody] Kinematografi kinematografi)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != kinematografi.IdKinematograf)
            {
                return BadRequest();
            }

            _context.Entry(kinematografi).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KinematografiExists(id))
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

        // POST: api/Kino
        [HttpPost]
        public async Task<IActionResult> PostKinematografi([FromBody] Kinematografi kinematografi)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Kinematografi.Add(kinematografi);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetKinematografi", new { id = kinematografi.IdKinematograf }, kinematografi);
        }

        // DELETE: api/Kino/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKinematografi([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var kinematografi = await _context.Kinematografi.FindAsync(id);
            if (kinematografi == null)
            {
                return NotFound();
            }

            _context.Kinematografi.Remove(kinematografi);
            await _context.SaveChangesAsync();

            return Ok(kinematografi);
        }

        private bool KinematografiExists(int id)
        {
            return _context.Kinematografi.Any(e => e.IdKinematograf == id);
        }
    }
}