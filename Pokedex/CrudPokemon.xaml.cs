using System;
using System.Collections.Generic;
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
using Windows.UI.Xaml.Navigation;

// O modelo de item de Página em Branco está documentado em https://go.microsoft.com/fwlink/?LinkId=234238

namespace Pokedex
{
    /// <summary>
    /// Uma página vazia que pode ser usada isoladamente ou navegada dentro de um Quadro.
    /// </summary>
    public sealed partial class CrudPokemon : Page
    {
        public string PokeName { get; set; }
        public string PokeHeight { get; set; }
        public string PokeWeight { get; set; }
        public string PokeTypeOne { get; set; }
        public string PokeTypeTwo { get; set; }
        public string PokeHp { get; set; }
        public string PokeAttack { get; set; }
        public string PokeDefense { get; set; }
        public string PokeSpeed { get; set; }
        public string PokeSpecialAttack { get; set; }
        public string PokeSpecialDefense { get; set; }
        public CrudPokemon()
        {
            this.InitializeComponent();
           
            
        }

        private void RegisterPokemon(object sender, RoutedEventArgs e)
        {
            //test
            PokeName = PokemonName.Text;
            PokeHeight = PokemonHeight.Text;
            PokeWeight = PokemonWeight.Text;
            PokeHp = PokemonHp.Text;
            PokeAttack = PokemonAttack.Text;
            PokeDefense = PokemonDefense.Text;
            PokeSpecialDefense = PokemonSpecialDefense.Text;
            PokeSpecialAttack = PokemonSpecialAttack.Text;
            PokeSpeed = PokemonSpeed.Text;
            PokeTypeOne = PokemonTypeOne.Text;
            PokeTypeTwo = PokemonTypeTwo.Text;
        }

    }
}
