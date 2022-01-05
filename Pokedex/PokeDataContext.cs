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
            var sFilePath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "storedPokemon199956.db");
            //C:\Users\ResourcePC\AppData\Local\Packages\556a5a1e-b6e2-4bca-8b27-5ff0c19d7593_jmcehrcabdhea\LocalState
            optionsBuilder.UseSqlite("Data Source =" + sFilePath);
           //optionsBuilder.UseLazyLoadingProxies();
        }




        
        public DbSet<Pokemon> Pokemon { get; set; }
        public DbSet<PokemonSprites> Sprite { get; set; }
        public DbSet<PokemonType> TypeList { get; set; }
        public DbSet<TypeDetail> Types { get; set; }
        public DbSet<Stat> Stats { get; set; }
        public DbSet<NamedAPIResourceList> Listing { get; set; }
        public DbSet<PokemonCRUD> UserPokemon   { get; set; }

    }
}
