using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Maksimovich.Models
{
    public class FC
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public ICollection<Player> Players { get; set; }
        public string Coach { get; set; }

        public string getCoach()
        {
            return "club's head coach is" + Coach;
        }
    }
}
