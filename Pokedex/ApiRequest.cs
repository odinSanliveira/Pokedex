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
        public static HttpClient apiClient { get; set; }
        public static Uri previous { get; set; }
        public static Uri next { get; set; }
        

        public static void InitializeClient()
        {
            apiClient = new HttpClient();
            apiClient.DefaultRequestHeaders.Accept.Clear();
            apiClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        }
        public static async Task<NamedAPIResourceList> getAPIAtribbute(string endpoint)
        {
            HttpClient Client = new HttpClient();
            var responseMessage = await Client.GetAsync(endpoint);
            var jsonMessage = await responseMessage.Content.ReadAsStringAsync();

            var serializer = new DataContractJsonSerializer(typeof(NamedAPIResourceList));
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonMessage));

            var result = (NamedAPIResourceList)serializer.ReadObject(ms);

            return result;
        }

        public static async Task<List<string>> getTypeList(string endpoint)
        {
            HttpClient Client = new HttpClient();
            var responseMessage = await Client.GetAsync(endpoint);
            var jsonMessage = await responseMessage.Content.ReadAsStringAsync();

            var serializer = new DataContractJsonSerializer(typeof(TypeFilterClass));
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonMessage));

            var result = (TypeFilterClass)serializer.ReadObject(ms);

            var result2 = result.pokemon;

            var resul3 = (from pokemon in result2
                         select string.Format(pokemon.pokemon.name)).ToList<String>();

            return resul3;
        }

        public static async Task<Pokemon> GetPokemonDetailByUrl(string url)
        {

            HttpClient Client = new HttpClient();

            var responseMessage = await Client.GetAsync(url);
            var jsonMessage = await responseMessage.Content.ReadAsStringAsync();

            var serializer = new DataContractJsonSerializer(typeof(Pokemon));
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonMessage));

            var detail = (Pokemon)serializer.ReadObject(ms);

            return detail;
            //return a Pokemon with all attributes required
        }

        public static async Task FillPokedexList(ObservableCollection<Pokemon> pokedex, string endpoint)
        {
            PokeDataContext nes = new PokeDataContext();
            try
            {
                var pokemonData = await getAPIAtribbute(endpoint);
                next = pokemonData.next;
                previous = pokemonData.previous;
                var pokemons = pokemonData.results;
                using (var db = new PokeDataContext())
                {
                    db.Listing.Add(pokemonData);
                    db.SaveChanges();
                    
                }
                /* here we could implement path for the attributes*/
                pokedex.Clear();

                foreach (var pokemon in pokemons)
                {

                    var pokemonDetail = await GetPokemonDetailByUrl(pokemon.url); //access when is clicked                    
                    pokedex.Add(pokemonDetail);
                    pokemonDetail.Save(pokemonDetail);
                }

            }
            catch (Exception exe)
            {
                return;
            }


        }
        public static async Task<PokemonType> GetPokemonType(string endpoint)
        {
            HttpClient Client = new HttpClient();

            var responseMessage = await Client.GetAsync(endpoint);
            var jsonMessage = await responseMessage.Content.ReadAsStringAsync();

            var serializer = new DataContractJsonSerializer(typeof(PokemonType));
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonMessage));

            var detail = (PokemonType)serializer.ReadObject(ms);

            return detail;
        }


    }
}
