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
        public int ItemSelected { get; set; }

        public BitmapImage SelectedPokemonImage { get; set; }

        public CrudPokemon()
        {
            this.InitializeComponent();
            SelectedPokemonImage = new BitmapImage();   
            Pokemon = new ObservableCollection<PokemonCRUD>();
            userPokemon = new PokemonCRUD();
            DBOperation.ReadCRUDB(Pokemon);
        }

        
        private void PokeListViewCrud_ItemClick(object sender, ItemClickEventArgs e)
        {
            var selectedPokemon = (PokemonCRUD)e.ClickedItem;
            ItemSelected = selectedPokemon.id;

            PokemonName.Text = selectedPokemon.pokemonName;
            TypeOne.Text = selectedPokemon.pokemonType;
            TypeBlock.Text = "Type";
            HpBlock.Text = "HP";
            Hp.Text = selectedPokemon.HPCrud.ToString();
            AttackBlock.Text = "Attack";
            Attack.Text = selectedPokemon.AttackCrud.ToString();
            DefenseBlock.Text = "Defense";
            Defense.Text = selectedPokemon.DefenseCrud.ToString();
            SpecialAttackBlock.Text = "Special Attack";
            SpecialAttack.Text = selectedPokemon.SpecialAttackCrud.ToString();
            SpecialDefenseBlock.Text = "Special Defense";
            SpecialDefense.Text = selectedPokemon.SpecialDefenseCrud.ToString();
            SpeedBlock.Text = "Speed";
            Speed.Text = selectedPokemon.Speed.ToString();
            HeightBlock.Text = "Height";
            Height.Text = selectedPokemon.heightCRUD.ToString();
            WeightBlock.Text = "Weight";
            Weight.Text = selectedPokemon.weightCRUD.ToString();

            //botão precisa ser mostrado quando selecionado
            Delete_button.Visibility = Visibility.Visible;

            if (selectedPokemon.pokemonType2 == "")
            {
                TypeTwo.Text = "";
            }
            else
            {

                TypeTwo.Text = selectedPokemon.pokemonType2;
            }
           
            Uri url = new Uri(selectedPokemon.sprite, UriKind.Absolute);
            SelectedPokemonImage.UriSource = url;
            pokemondetailimage.Source = SelectedPokemonImage;

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
            var db = new PokeDataContext();
            db.UserPokemon.Add(this.userPokemon);
            db.SaveChanges();
            DBOperation.ReadCRUDB(Pokemon);
            userPokemon = new PokemonCRUD();

        }

        private void Delete_Pokemon(object sender, RoutedEventArgs e)
        {            

            foreach (UIElement element in StatsGrid.Children)
            {
                TextBlock textblock = element as TextBlock;

                if (textblock != null)
                {
                    textblock.Text = "";
                }
            }
            foreach (UIElement element in DetailHeadGrid.Children)
            {
                TextBlock textblock = element as TextBlock;
               
                if (textblock != null)
                {
                    textblock.Text = "";
                }
                
            }
            SelectedPokemonImage.UriSource = null;
            var id = ItemSelected;
            DBOperation.DeletePokemonCrud(id);
            DBOperation.ReadCRUDB(Pokemon);

            //botão se esconde quando deleta um pokémon
            Delete_button.Visibility = Visibility.Collapsed;

        }

        private void Clear_Boxes(object sender, RoutedEventArgs e)
        {
            foreach (UIElement element in AddingNewPokemon.Children)
            {
                TextBox textbox = element as TextBox;
                if (textbox != null)
                {
                    textbox.Text = String.Empty;
                }
            }
        }
            private void Update_Pokemon(object sender, RoutedEventArgs e)
        {
            var id = ItemSelected;
            DBOperation.AlterPokemonCrud(userPokemon, id);
            DBOperation.ReadCRUDB(Pokemon);
            
        }
        private void TextBox_OnBeforeTextChanging(TextBox sender,
                                          TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !char.IsDigit(c));
        }
        private void NumberBox_OnBeforeTextChanging(TextBox sender,
                                          TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !char.IsLetter(c));
        }
    }
}
