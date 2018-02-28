﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Windows.UI.Xaml.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GameOfChallengers.ViewModels;
using GameOfChallengers.Models;
using GameOfChallengers.Controllers;

namespace GameOfChallengers.Views.Battle
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BattleScreen : ContentPage
	{
        private TeamViewModel _viewModel;
        public Creature Data { get; set; }
        public string a = "MM";
        Button b;
		public BattleScreen ()
		{
            
            //BattleController bc = new BattleController(_viewModel,a);
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
           
            _viewModel = new TeamViewModel();
            BindingContext = a;
		}

        private async void DeleteTeamMember(object sender, EventArgs e)
        {

            MessagingCenter.Send(this, "DeleteData", Data);
            await Navigation.PopAsync();

        }
        private void ClickedZeroZero(object sender, EventArgs e)
        {
            zero.Text = "D"; 
        }


	}
}