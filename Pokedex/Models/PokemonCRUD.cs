using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.Models
{
    public class PokemonCRUD
    {
        [Key()]
        public int id { get; set; }
        public string pokemonName { get; set; }
        public string pokemonType { get; set; }
        public string pokemonType2 { get; set; }
        public int HPCrud { get; set; }
        public int AttackCrud { get; set; }
        public int DefenseCrud { get; set; }
        public int SpecialAttackCrud { get; set; }
        public int SpecialDefenseCrud   { get; set; }
        public int Speed { get; set; }
        public int heightCRUD { get; set; }
        public int weightCRUD   { get; set; }
    }
}
