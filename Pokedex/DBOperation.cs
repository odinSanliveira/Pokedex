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
        ObservableCollection<Pokemon> pokedex;
        public static void ReadDB(ObservableCollection<Pokemon> empty)
        {
            using (var db = new PokeDataContext()) {

                ObservableCollection<Pokemon> full = new ObservableCollection<Pokemon>(db.Pokemon);
                foreach (var pokemon in full)
                {                  
                    empty.Add(pokemon);
                }
            }
            
        }
    }
}
