using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    namespace Pokedex.Models
    {
        //public class Pokemon
        //{

        //    public int Id { get; set; }
        //    public string NamePokemon { get; set; }
        //    public Uri Link { get; set; }

        //}
        public class Pokemon
        {
            public int Id { get; set; }

            [JsonProperty("name")]
            public string NamePokemon { get; set; }
            //[JsonProperty("sprites")]
            //public Sprites Sprites { get; set; }

            [JsonProperty("id")]
            public int Id { get; set; }
        }
        
        public class Sprites
        {
            public string back_default { get; set; }
            public object back_female { get; set; }
            public string back_shiny { get; set; }
            public object back_shiny_female { get; set; }
            public string front_default { get; set; }
            public object front_female { get; set; }
            public string front_shiny { get; set; }
            public object front_shiny_female { get; set; }
            //public Versions versions { get; set; }
        }
        public class Icons
        {
            public string front_default { get; set; }
            public object front_female { get; set; }
        }
        public class Form
        {
            public string name { get; set; }
            public string url { get; set; }
        }

        public class Ability
        {
            public Ability ability { get; set; }
            public bool is_hidden { get; set; }
            public int slot { get; set; }
        }
        public class GameIndice
        {
            public int game_index { get; set; }
            public Version version { get; set; }
        }
        public class Move
        {
            public Move move { get; set; }
            //public List<VersionGroupDetail> version_group_details { get; set; }
        }

        public class PokemonDataWrapper
        {
            public PokedexList data { get; set; }
        }
    }
