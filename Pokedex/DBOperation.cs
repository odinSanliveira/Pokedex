using PagedList;
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

        public static ObservableCollection<Pokemon> ReadDB(ObservableCollection<Pokemon> empty, int Index)
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
                                   select new Pokemon
                                   {
                                       id = PokemonUnit.id,
                                       name = PokemonUnit.name,
                                       sprites = Sprites,
                                       height=PokemonUnit.height,
                                       types = (List<PokemonType>)SaveLists.Where(m => m.Pokemonid == PokemonUnit.id),
                                       stats = (List<Stat>)SaveStats.Where(m => m.Pokemonid == PokemonUnit.id),
                                       weight=PokemonUnit.weight
                                   });
                empty.Clear();
                var ActualPage = Index;
                var onePageOfPokemons = SavePokemon.ToPagedList(ActualPage, 10);
                var Convert = onePageOfPokemons.ToList<Pokemon>();

                foreach (var item in Convert)
                {
                    empty.Add(item);
                }

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

                empty.Clear();
                foreach (var item in SavePokemon)
                {
                    empty.Add(item);
                }

                return empty;



            }
        }
        public static ObservableCollection<Pokemon> SearchDBByName(ObservableCollection<Pokemon> empty)
        {
            using (var db = new PokeDataContext())
            {
                string mystring = "saur";

                var SaveLists = from TypeList in db.TypeList
                                join Types in db.Types
                                on TypeList.TypeID equals Types.id
                                select new PokemonType { id = TypeList.id, Pokemonid = TypeList.Pokemonid, type = Types, TypeID = Types.id };

                var SaveStats = from Stats in db.Stats
                                select new Stat { Pokemonid = Stats.Pokemonid, id = Stats.id, base_stat = Stats.base_stat };

                var SavePokemon = (from PokemonUnit in db.Pokemon
                                   join Sprites in db.Sprite
                                   on PokemonUnit.id equals Sprites.id
                                   where PokemonUnit.name.Contains(mystring)
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

                empty.Clear();
                foreach (var item in SavePokemon)
                {
                    empty.Add(item);
                }

                return empty;



            }
        }

        public static ObservableCollection<Pokemon> SearchDBByType(ObservableCollection<Pokemon> empty)
        {
            using (var db = new PokeDataContext())
            {
                string mystring = "fire";

                var SaveLists = from TypeList in db.TypeList
                                join Types in db.Types
                                on TypeList.TypeID equals Types.id
                                select new PokemonType { id = TypeList.id, Pokemonid = TypeList.Pokemonid, type = Types, TypeID = Types.id };

                var SaveStats = from Stats in db.Stats
                                select new Stat { Pokemonid = Stats.Pokemonid, id = Stats.id, base_stat = Stats.base_stat };

                var SavePokemon = (from PokemonUnit in db.Pokemon
                                   join Sprites in db.Sprite
                                   on PokemonUnit.id equals Sprites.id
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
                //Pode-se usar tanto name quanto url, url requer uma pequena alteração de paramatros, pois o tipo em si, só diferencia por uma número na string
                empty.Clear();
                var SelectByTypeName = (SavePokemon.Where(m => m.types.Any(u => u.type.name == mystring))).ToList<Pokemon>();
                foreach (var item in SelectByTypeName)
                {
                    empty.Add(item);
                }

                return empty;



            }
        }

        public static string resourceDBRead()
        {
            using (var db = new PokeDataContext())
            {
                var result = (from List in db.Listing
                             select new NamedAPIResourceList
                             {
                                 id = List.id,
                                 next = List.next
                             }).ToList();
                var LastResult = result.LastOrDefault();
                var endpoint = LastResult.next.ToString();
                var dummy = 1;

                return endpoint;

            }
        }

    }
}
