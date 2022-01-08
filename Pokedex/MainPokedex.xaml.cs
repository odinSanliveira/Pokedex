using PagedList;
using Pokedex.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;


namespace Pokedex
{
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

            if (comboBoxItem.Content.ToString() == "select" || comboBoxItem.Content.ToString() == "all")
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

            var comboBoxItem = Types.Items[Types.SelectedIndex] as ComboBoxItem;


            if (comboBoxItem.Content.ToString() == "select" || comboBoxItem.Content.ToString() == "all")
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
                List<string> pokemonsInDB = new List<string>();
                TypePage++;

                var typeSelected = comboBoxItem.Content.ToString();

                var result = await ApiRequest.getTypeList("https://pokeapi.co/api/v2/type/"+typeSelected);
                DBOperation.SearchDBByType(Pokemon, typeSelected, TypePage);
                var currentPagePokemons = result.ToPagedList(TypePage, 10);

                foreach (var item in Pokemon)
                {
                    pokemonsInDB.Add(item.name);
                }
                var filtering = currentPagePokemons.Except(pokemonsInDB);
                foreach (var notLoadeadPoke in filtering)
                {
                    var url = "https://pokeapi.co/api/v2/pokemon/"+notLoadeadPoke;
                    var notLoadedPokeDetails = await ApiRequest.GetPokemonDetailByUrl(url);
                    Pokemon.Add(notLoadedPokeDetails);

                    using (var db = new PokeDataContext())
                    {
                        if (!db.Pokemon.Any(u => u.id == notLoadedPokeDetails.id))
                        {
                            db.Pokemon.Add(notLoadedPokeDetails);
                            db.SaveChanges();
                        }
                    }

                }

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

        private async void Types_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBoxItem = Types.Items[Types.SelectedIndex] as ComboBoxItem;

            if (comboBoxItem.Content.ToString() != "select")
            {
                if (comboBoxItem.Content.ToString() == "all")
                {
                    Page = 1;
                    TypePage = 1;

                    DBOperation.ReadDB(Pokemon, Page);
                }
                else
                {
                    List<string> pokemonsInDB = new List<string>();
                    Page=1;
                    TypePage=1;

                    var typeSelected = comboBoxItem.Content.ToString();

                    var result = await ApiRequest.getTypeList("https://pokeapi.co/api/v2/type/"+typeSelected);
                    DBOperation.SearchDBByType(Pokemon, typeSelected, TypePage);
                    var currentPagePokemons = result.ToPagedList(TypePage, 10);

                    foreach (var item in Pokemon)
                    {
                        pokemonsInDB.Add(item.name);
                    }
                    var filtering = currentPagePokemons.Except(pokemonsInDB);
                    foreach (var notLoadeadPoke in filtering)
                    {
                        var url = "https://pokeapi.co/api/v2/pokemon/"+notLoadeadPoke;
                        var notLoadedPokeDetails = await ApiRequest.GetPokemonDetailByUrl(url);
                        Pokemon.Add(notLoadedPokeDetails);

                        using (var db = new PokeDataContext())
                        {
                            if (!db.Pokemon.Any(u => u.id == notLoadedPokeDetails.id))
                            {
                                db.Pokemon.Add(notLoadedPokeDetails);
                                db.SaveChanges();
                            }
                        }

                    }


                }
            }

        }
        private async void AutoSuggest_TextFind(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            //pesquisa por nome
            if (RadioNome.IsChecked == true)
            {
                if (sender.Text.Length > 1 && sender.Text.Any(c => !char.IsDigit(c)))
                {
                    var nameRequested = sender.Text;
                    DBOperation.SearchDBByName(Pokemon, nameRequested);
                    if (Pokemon.Count == 0)
                    {
                        var searchAPIurl = "https://pokeapi.co/api/v2/pokemon/"+nameRequested;
                        var searchRequest = await ApiRequest.GetPokemonDetailByUrl(searchAPIurl);
                        Pokemon.Clear();
                        Pokemon.Add(searchRequest);
                    }
                }
                else
                {
                    sender.ItemsSource = new String[] { "No suggestions..." };
                    Page = 1;
                    DBOperation.ReadDB(Pokemon, Page);
                }
            }


            else
            {
                //pesquisa por ID

                if (sender.Text.Length > 1 && sender.Text.Any(c => !char.IsLetter(c)))
                {
                    var idRequested = int.Parse(sender.Text);
                    DBOperation.SearchDBByID(Pokemon, idRequested);
                    if (Pokemon.Count == 0)
                    {
                        var searchAPIurl = "https://pokeapi.co/api/v2/pokemon/"+idRequested;
                        var searchRequest = await ApiRequest.GetPokemonDetailByUrl(searchAPIurl);
                        Pokemon.Clear();
                        Pokemon.Add(searchRequest);
                    }
                }
                else
                {
                    sender.ItemsSource = new String[] { "No suggestions..." };
                    Page = 1;
                    DBOperation.ReadDB(Pokemon, Page);
                }
            }
        }
    }
}
