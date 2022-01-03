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


namespace Pokedex
{
    public sealed partial class MainPage : Page
    {
        public ObservableCollection<NamedAPIResource> Pokedex { get; set; }
        public ObservableCollection<Pokemon> Pokemon { get; set; }
        public int Page { get; set; }
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
            Page = 1;

                DBOperation.ReadDB(Pokemon, Page);
                if(Pokemon.Count == 0)
                {
                    await ApiRequest.FillPokedexList(Pokemon, "https://pokeapi.co/api/v2/pokemon/?offset=0&limit=10");
                }

            if (ApiRequest.previous == null)
            {
               Previous.IsEnabled = false;
            }
            
            ProgressRing.IsActive = false;
            ProgressRing.Visibility = Visibility.Collapsed;
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
            Page--;
            ProgressRing.IsActive = true;
            ProgressRing.Visibility = Visibility.Visible;
            DBOperation.ReadDB(Pokemon,Page);
            if (Page == 1)
            {
                Previous.IsEnabled = false;
            }

            ProgressRing.IsActive = false;
            ProgressRing.Visibility = Visibility.Collapsed;

        }

        private async void Next_Click(object sender, RoutedEventArgs e)
        {
            Page++;
            ProgressRing.IsActive = true;
            ProgressRing.Visibility = Visibility.Visible;
            string PageAPIReference = DBOperation.resourceDBRead();


                DBOperation.ReadDB(Pokemon, Page);
                if (Pokemon.Count == 0)
                {
                    await ApiRequest.FillPokedexList(Pokemon, PageAPIReference);
                }
            

            if (Page == 1)
            {
                Previous.IsEnabled = false;
            }
            else if(PageAPIReference == null)
            {
                Previous.IsEnabled = false;
            }
            else
            {
                Previous.IsEnabled = true;
            }
            

            ProgressRing.IsActive = false;
            ProgressRing.Visibility = Visibility.Collapsed;

        }
    }
}
