using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.Models
{
    public class PokemonType
    {
        public int Pokemonid { get; set; }
        public int id { get; set; }
        [ForeignKey("Types")]
        public int TypeID { get; set; }
        public virtual NamedAPIResource type { get; set; }

    }
}
