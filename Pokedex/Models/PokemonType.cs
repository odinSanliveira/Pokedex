using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.Models
{
    public  class PokemonType
    {
        [Key()]
        public int TypeID { get; set; }
        public NamedAPIResource type { get; set; }

    }
}
