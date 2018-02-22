using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GameOfChallengers.Views.Battle
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BattleOver : ContentPage
	{
		public BattleOver ()
		{
			InitializeComponent ();
		}

        private async void NextBattle_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BattleScreen());
        }
    }
}