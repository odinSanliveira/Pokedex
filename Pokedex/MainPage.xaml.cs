using Pokedex.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using static Pokedex.DBCreation;

// O modelo de item de Página em Branco está documentado em https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x416

namespace Pokedex
{
    /// <summary>
    /// Uma página vazia que pode ser usada isoladamente ou navegada dentro de um Quadro.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        //ApiRequest api = new ApiRequest();
        public ObservableCollection<NamedAPIResource> Pokedex { get; set; }
        public ObservableCollection<Pokemon> Pokemon { get; set; }
        public MainPage() 
        {
            this.InitializeComponent();
            ApiRequest.InitializeClient();
            Pokedex = new ObservableCollection<NamedAPIResource>();
            Pokemon = new ObservableCollection<Pokemon>();
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            SplitMenu.IsPaneOpen = !SplitMenu.IsPaneOpen;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ProgressRing.IsActive = true;
            ProgressRing.Visibility = Visibility.Visible;
            
            await ApiRequest.FillPokedexList(Pokemon);
           
            ProgressRing.IsActive = false;
            ProgressRing.Visibility = Visibility.Collapsed;
        }

        private void PokeListViewMain_ItemClick(object sender, ItemClickEventArgs e)
        {

            var selectedPokemon = (Pokemon)e.ClickedItem;

            PokemonName.Text = selectedPokemon.name;

            var pokemonImage = new BitmapImage();
            Uri url = new Uri(selectedPokemon.sprites.front_default, UriKind.Absolute);
            pokemonImage.UriSource = url;
            PokemonDetailImage.Source = pokemonImage;
            DBCreation.addRecord(selectedPokemon.id,selectedPokemon.name);




        }
    }
}
