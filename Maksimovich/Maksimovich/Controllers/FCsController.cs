using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Maksimovich.Models;

namespace Maksimovich.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FCsController : ControllerBase
    {
        private readonly FCContext _context;

        public FCsController(FCContext context)
        {
            _context = context;
        }

        // GET: api/FCs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FC>>> GetFC()
        {
            return await _context.FC.ToListAsync();
        }

        // GET: api/FCs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FC>> GetFC(long id)
        {
            var fC = await _context.FC.FindAsync(id);

            if (fC == null)
            {
                return NotFound();
            }

            return fC;
        }

        // PUT: api/FCs/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<FC>> PostFC(FC fC)
        {
            _context.FC.Add(fC);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFC", new { id = fC.Id }, fC);
        }

        // DELETE: api/FCs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<FC>> DeleteFC(long id)
        {
            var fC = await _context.FC.FindAsync(id);
            if (fC == null)
            {
                return NotFound();
            }

            _context.FC.Remove(fC);
            await _context.SaveChangesAsync();

            return fC;
        }

        private bool FCExists(long id)
        {
            return _context.FC.Any(e => e.Id == id);
        }
    }
}
