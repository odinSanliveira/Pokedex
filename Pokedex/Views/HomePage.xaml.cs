using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


namespace Pokedex
{
    public sealed partial class HomePage : Page
    {
        public HomePage()
        {
            this.InitializeComponent();
            QrCode.Visibility = Visibility.Visible;
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            QrCode.Visibility = Visibility.Visible;

        }
    }
}
