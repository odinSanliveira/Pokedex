using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.Models
{
    public class NamedAPIResource
    {
        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("url")]
        public string url { get; set; }

    }
}
