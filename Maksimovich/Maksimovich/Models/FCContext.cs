using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Maksimovich.Models;

namespace Maksimovich.Models
{
    public class FCContext : DbContext
    {
        public FCContext(DbContextOptions<FCContext> options)
            : base(options)
        {
        }

        public DbSet<Player> FCs { get; set; }

        public DbSet<Maksimovich.Models.FC> FC { get; set; }
    }
}