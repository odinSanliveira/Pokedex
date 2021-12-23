using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pokedex.Models
{
    public class NamedAPIResource
    {
        
        public int id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        
    }
}

