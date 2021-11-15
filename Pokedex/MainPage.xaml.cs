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
        public MainPage() 
        {
            this.InitializeComponent();
            ApiRequest.InitializeClient();
            Pokedex = new ObservableCollection<NamedAPIResource>();
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            SplitMenu.IsPaneOpen = !SplitMenu.IsPaneOpen;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ProgressRing.IsActive = true;
            ProgressRing.Visibility = Visibility.Visible;
            
            await ApiRequest.FillPokedexList(Pokedex);

            ProgressRing.IsActive = false;
            ProgressRing.Visibility = Visibility.Collapsed;
        }
    }
}
