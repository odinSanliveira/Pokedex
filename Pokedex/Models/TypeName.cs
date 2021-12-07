using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.Models
{
    public class TypeName
    {
        
        public int TypenameID { get; set; }
        public string name { get; set; }

        public ICollection<PokemonType> PokemonType { get; set; }
    }
}
