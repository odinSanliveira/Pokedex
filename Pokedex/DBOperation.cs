using PagedList;
using Pokedex.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Pokedex
{
    public class DBOperation
    {

        public static ObservableCollection<Pokemon> ReadDB(ObservableCollection<Pokemon> empty, int Index)
        {
            using (var db = new PokeDataContext())
            {

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

        public static ObservableCollection<Pokemon> SearchDBByID(ObservableCollection<Pokemon> empty, int Index)
        {
            using (var db = new PokeDataContext())
            {

                var SaveLists = from TypeList in db.TypeList
                                join Types in db.Types
                                on TypeList.TypeID equals Types.id
                                select new PokemonType { id = TypeList.id, Pokemonid = TypeList.Pokemonid, type = Types, TypeID = Types.id };

                var SaveStats = from Stats in db.Stats
                                select new Stat { Pokemonid = Stats.Pokemonid, id = Stats.id, base_stat = Stats.base_stat };

                var SavePokemon = (from PokemonUnit in db.Pokemon
                                   join Sprites in db.Sprite
                                   on PokemonUnit.id equals Sprites.id
                                   where PokemonUnit.id.Equals(Index)
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
        public static ObservableCollection<Pokemon> SearchDBByName(ObservableCollection<Pokemon> empty, string nameRequested)
        {
            using (var db = new PokeDataContext())
            {

                var SaveLists = from TypeList in db.TypeList
                                join Types in db.Types
                                on TypeList.TypeID equals Types.id
                                select new PokemonType { id = TypeList.id, Pokemonid = TypeList.Pokemonid, type = Types, TypeID = Types.id };

                var SaveStats = from Stats in db.Stats
                                select new Stat { Pokemonid = Stats.Pokemonid, id = Stats.id, base_stat = Stats.base_stat };

                var SavePokemon = (from PokemonUnit in db.Pokemon
                                   join Sprites in db.Sprite
                                   on PokemonUnit.id equals Sprites.id
                                   where PokemonUnit.name.Contains(nameRequested)
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

        public static ObservableCollection<Pokemon> SearchDBByType(ObservableCollection<Pokemon> empty, string typeSelected, int TypeIndex)
        {
            using (var db = new PokeDataContext())
            {
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
                                   }).ToList<Pokemon>();

                //Pode-se usar tanto name quanto url, url requer uma pequena alteração de paramatros, pois o tipo em si, só diferencia por uma número na string
                var SelectByTypeName = (SavePokemon.Where(m => m.types.Any(u => u.type.name == typeSelected))).ToList<Pokemon>();
                empty.Clear();

                var ActualPage = TypeIndex;
                var onePageOfPokemons = SelectByTypeName.ToPagedList(ActualPage, 10);
                var Convert = onePageOfPokemons.ToList<Pokemon>();

                foreach (var item in Convert)
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


        //Operations on CRUD

        public static ObservableCollection<PokemonCRUD> ReadCRUDB(ObservableCollection<PokemonCRUD> empty)
        {
            using (var db = new PokeDataContext())
            {
                var result = (from UserPokemon in db.UserPokemon
                              select new PokemonCRUD
                              {
                                  id = UserPokemon.id,
                                  pokemonName = UserPokemon.pokemonName,
                                  pokemonType = UserPokemon.pokemonType,
                                  pokemonType2 = UserPokemon.pokemonType2,
                                  sprite = UserPokemon.sprite,
                                  HPCrud = UserPokemon.HPCrud,
                                  heightCRUD = UserPokemon.heightCRUD,
                                  weightCRUD = UserPokemon.weightCRUD,
                                  AttackCrud = UserPokemon.AttackCrud,
                                  DefenseCrud = UserPokemon.DefenseCrud,
                                  SpecialDefenseCrud = UserPokemon.SpecialDefenseCrud,
                                  SpecialAttackCrud = UserPokemon.SpecialAttackCrud,
                                  Speed = UserPokemon.Speed
                              }).ToList<PokemonCRUD>();
                empty.Clear();
                foreach (var item in result)
                {
                    empty.Add(item);
                }

                return empty;
            }
        }

        public static void DeletePokemonCrud(int Index)
        {
            using (var db = new PokeDataContext())
            {
                var result = (from UserPokemon in db.UserPokemon
                              where UserPokemon.id == Index
                              select new PokemonCRUD
                              {
                                  id = UserPokemon.id,
                                  pokemonName = UserPokemon.pokemonName,
                                  pokemonType = UserPokemon.pokemonType,
                                  pokemonType2 = UserPokemon.pokemonType2,
                                  sprite = UserPokemon.sprite,
                                  HPCrud = UserPokemon.HPCrud,
                                  heightCRUD = UserPokemon.heightCRUD,
                                  weightCRUD = UserPokemon.weightCRUD,
                                  AttackCrud = UserPokemon.AttackCrud,
                                  DefenseCrud = UserPokemon.DefenseCrud,
                                  SpecialDefenseCrud = UserPokemon.SpecialDefenseCrud,
                                  SpecialAttackCrud = UserPokemon.SpecialAttackCrud,
                                  Speed = UserPokemon.Speed
                              }).ToList<PokemonCRUD>();
                foreach (var item in result)
                {
                    db.UserPokemon.Remove(item);
                    db.SaveChanges();
                }
            }
        }

        public static void AlterPokemonCrud(PokemonCRUD alteredPokemon, int Index)
        {
            using (var db = new PokeDataContext())
            {
                var result = (from UserPokemon in db.UserPokemon
                              where UserPokemon.id == Index
                              select UserPokemon).ToList<PokemonCRUD>();
                foreach (var item in result)
                {

                    item.AttackCrud = alteredPokemon.AttackCrud;
                    item.DefenseCrud = alteredPokemon.DefenseCrud;
                    item.HPCrud = alteredPokemon.HPCrud;
                    item.pokemonName = alteredPokemon.pokemonName;
                    item.pokemonType = alteredPokemon.pokemonType;
                    item.pokemonType2 = alteredPokemon.pokemonType2;
                    item.SpecialAttackCrud = alteredPokemon.SpecialAttackCrud;
                    item.SpecialDefenseCrud= alteredPokemon.SpecialDefenseCrud;
                    item.Speed = alteredPokemon.Speed;
                    item.weightCRUD = alteredPokemon.weightCRUD;
                    item.heightCRUD = alteredPokemon.heightCRUD;
                }

            }
        }
    }
}
