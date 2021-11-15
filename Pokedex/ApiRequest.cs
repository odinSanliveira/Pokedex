using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
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

        public static HttpClient apiClient { get; set; }

        public static void InitializeClient()
        {
            apiClient = new HttpClient();
            apiClient.DefaultRequestHeaders.Accept.Clear();
            apiClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        }

        




        //public static async Task<Pokemon> FillPokedex(ObservableCollection<Pokemon> pokemon)
        //{
        //    var Url = String.Format("https://pokeapi.co/api/v2/pokemon/");
        //    var jsonMessage = await CallPokedexAsync(Url);

        //    var serializer = new DataContractJsonSerializer(typeof(Pokemon));
        //    var ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonMessage));

        //    var result = (Pokemon)serializer.ReadObject(ms);
        //    Console.Write(result);
        //    return result;
        //}
        public static async Task<NamedAPIResourceList> GetPokemonsAsync()
        {

            HttpClient Client = new HttpClient();
            int offset = 0;
            int limit = 15;
            string url = String.Format("https://pokeapi.co/api/v2/pokemon/?offset={0}&limit={1}", offset, limit);
            var responseMessage = await Client.GetAsync(url);
            var jsonMessage = await responseMessage.Content.ReadAsStringAsync();

            var serializer = new DataContractJsonSerializer(typeof(NamedAPIResourceList));
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonMessage));
                
            var result = (NamedAPIResourceList)serializer.ReadObject(ms);
            return result;
            
            

            
        }


        public static async Task FillPokedexList(ObservableCollection<NamedAPIResource> pokedex)
        {
            var pokemonData = await GetPokemonsAsync();

            var pokemons = pokemonData.results;

            /* here we could implement path for the attributes*/

            foreach(var pokemon in pokemons)
            {   
                pokedex.Add(pokemon);
            }
        }
    }
}
