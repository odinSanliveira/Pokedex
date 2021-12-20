using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public virtual PokemonSprites sprites { get; set; }
        public virtual List<PokemonType> types { get; set; }

        public int height { get; set; }
        public int weight { get; set; }
        
        public virtual List<Stat> stats { get; set; }
        
    

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

        public void Save(Pokemon pokemon)
        {
            var db = new PokeDataContext();
            db.Pokemon.Add(this);
            db.SaveChanges();

        }


    }
}
