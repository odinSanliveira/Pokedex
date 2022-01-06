using Pokedex.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

// O modelo de item de Página em Branco está documentado em https://go.microsoft.com/fwlink/?LinkId=234238

namespace Pokedex
{
    /// <summary>
    /// Uma página vazia que pode ser usada isoladamente ou navegada dentro de um Quadro.
    /// </summary>
    public sealed partial class MainPokedex : Page
    {



        public ObservableCollection<Pokemon> Pokemon { get; set; }
        public int Page { get; set; }
        public int TypePage { get; set; }
        public MainPokedex()
        {
            this.InitializeComponent();
            ApiRequest.InitializeClient();
            Pokemon = new ObservableCollection<Pokemon>();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ProgressRing.IsActive = true;
            ProgressRing.Visibility = Visibility.Visible;
            Page = 1;
            TypePage = 1;

            DBOperation.ReadDB(Pokemon, Page);
            if (Pokemon.Count == 0)
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
            ProgressRing.IsActive = true;
            ProgressRing.Visibility = Visibility.Visible;
            var comboBoxItem = Types.Items[Types.SelectedIndex] as ComboBoxItem;

            if (comboBoxItem.Content.ToString() == "all" || comboBoxItem.Content.ToString() == null)
            {
                Page--;
                DBOperation.ReadDB(Pokemon, Page);
            }
            else
            {
                TypePage--;
                var typeSelected = comboBoxItem.Content.ToString();
                DBOperation.SearchDBByType(Pokemon, typeSelected, TypePage);

            }


            if (Page == 1 && TypePage == 1)
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
            string PageAPIReference = DBOperation.resourceDBRead();
            string TypePageApiReference;
            //Definir lazyloading de tipos pela APIRequest


            var comboBoxItem = Types.Items[Types.SelectedIndex] as ComboBoxItem;

            if (comboBoxItem.Content.ToString() == "all" || comboBoxItem.Content.ToString() == null)
            {
                Page++;
                DBOperation.ReadDB(Pokemon, Page);
                if (Pokemon.Count == 0)
                {
                    await ApiRequest.FillPokedexList(Pokemon, PageAPIReference);
                }
            }
            else
            {
                TypePage++;
                var typeSelected = comboBoxItem.Content.ToString();
                DBOperation.SearchDBByType(Pokemon, typeSelected, TypePage);

            }

            if (Page == 1 && TypePage == 1)
            {
                Previous.IsEnabled = false;
            }
            else if (PageAPIReference == null)
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

        private void Types_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBoxItem = Types.Items[Types.SelectedIndex] as ComboBoxItem;
            if (comboBoxItem.Content.ToString() != "all")
            {
                var typeSelected = comboBoxItem.Content.ToString();
                DBOperation.SearchDBByType(Pokemon, typeSelected, TypePage);
            }else if(comboBoxItem.Content.ToString() == "all")
            {
                Page = 1;
                DBOperation.ReadDB(Pokemon, Page);
            }


        }
    }
}
