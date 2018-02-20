using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GameOfChallengers.Views.Character
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BuildTeam : ContentPage
	{
		public BuildTeam ()
		{
			InitializeComponent ();
		}

        //private async void SaveTeam_Clicked(object sender, EventArgs e)
        //{
        //    await Navigation.PushAsync(new EditCharacterPage(_viewModel));
        //}

        //private async void Edit_Clicked(object sender, EventArgs e)
        //{
        //    await Navigation.PushAsync(new EditCharacterPage(_viewModel));
        //}
    }
}