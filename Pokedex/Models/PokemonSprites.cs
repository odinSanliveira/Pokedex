using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.Models
{
    public class PokemonSprites
    {
        [Key()]
        public int SpriteID {get; set;}
        public string front_default { get; set; }
    }
}
