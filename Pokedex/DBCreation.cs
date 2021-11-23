using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Pokedex
{
    class DBCreation
    {
        public DBCreation() { }

        public async static void InitDB()
        {
            await ApplicationData.Current.LocalFolder.CreateFileAsync("storedPokemon3.db", CreationCollisionOption.OpenIfExists);
            string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "storedPokemon3.db");

            using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
            {
                con.Open();
                string initCMD = "CREATE TABLE IF  NOT EXISTS " +
                    "Pokemon(id INTEGER NOT NULL UNIQUE," +
                    "name TEXT NOT NULL," +
                    "sprites TEXT NOT NULL)";

                SqliteCommand CMDcreateTable = new SqliteCommand(initCMD, con);
                CMDcreateTable.ExecuteReader();
                con.Close();
            }
        }

        public static void addRecord(int id, String name, String sprites)
        {
            string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "storedPokemon3.db");

            using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
            {
                con.Open();
                SqliteCommand CMD_Insert = new SqliteCommand();
                CMD_Insert.Connection = con;

                CMD_Insert.CommandText = "INSERT OR REPLACE INTO Pokemon VALUES(@idPoke, @nameP, @sprites);";
                CMD_Insert.Parameters.AddWithValue("@idPoke", id);
                CMD_Insert.Parameters.AddWithValue("@nameP", name);
                CMD_Insert.Parameters.AddWithValue("@sprites", sprites);

                CMD_Insert.ExecuteReader();

                con.Close();
            }
        }

        public class storedPokeData
        {
            public int idPokemon { get; set; }
            public String namePokemon { get; set; }
            public String spritesPokemon { get; set; }
            //TODO:Achar como usar o reader com o datatype blob
            public storedPokeData(int id, String name, String sprites)
            {
                idPokemon = id;
                namePokemon = name;
                spritesPokemon = sprites;
            }
        }

        public static ObservableCollection<storedPokeData> GetStoredPokeData()
        {
            ObservableCollection<storedPokeData> pokeList = new ObservableCollection<storedPokeData>();
            string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "storedPokemon3.db");

            using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
            {
                con.Open();

                String selectCmd = "SELECT id, name, sprites FROM Pokemon";
                SqliteCommand cmd_getRec = new SqliteCommand(selectCmd, con);

                SqliteDataReader reader = cmd_getRec.ExecuteReader();

                while (reader.Read())
                {
                    int teste = int.Parse(reader.GetString(0));
                    pokeList.Add(new storedPokeData(teste, reader.GetString(1),reader.GetString(2)));
                }

                con.Close();
            }

            return pokeList;
        }



    }
}
