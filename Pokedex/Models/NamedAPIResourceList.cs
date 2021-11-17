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
           
            public int count { get; set; }

            
            public Uri next { get; set; }

            
            public Uri previous { get; set; }

           
            public List<NamedAPIResource> results { get; set; }

            
        }
    }
