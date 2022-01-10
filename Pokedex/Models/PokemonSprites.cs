using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.Models
{
    public class PokemonSprites
    {
        [ForeignKey("Pokemon")]
        public int Pokemonid { get; set; }
        public int id { get; set; }
        public string front_default { get; set; }
    }
}
