using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.Models
{
    public class Stat
    {
        public long base_stat { get; set; }
        public long effort { get; set; }
        public Species stat { get; set; }
    }
}
