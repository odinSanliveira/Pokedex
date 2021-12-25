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

            //await ApiRequest.FillPokedexList(Pokemon, "https://pokeapi.co/api/v2/pokemon/?offset={0}&limit={1}");
            //if(ApiRequest.previous == null)
            //{
            //   Previous.IsEnabled = false;
            //}

            ProgressRing.IsActive = false;
            ProgressRing.Visibility = Visibility.Collapsed;
            //DBOperation.ReadDB(Pokemon);
            //DBOperation.SearchDBByID(Pokemon);
            //DBOperation.SearchDBByType(Pokemon);
            //DBOperation.SearchDBByName(Pokemon);
        }

        private void PokeListViewMain_ItemClick(object sender, ItemClickEventArgs e)
        {

            var selectedPokemon = (Pokemon)e.ClickedItem;

            PokemonName.Text = selectedPokemon.name;
            TypeOne.Text = selectedPokemon.types[0].type.name;
            TypeBlock.Text = "Type";
            HpBlock.Text = "HP";
            Hp.Text = selectedPokemon.stats[0].base_stat.ToString();
            AttackBlock.Text = "Attack";
            Attack.Text = selectedPokemon.stats[1].base_stat.ToString();
            DefenseBlock.Text = "Defense";
            Defense.Text = selectedPokemon.stats[2].base_stat.ToString();
            SpecialAttackBlock.Text = "Special Attack";
            SpecialAttack.Text = selectedPokemon.stats[3].base_stat.ToString();
            SpecialDefenseBlock.Text = "Special Defense";
            SpecialDefense.Text = selectedPokemon.stats[4].base_stat.ToString();
            SpeedBlock.Text = "Speed";
            Speed.Text = selectedPokemon.stats[5].base_stat.ToString();
            HeightBlock.Text = "Height";
            Height.Text = selectedPokemon.height.ToString();
            WeightBlock.Text = "Weight";
            Weight.Text = selectedPokemon.weight.ToString();

            if (selectedPokemon.types.Count > 1)
            {
                TypeTwo.Text = selectedPokemon.types[1].type.name;
            }
            else
            {
                TypeTwo.Text = "";
            }
           
            
            var pokemonImage = new BitmapImage();
            Uri url = new Uri(selectedPokemon.sprites.front_default, UriKind.Absolute);
            pokemonImage.UriSource = url;
            PokemonDetailImage.Source = pokemonImage;




        }

        private async void Previous_Click(object sender, RoutedEventArgs e)
        {
            ProgressRing.IsActive = true;
            ProgressRing.Visibility = Visibility.Visible;
            await ApiRequest.FillPokedexList(Pokemon, ApiRequest.previous.ToString());
            if (ApiRequest.previous == null)
            {
                Previous.IsEnabled = false;
            }

            ProgressRing.IsActive = false;
            ProgressRing.Visibility = Visibility.Collapsed;

        }

        private async void Next_Click(object sender, RoutedEventArgs e)
        {
            ProgressRing.IsActive = true;
            ProgressRing.Visibility = Visibility.Visible;

            await ApiRequest.FillPokedexList(Pokemon, ApiRequest.next.ToString());
            if (ApiRequest.previous != null)
            {
                Previous.IsEnabled = true;
            }else if(ApiRequest.next == null)
            {
                Previous.IsEnabled = false;
            }
            

            ProgressRing.IsActive = false;
            ProgressRing.Visibility = Visibility.Collapsed;
            //DBOperation.ReadDB();

        }
    }
}
