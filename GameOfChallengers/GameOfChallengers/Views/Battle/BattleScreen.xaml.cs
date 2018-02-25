using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Windows.UI.Xaml.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GameOfChallengers.ViewModels;
using GameOfChallengers.Models;

namespace GameOfChallengers.Views.Battle
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BattleScreen : ContentPage
	{
        public Creature Data { get; set; }

		public BattleScreen ()
		{
			InitializeComponent ();
            Data = new Creature
            {
                Name = "Character name",
                Id = Guid.NewGuid().ToString(),
                Type = 0,
                Level = 1,
                XP = 0,
                MaxHealth = 10,
                CurrHealth = 10,
                Alive = true,

            };
		}

        private async void DeleteTeamMember(object sender, EventArgs e)
        {

            MessagingCenter.Send(this, "DeleteData", Data);
            await Navigation.PopAsync();

        }

	}
}