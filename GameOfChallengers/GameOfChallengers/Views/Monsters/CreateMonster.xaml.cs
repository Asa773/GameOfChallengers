using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GameOfChallengers.Views.Monsters;
using GameOfChallengers.ViewModels;
using GameOfChallengers.Models;

namespace GameOfChallengers.Views.Monsters
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CreateMonster : ContentPage
	{
        public Creature Data{ get; set; }
		public CreateMonster ()
		{
			InitializeComponent ();
            Data = new Creature
            {
                Name = "Monster",
                CurrHealth = 0,
                Speed = 0,
                Attack = 0,
                Defense = 0,
                XP = 0
            };
		}

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddData", Data);
            await
                Navigation.PopAsync();
        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

    }
}