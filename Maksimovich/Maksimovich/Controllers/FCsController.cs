using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Maksimovich.Models;
using Microsoft.AspNetCore.Authorization;

namespace Maksimovich.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FCsController : ControllerBase
    {
        private readonly DBContext _context;

        public FCsController(DBContext context)
        {
            _context = context;
        }

        // GET: api/FCs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FC>>> GetFCs()
        {
            return await _context.FCs.ToListAsync();
        }

        // GET: api/FCs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FC>> GetFC(long id)
        {
            var fC = await _context.FCs.FindAsync(id);

            if (fC == null)
            {
                return NotFound();
            }

            return fC;
        }

        [HttpGet("{id}/Coach")]
        [Authorize]
        public string GetCoach(long id)
        {
            var fc = _context.FCs.Find(id);

            if (fc == null)
            {
                return null;
            }
            return fc.getCoach();
        }

        [HttpGet("{id}/Captain")]
        [Authorize]
        public IEnumerable<Player> GetCaptain(long id)
        {
            return _context.getCaptain(id);
        }

        [HttpGet("Big/{alter}")]
        [Authorize]
        public IEnumerable<FC> GetBig(int alter)
        {
            return _context.getBigFCs(_context.FCs, alter);

        }

        // PUT: api/FCs/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFC(long id, FC fC)
        {
            if (id != fC.Id)
            {
                return BadRequest();
            }

            _context.Entry(fC).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FCExists(id))
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

        // POST: api/FCs
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<FC>> PostFC(FC fC)
        {
            if (_context.FCs.FirstOrDefault(f => f.Id == fC.Id) != null)
                return BadRequest();

            _context.FCs.Add(fC);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFC", new { id = fC.Id }, fC);
        }

        // DELETE: api/FCs/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<FC>> DeleteFC(long id)
        {
            var fC = await _context.FCs.FindAsync(id);
            if (fC == null)
            {
                return NotFound();
            }

            _context.FCs.Remove(fC);
            await _context.SaveChangesAsync();

            return fC;
        }

        private bool FCExists(long id)
        {
            return _context.FCs.Any(e => e.Id == id);
        }
    }
}
