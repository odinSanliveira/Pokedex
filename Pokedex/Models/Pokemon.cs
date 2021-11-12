using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.Models
{
    public class Pokemon
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string NamePokemon { get; set; }
        [JsonProperty("url")]
        public Uri Link { get; set; }

    }
}
