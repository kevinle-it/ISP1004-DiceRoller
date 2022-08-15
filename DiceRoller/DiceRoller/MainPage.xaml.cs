using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiceRoller.Models;
using Xamarin.Forms;

namespace DiceRoller
{
    public partial class MainPage : ContentPage
    {
        private int selectedDieNumSides = 4;

        public MainPage()
        {
            InitializeComponent();
        }

        public void RadioButton_CheckedChanged(System.Object sender, Xamarin.Forms.CheckedChangedEventArgs e)
        {
            RadioButton button = sender as RadioButton;
            int.TryParse(button.Value.ToString(), out selectedDieNumSides);
        }

        public void DisplayOne(System.Object sender, System.EventArgs e)
        {
            Die d = new Die(selectedDieNumSides);
            // display 1 result
            d.Roll();
            Result1.Text = d.CurrentSide.ToString();
            // hide label 2
            Result2.IsVisible = false;
        }

        public void DisplayTwo(System.Object sender, System.EventArgs e)
        {
            Die d = new Die(selectedDieNumSides);
            // display 2 results
            d.Roll();
            Result1.Text = d.CurrentSide.ToString();
            d.Roll();
            Result2.Text = d.CurrentSide.ToString();
            // show label 2
            Result2.IsVisible = true;
        }
    }
}

