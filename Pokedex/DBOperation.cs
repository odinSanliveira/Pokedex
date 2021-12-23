using Pokedex.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex
{
    public class DBOperation
    {

        public static ObservableCollection<Pokemon> ReadDB(ObservableCollection<Pokemon> empty)
        {
            using (var db = new PokeDataContext()) {

                var SaveLists = from TypeList in db.TypeList
                             join Types in db.Types
                             on TypeList.TypeID equals Types.id
                             select new PokemonType { id = TypeList.id, Pokemonid = TypeList.Pokemonid, type = Types, TypeID = Types.id };

                var SaveStats = from Stats in db.Stats
                               select new Stat { Pokemonid = Stats.Pokemonid, id = Stats.id, base_stat = Stats.base_stat };

                var SavePokemon = (from PokemonUnit in db.Pokemon
                              join Sprites in db.Sprite
                              on PokemonUnit.id equals Sprites.id
                              select new Pokemon { id = PokemonUnit.id, 
                                  name = PokemonUnit.name, 
                                  sprites = Sprites,
                                  height=PokemonUnit.height,
                                  types = (List<PokemonType>)SaveLists.Where(m => m.Pokemonid == PokemonUnit.id),
                                  stats = (List<Stat>)SaveStats.Where(m => m.Pokemonid == PokemonUnit.id),
                                  weight=PokemonUnit.weight }).ToList<Pokemon>();


                ObservableCollection<Pokemon> storedPokemonList = new ObservableCollection<Pokemon>(SavePokemon);
                empty = storedPokemonList;
                return empty;
               
            }

        }

        public static ObservableCollection<Pokemon> SearchDBByID(ObservableCollection<Pokemon> empty)
        {
            using (var db = new PokeDataContext())
            {
                int myint = 3;

                var SaveLists = from TypeList in db.TypeList
                                join Types in db.Types
                                on TypeList.TypeID equals Types.id
                                select new PokemonType { id = TypeList.id, Pokemonid = TypeList.Pokemonid, type = Types, TypeID = Types.id };

                var SaveStats = from Stats in db.Stats
                                select new Stat { Pokemonid = Stats.Pokemonid, id = Stats.id, base_stat = Stats.base_stat };

                var SavePokemon = (from PokemonUnit in db.Pokemon
                                   join Sprites in db.Sprite
                                   on PokemonUnit.id equals Sprites.id
                                   where PokemonUnit.id.Equals(myint)
                                   select new Pokemon
                                   {
                                       id = PokemonUnit.id,
                                       name = PokemonUnit.name,
                                       sprites = Sprites,
                                       height=PokemonUnit.height,
                                       types = (List<PokemonType>)SaveLists.Where(m => m.Pokemonid == PokemonUnit.id),
                                       stats = (List<Stat>)SaveStats.Where(m => m.Pokemonid == PokemonUnit.id),
                                       weight=PokemonUnit.weight
                                   }
                                   ).ToList<Pokemon>();

                ObservableCollection<Pokemon> storedPokemonList = new ObservableCollection<Pokemon>(SavePokemon);
                empty = storedPokemonList;
                return empty;



            }
        }

    }
}
