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

// O modelo de item de Página em Branco está documentado em https://go.microsoft.com/fwlink/?LinkId=234238

namespace Pokedex
{
    /// <summary>
    /// Uma página vazia que pode ser usada isoladamente ou navegada dentro de um Quadro.
    /// </summary>
       
    public sealed partial class CrudPokemon : Page
    {
        public ObservableCollection<Pokemon> Pokemon { get; set; }

        public CrudPokemon()
        {
            this.InitializeComponent();
            Pokemon = new ObservableCollection<Pokemon>();
        }

        
        private void PokeListViewCrud_ItemClick(object sender, ItemClickEventArgs e)
        {


        }

        private void Register_click(object sender, RoutedEventArgs e) 
        { 
            PokemonName.Text = PokeName.Text;
            TypeOne.Text = PokeTypeOne.Text;
            TypeBlock.Text = "Type";
            HpBlock.Text = "HP";
            Hp.Text = PokeHp.Text;
            AttackBlock.Text = "Attack";
            Attack.Text = PokeAttack.Text;
            DefenseBlock.Text = "Defense";
            Defense.Text = PokeDefense.Text;
            SpecialAttackBlock.Text = "Special Attack";
            SpecialAttack.Text = PokeSpecialAttack.Text;
            SpecialDefenseBlock.Text = "Special Defense";
            SpecialDefense.Text = PokeSpecialDefense.Text;
            SpeedBlock.Text = "Speed";
            Speed.Text = PokeSpeed.Text;
            HeightBlock.Text = "Height";
            Height.Text = PokeHeight.Text;
            WeightBlock.Text = "Weight";
            Weight.Text = PokeWeight.Text;

            if(PokeTypeTwo.Text == "")
            {
                TypeTwo.Text = "";
            }
            else
            {

                TypeTwo.Text = PokeTypeTwo.Text;
            }

        }
    }
}
