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
        String selectedImg = "BlankImage.jpeg";

		public CreateMonster ()
		{
			InitializeComponent ();
            Data = new Creature
            {
                Name = "Monster name",
                Id = Guid.NewGuid().ToString(),
                Type = 1,
                Level = 1,
                XP = 0,
                MaxHealth = 10,
                CurrHealth = 10,
                Alive = true,
                ImageURI = "BlankImage.jpeg",
            };

            BindingContext = this;
            selectedImg = ImagePicker.SelectedItem.ToString();
            Data.ImageURI = ImagePicker.SelectedItem.ToString();

        }


        private void setImage(object sender, EventArgs e)
        {
            selectedImg = ImagePicker.SelectedItem.ToString();
            ChangeImg.Source = selectedImg;
        }


        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddData", Data);
            await Navigation.PopAsync();
        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

    }
}