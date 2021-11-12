using Newtonsoft.Json;
using Pokedex.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex
{
    public class ApiRequest
    {
        public string Url { get; set; }
        private int actual = 0;




        public static async Task<Pokemon> FillPokedex(ObservableCollection<Pokemon> pokemon)
        {
            var Url = String.Format("https://pokeapi.co/api/v2/pokemon/");
            var jsonMessage = await CallPokedexAsync(Url);

            var serializer = new DataContractJsonSerializer(typeof(Pokemon));
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonMessage));

            var result = (Pokemon)serializer.ReadObject(ms);
            Console.Write(result);
            return result;
        }


        public static async Task<string> CallPokedexAsync(string url)
        {

            HttpClient http = new HttpClient();
            var response = await http.GetAsync(url);
            return await response.Content.ReadAsStringAsync();
        }
    }
}
