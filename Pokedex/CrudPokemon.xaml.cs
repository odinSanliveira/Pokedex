using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Pokedex.Models;
using System.Collections.ObjectModel;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// O modelo de item de Página em Branco está documentado em https://go.microsoft.com/fwlink/?LinkId=234238

namespace Pokedex
{
    /// <summary>
    /// Uma página vazia que pode ser usada isoladamente ou navegada dentro de um Quadro.
    /// </summary>
       
    public sealed partial class CrudPokemon : Page
    {
        public ObservableCollection<Pokemon> Pokemon { get; set; }
        public PokemonCRUD userPokemon { get; set; }

        public CrudPokemon()
        {
            this.InitializeComponent();
            Pokemon = new ObservableCollection<Pokemon>();
            userPokemon = new PokemonCRUD();
        }

        
        private void PokeListViewCrud_ItemClick(object sender, ItemClickEventArgs e)
        {
            
            

        }

        private void Register_click(object sender, RoutedEventArgs e) 
        {
            
            var PokeCrudName = PokeName.Text;
            var PokeCrudTypeOne = PokeTypeOne.Text;
            var PokeCrudTypeTwo = PokeTypeTwo.Text;
            var PokeCrudHP = PokeHp.Text;
            var PokeCrudATK = PokeAttack.Text;
            var PokeCrudDefense = PokeDefense.Text;
            var PokeCrudSpecialATK = PokeSpecialAttack.Text;
            var PokeCrudSpecialDefense = PokeSpecialDefense.Text;
            var PokeCrudSpeed = PokeSpeed.Text;
            var PokeCrudHeight = PokeHeight.Text;
            var PokeCrudWeight = PokeWeight.Text;


            if (PokeTypeTwo.Text == "")
            {
                TypeTwo.Text = "";
            }
            else
            {

                TypeTwo.Text = PokeTypeTwo.Text;
            }
            
            userPokemon.pokemonName = PokeCrudName;
            userPokemon.pokemonType = PokeCrudTypeOne;
            userPokemon.pokemonType2 = PokeCrudTypeTwo;
            userPokemon.HPCrud = int.Parse(PokeCrudHP);
            userPokemon.AttackCrud = int.Parse(PokeCrudATK);
            userPokemon.DefenseCrud = int.Parse(PokeCrudDefense);
            userPokemon.SpecialAttackCrud = int.Parse(PokeCrudSpecialATK);
            userPokemon.SpecialDefenseCrud = int.Parse(PokeCrudSpecialDefense);
            userPokemon.Speed = int.Parse(PokeCrudSpeed);
            userPokemon.heightCRUD = int.Parse(PokeCrudHeight);
            userPokemon.weightCRUD = int.Parse(PokeCrudWeight);

            var db = new PokeDataContext();
            db.UserPokemon.Add(this.userPokemon);
            db.SaveChanges();

        }
    }
}
