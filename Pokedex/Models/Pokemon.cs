using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.Models
{
    public class Pokemon
    {
        public int id { get; set; }
        public string name { get; set; }
        //public Species Species { get; set; }
        public PokemonSprites sprites { get; set; }
    }
}
