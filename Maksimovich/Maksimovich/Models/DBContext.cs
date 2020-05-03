using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Maksimovich.Models
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {
        }

        //public DbSet<Player> Players { get; set; }
        public DbSet<FC> FCs { get; set; }

        public IEnumerable<Player> getCaptain(long id)
        {
            var Club = FCs.Where(f => f.Id == id).Select(p => p.Players).AsNoTracking().FirstOrDefault();
            if (Club == null)
                return null;
            else
                return Club.Where(p => p.IsCaptain);
        }

        public IEnumerable<FC> getBigFCs(IEnumerable<FC> Clubs, int c)
        {
            return Clubs.Where(p => p.Players.Count > c);
        }

        public List<Player> getAllPlayers()
        {
            var res = new List<Player>();
            foreach (var i in FCs.ToList())
            {
                res.AddRange(i.Players);
            }
            return res;
        }

        public Player getPlayer(long id)
        {
            foreach (var i in FCs.ToList())
            {
                var res = i.Players.FirstOrDefault(p => p.Id == id);
                if (res != null)
                    return res;
            }
            return null;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FC>()
               .OwnsMany(property => property.Players);
        }
    }
}
