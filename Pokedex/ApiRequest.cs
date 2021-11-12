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
        public static async Task<PokemonDataWrapper> GetPokemonsAsync()
        {

            HttpClient Client = new HttpClient();
            var responseMessage = await Client.GetAsync("https://pokeapi.co/api/v2/pokemon/");
            var jsonMessage = await responseMessage.Content.ReadAsStringAsync();

            var serializer = new DataContractJsonSerializer(typeof(PokemonDataWrapper));
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonMessage));

                var result = (PokemonDataWrapper)serializer.ReadObject(ms);
            return result;

            //var jsonResponse = await Client.GetStringAsync("https://pokeapi.co/api/v2/pokemon/");
            //var pokemonModel = JsonConvert.DeserializeObject<ObservableCollection<Pokemon>>(jsonResponse);
            //return pokemonModel;
            
        }


        public static async Task FillPokedexList(ObservableCollection<Pokemon> pokedex)
        {
            var pokemonData = await GetPokemonsAsync();

            var pokemons = pokemonData.data.listPokemon;

            /* here we could implement path for the attributes*/

            foreach(var pokemon in pokemons)
            {
                pokedex.Add(pokemon);
            }
        }
    }
}
