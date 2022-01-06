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
using Windows.UI.Xaml.Media.Imaging;

// O modelo de item de Página em Branco está documentado em https://go.microsoft.com/fwlink/?LinkId=234238

namespace Pokedex
{
    /// <summary>
    /// Uma página vazia que pode ser usada isoladamente ou navegada dentro de um Quadro.
    /// </summary>
       
    public sealed partial class CrudPokemon : Page
    {
        public ObservableCollection<PokemonCRUD> Pokemon { get; set; }
        public PokemonCRUD userPokemon { get; set; }

        public CrudPokemon()
        {
            this.InitializeComponent();
            Pokemon = new ObservableCollection<PokemonCRUD>();
            userPokemon = new PokemonCRUD();
        }

        
        private void PokeListViewCrud_ItemClick(object sender, ItemClickEventArgs e)
        {
            
            

        }

        private void Register_click(object sender, RoutedEventArgs e) 
        {


            userPokemon.pokemonName = PokeName.Text;
            userPokemon.pokemonType = PokeTypeOne.Text;
            userPokemon.pokemonType2 = PokeTypeTwo.Text;
            userPokemon.sprite = "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/"+PokeIdSprite.Text+".png";
            userPokemon.HPCrud = int.Parse(PokeHp.Text);
            userPokemon.AttackCrud = int.Parse(PokeAttack.Text);
            userPokemon.DefenseCrud = int.Parse(PokeDefense.Text);
            userPokemon.SpecialAttackCrud = int.Parse(PokeSpecialAttack.Text);
            userPokemon.SpecialDefenseCrud = int.Parse(PokeSpecialDefense.Text);
            userPokemon.Speed = int.Parse(PokeSpeed.Text);
            userPokemon.heightCRUD = int.Parse(PokeHeight.Text);
            userPokemon.weightCRUD = int.Parse(PokeWeight.Text);

            var pokemonImage = new BitmapImage();
            Uri url = new Uri(userPokemon.sprite, UriKind.Absolute);
            pokemonImage.UriSource = url;
            pokemondetailimage.Source = pokemonImage;

            if (PokeTypeTwo.Text == "")
            {
                TypeTwo.Text = "";
            }
            else
            {

                TypeTwo.Text = PokeTypeTwo.Text;
            }

            var db = new PokeDataContext();
            db.UserPokemon.Add(this.userPokemon);
            db.SaveChanges();
            DBOperation.ReadCRUDB(Pokemon);

        }
    }
}
