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
    public class PlayersController : ControllerBase
    {
        private readonly DBContext _context;

        public PlayersController(DBContext context)
        {
            _context = context;
        }

        // GET: api/Players
        [HttpGet]
        public IEnumerable<Player> GetPlayers()
        {
            return  _context.getAllPlayers();
        }

        // GET: api/Players/5
        [HttpGet("{id}")]
        public Player GetPlayer(long id)
        {
            return _context.getPlayer(id);
        }

        // PUT: api/Players/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlayer(long id, Player player)
        {
            if (id != player.Id)
            {
                return BadRequest();
            }

            _context.Entry(player).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerExists(id))
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

        // POST: api/Players
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("toClub/{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<Player>> PostPlayer(Player player, long id)
        {
            if(_context.getAllPlayers().FirstOrDefault(p=>p.Id==player.Id) != null)
                return BadRequest();

            var FC = await _context.FCs.FindAsync(id);

            if (FC == null)
                return BadRequest();

            FC.Players.Add(player);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PlayerExists(player.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPlayer", new { id = player.Id }, player);
        }

        // DELETE: api/Players/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<Player>> DeletePlayer(long id)
        {
            var player = _context.getPlayer(id);

            if (player == null)
            {
                return NotFound();
            }

            _context.FCs.Where(f => f.Players.FirstOrDefault(p => p.Id == id) != null).FirstOrDefault().Players.Remove(player);
            await _context.SaveChangesAsync();

            return player;
        }

        private bool PlayerExists(long id)
        {
            return _context.getAllPlayers().Any(e => e.Id == id);
        }
    }
}
