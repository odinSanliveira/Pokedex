using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


namespace Pokedex
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            Home.IsSelected = true;
            FramePokedex.Navigate(typeof(HomePage));
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            SplitMenu.IsPaneOpen = !SplitMenu.IsPaneOpen;
        }
        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Register.IsSelected)
            {
                FramePokedex.Navigate(typeof(CrudPokemon));
            }
            else if (PokedexLayout.IsSelected)
            {
                FramePokedex.Navigate(typeof(MainPokedex));
            }else if (Home.IsSelected)
            {
                FramePokedex.Navigate(typeof(HomePage));

            }
        }
    }
}
