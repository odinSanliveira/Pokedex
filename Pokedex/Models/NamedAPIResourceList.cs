using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    namespace Pokedex.Models
    {        
        public class NamedAPIResourceList
        {
            [JsonProperty("count")]
            public int count { get; set; }

            [JsonProperty("next")]
            public Uri next { get; set; }

            [JsonProperty("previous")]
            public Uri previous { get; set; }

            [JsonProperty("results")]
            public List<NamedAPIResource> results { get; set; }

            
        }
    }
