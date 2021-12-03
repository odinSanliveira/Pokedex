using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.Models
{
    public class Pokemon
    {
        //@ConvertCollumn = id_Pokemon
        
        public int id { get; set; }
        public string name { get; set; }
        public PokemonSprites sprites { get; set; }
        
        public List<PokemonType> types { get; set; }

        public int height { get; set; }
        public int weight { get; set; }
        public List<Stat> stats { get; set; }

        public string TypeGetter()
        {
            List<string> typePoke = new List<string>();
            string typeUniqueList = string.Empty;
            foreach (var item in types)
            {
                typeUniqueList += item.type.name + "\n";
                typePoke.Add(item.type.name);
            }
            return typeUniqueList;
        }



    }
}
