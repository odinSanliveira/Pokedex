using Pokedex.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace Pokedex
{
    public sealed partial class CrudPokemon : Page
    {
        public ObservableCollection<PokemonCRUD> Pokemon { get; set; }
        public PokemonCRUD userPokemon { get; set; }
        public PokemonCRUD toEditPokemon { get; set; }
        public int ItemSelected { get; set; }

        public BitmapImage SelectedPokemonImage { get; set; }

        public CrudPokemon()
        {
            this.InitializeComponent();
            SelectedPokemonImage = new BitmapImage();
            Pokemon = new ObservableCollection<PokemonCRUD>();
            userPokemon = new PokemonCRUD();
            DBOperation.ReadCRUDB(Pokemon);
            //botão alterar inativo inicialmente
            UpdateButton.IsEnabled = false;
            ClearButton.IsEnabled = false;
        }


        private void PokeListViewCrud_ItemClick(object sender, ItemClickEventArgs e)
        {
            var selectedPokemon = (PokemonCRUD)e.ClickedItem;
            ItemSelected = selectedPokemon.id;

            PokemonName.Text = selectedPokemon.pokemonName;
            TypeBlock.Text = "Type";
            TypeOne.Text = selectedPokemon.pokemonType;
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
            EditFormButton.Visibility = Visibility.Visible;

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
            if (int.Parse(PokeIdSprite.Text) > 898)
            {
                PokeIdSprite.Text = "898";
            }
            userPokemon.sprite = "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/" + PokeIdSprite.Text + ".png";
            userPokemon.HPCrud = int.Parse(PokeHp.Text);
            userPokemon.AttackCrud = int.Parse(PokeAttack.Text);
            userPokemon.DefenseCrud = int.Parse(PokeDefense.Text);
            userPokemon.SpecialAttackCrud = int.Parse(PokeSpecialAttack.Text);
            userPokemon.SpecialDefenseCrud = int.Parse(PokeSpecialDefense.Text);
            userPokemon.Speed = int.Parse(PokeSpeed.Text);
            userPokemon.heightCRUD = int.Parse(PokeHeight.Text);
            userPokemon.weightCRUD = int.Parse(PokeWeight.Text);

            var isNull = userPokemon.GetType().GetProperties()
              .Where(m => m.PropertyType == typeof(string))
              .Select(m => (string)m.GetValue(userPokemon))
              .Any(value => string.IsNullOrEmpty(value));

            var db = new PokeDataContext();
            if (!db.UserPokemon.Any(u => u.id == userPokemon.id) && !isNull)
            {
                db.UserPokemon.Add(this.userPokemon);
                db.SaveChanges();
            }
            DBOperation.ReadCRUDB(Pokemon);
            ClearBoxes(AddingNewPokemon);
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

            //botões se escondem quando deleta um pokémon
            Delete_button.Visibility = Visibility.Collapsed;
            EditFormButton.Visibility = Visibility.Collapsed;

        }

        private void ClearForm(object sender, RoutedEventArgs e)
        {
            ClearBoxes(AddingNewPokemon);
            SaveButton.IsEnabled = true;
            UpdateButton.IsEnabled = false;
            ClearButton.IsEnabled = false;
        }

        public void ClearBoxes(Grid GridName)
        {
            foreach (UIElement element in GridName.Children)
            {
                TextBox textbox = element as TextBox;
                if (textbox != null)
                {
                    textbox.Text = String.Empty;
                }
            }
        }
        private void UpdatePokemon(object sender, RoutedEventArgs e)
        {
            var isNull = userPokemon.GetType().GetProperties()
              .Where(m => m.PropertyType == typeof(string))
              .Select(m => (string)m.GetValue(userPokemon))
              .Any(value => string.IsNullOrEmpty(value));

            if (!isNull)
            {
                var id = ItemSelected;
            userPokemon.pokemonName = PokeName.Text;
            userPokemon.pokemonType = PokeTypeOne.Text;
            userPokemon.pokemonType2 = PokeTypeTwo.Text;
            userPokemon.sprite = "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/" + PokeIdSprite.Text + ".png";
            if (PokeIdSprite.Text == "")
            {
                userPokemon.sprite = SelectedPokemonImage.UriSource.ToString();
            }
            userPokemon.HPCrud = int.Parse(PokeHp.Text);
            userPokemon.AttackCrud = int.Parse(PokeAttack.Text);
            userPokemon.DefenseCrud = int.Parse(PokeDefense.Text);
            userPokemon.SpecialAttackCrud = int.Parse(PokeSpecialAttack.Text);
            userPokemon.SpecialDefenseCrud = int.Parse(PokeSpecialDefense.Text);
            userPokemon.Speed = int.Parse(PokeSpeed.Text);
            userPokemon.heightCRUD = int.Parse(PokeHeight.Text);
            userPokemon.weightCRUD = int.Parse(PokeWeight.Text);

            

            
             DBOperation.AlterPokemonCrud(userPokemon, id);
            }

            DBOperation.ReadCRUDB(Pokemon);

            
            ClearBoxes(AddingNewPokemon);
            SaveButton.IsEnabled = true;
            UpdateButton.IsEnabled = false;
            ClearButton.IsEnabled = false;
        }
        private void UpdateForm(object sender, RoutedEventArgs e)
        {
            PokeName.Text = PokemonName.Text;
            PokeTypeOne.Text = TypeOne.Text;
            PokeHp.Text = Hp.Text;
            PokeAttack.Text = Attack.Text;
            PokeDefense.Text = Defense.Text;
            PokeSpecialAttack.Text = SpecialAttack.Text;
            PokeSpecialDefense.Text = SpecialDefense.Text;
            PokeSpeed.Text = Speed.Text;
            PokeHeight.Text = Height.Text;
            PokeWeight.Text = Weight.Text;
            PokeIdSprite.Text = "";
            if (TypeTwo.Text != "")
            {
                PokeTypeTwo.Text = TypeTwo.Text;
            }
            SaveButton.IsEnabled = false;
            UpdateButton.IsEnabled = true;
            ClearButton.IsEnabled = true;
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
