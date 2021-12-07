using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pokedex.Models;
using System.Collections.ObjectModel;
using System.IO;
using Windows.Storage;

namespace Pokedex
{
    public class PokeDataContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var sFilePath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "storedPokemon2250.db");
            optionsBuilder.UseSqlite("Data Source =" + sFilePath);
        }


        
        public DbSet<Pokemon> Pokemon { get; set; }
        public DbSet<PokemonSprites> Sprite { get; set; }
        public DbSet<TypeName> Types { get; set; }
        public DbSet<Stat> Stats { get; set; }

    }
}
