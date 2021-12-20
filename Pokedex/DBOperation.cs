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

                //var result1 = from TypeList in db.TypeList
                //              join Types in db.Types
                //              on TypeList.TypeID equals Types.id
                //              group Types by new NamedAPIResource
                //              {
                //                  id = Types.id,
                //                  name = Types.name,
                //                  url = Types.url
                //              }
                //              into g
                //              select g;
                //var result12 = from g

                //int asd = 0;
                //var result = (from PokemonUnit in db.Pokemon
                //              join TypeList in db.TypeList
                //              on PokemonUnit.id equals TypeList.Pokemonid
                //              join Types in db.Types
                //              on TypeList.TypeID equals Types.id
                //              group PokemonUnit by new
                //              {
                //                  id = PokemonUnit.id,
                //                  name = PokemonUnit.name,
                //                  sprites = PokemonUnit.sprites,
                //                  types = TypeList,
                //                  height = PokemonUnit.height,
                //                  weight = PokemonUnit.weight
                //              }
                //              into g
                //             select new Pokemon {
                //             id = g.Key.id,
                //             name = g.Key.name,
                //             sprites = g.Key.sprites,
                //             types = asd});.ToList();
                var resulds = result;
                             
                             

                //var result1 = from List in db.TypeList
                //              join Types in db.Types
                //              on List.TypeID equals Types.id
                //              group List by List.Pokemonid into 



                                  //var result = sb.Swithches_SW_PanalComponents.ToList();
                                  //var result = from List in db.TypeList
                                  //             join Types in db.Types
                                  //             on List.TypeID equals Types.id
                                  //             join PokemonUnit in db.Pokemon
                                  //             on List.Pokemonid equals PokemonUnit.id
                                  //             select new { List.id, Types.name };


                                  //var TypeSs = (from TypeList in db.TypeList
                                  //join Types in db.Types on TypeList.id equals Types.id).ToList();




















                                  //var nestedquery =

                                  //                  from TypeList in db.TypeList

                                  //                  join Types in db.Types on TypeList.id equals Types.id
                                  //                  join Pokey in db.Pokemon on TypeList.id equals Pokey.id
                                  //                  select new Pokemon
                                  //                  {
                                  //                      id = Pokey.id,
                                  //                      name = Pokey.name,
                                  //                      sprites = Pokey.sprites,
                                  //                      //types = Types,
                                  //                      height = Pokey.height,
                                  //                      weight = Pokey.weight
                                  //                  };
                                  //// var result = nestedquery.ToList();
                                  //ObservableCollection<Pokemon> x = new ObservableCollection<Pokemon>(nestedquery);


                                  //return x;



                                  //var query = from types in db.Types
                                  //            join typelist in db.TypeList on types.id equals typelist.id
                                  //            join pokey in db.Pokemon
                                  //            select new Pokemon() { id = pokey.id, name = }

                                  //ObservableCollection < Pokemon > x = new ObservableCollection<Pokemon>(EntryPoint);

                                  //return x;
            }

        }
    }
}
