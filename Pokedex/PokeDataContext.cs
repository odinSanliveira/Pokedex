using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pokedex.Models;
using System.Collections.ObjectModel;

namespace Pokedex
{
    public class PokeDataContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = StoredPokemonData.db");
        }


        
        public DbSet<Pokemon> StoredPokemon { get; set; }
        public DbSet<PokemonSprites> StoredSprite { get; set; }
        public DbSet<PokemonType> StoredType { get; set; }
        public DbSet<Stat> StoredStat { get; set; }

    }
}
