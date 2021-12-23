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
        //var poke = ObservableCollection<Pokemon> pokedex;
        //SELECT Pokemon.id, Pokemon.name, spritesfront_default, Types.name, url FROM Types, Pokemon LEFT  OUTER JOIN  TypeList ON Types.id = TypeList.typeid
        public static void ReadDB()
        {
            using (var db = new PokeDataContext()) {

                var result = from TypeList in db.TypeList
                             join Types in db.Types
                             on TypeList.TypeID equals Types.id
                             select new PokemonType { id = TypeList.id, Pokemonid = TypeList.Pokemonid, type = Types, TypeID = Types.id };
                var result12 = result.ToList<PokemonType>();



                var result34 = from Stats in db.Stats
                               select new Stat { Pokemonid = Stats.Pokemonid, id = Stats.id, base_stat = Stats.base_stat };


                var result2 = (from PokemonUnit in db.Pokemon
                              join Sprites in db.Sprite
                              on PokemonUnit.id equals Sprites.id
                              select new Pokemon { id = PokemonUnit.id, 
                                  name = PokemonUnit.name, 
                                  sprites = Sprites,
                                  height=PokemonUnit.height,
                                  types = (List<PokemonType>)result.Where(m => m.Pokemonid == PokemonUnit.id),
                                  stats = (List<Stat>)result34.Where(m => m.Pokemonid == PokemonUnit.id),
                                  weight=PokemonUnit.weight }).ToList<Pokemon>();


                ObservableCollection<Pokemon> storedPokemonList = new ObservableCollection<Pokemon>(result2);
                
               
            }

        }
    }
}
