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
using GameOfChallengers.Controllers;

namespace GameOfChallengers.Views.Battle
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BattleScreen : ContentPage
	{
        BattleController bc = new BattleController();
        private TeamViewModel _viewModel;
        public Creature Data { get; set; }
		public BattleScreen ()
		{
            _viewModel = TeamViewModel.Instance;
            _viewModel.LoadData();
           
			InitializeComponent ();
            bc.SetBattleController(1);
            RefreshBattleScreen();
		}


        public void RefreshBattleScreen()
        {
            cell00.Image = GetImage(bc.GameBoard[0, 0]);
            cell01.Image = GetImage(bc.GameBoard[0, 1]);
            cell02.Image = GetImage(bc.GameBoard[0, 2]);
            cell03.Image = GetImage(bc.GameBoard[0, 3]);
            cell04.Image = GetImage(bc.GameBoard[0, 4]);
            cell05.Image = GetImage(bc.GameBoard[0, 5]);
            cell10.Image = GetImage(bc.GameBoard[1, 0]);
            cell11.Image = GetImage(bc.GameBoard[1, 1]);
            cell12.Image = GetImage(bc.GameBoard[1, 2]);
            cell13.Image = GetImage(bc.GameBoard[1, 3]);
            cell14.Image = GetImage(bc.GameBoard[1, 4]);
            cell15.Image = GetImage(bc.GameBoard[1, 5]);
            cell20.Image = GetImage(bc.GameBoard[2, 0]);
            cell21.Image = GetImage(bc.GameBoard[2, 1]);
            cell22.Image = GetImage(bc.GameBoard[2, 2]);
            cell23.Image = GetImage(bc.GameBoard[2, 3]);
            cell24.Image = GetImage(bc.GameBoard[2, 4]);
            cell25.Image = GetImage(bc.GameBoard[2, 5]);

        }


        private FileImageSource GetImage(Creature creature)
        {
           
            if (creature == null)
                return "BlankImage.jpeg";
            else
                 return creature.ImageURI;
        }

        public void BattleMessages(string message)
        {
            L1.Text = message ;
            //add message to some kind of list that can be displayed on the view
        }

       

        public void MoveAndAttack(int i, int j)
        {

            RefreshBattleScreen();
        }


        private void Clicked00(object sender, EventArgs e)
        {
            
            MoveAndAttack(0, 0);
           
        }

        private void Clicked01(object sender, EventArgs e)
        {
            MoveAndAttack(0,1);
        }
        private void Clicked02(object sender, EventArgs e)
        {
            MoveAndAttack(0,2);
        }
        private void Clicked03(object sender, EventArgs e)
        {
            MoveAndAttack(0,3);
        }
        private void Clicked04(object sender, EventArgs e)
        {
            MoveAndAttack(0,4);
        }
        private void Clicked05(object sender, EventArgs e)
        {
            MoveAndAttack(0,5);
        }
        private void Clicked10(object sender, EventArgs e)
        {
            MoveAndAttack(1,0);
        }
        private void Clicked11(object sender, EventArgs e)
        {
            MoveAndAttack(1,1);
        }
        private void Clicked12(object sender, EventArgs e)
        {
            MoveAndAttack(1,2);
        }
        private void Clicked13(object sender, EventArgs e)
        {
            MoveAndAttack(1,3);
        }
        private void Clicked14(object sender, EventArgs e)
        {
            MoveAndAttack(1,4);
        }
        private void Clicked15(object sender, EventArgs e)
        {
            MoveAndAttack(1,5);
        }
        private void Clicked20(object sender, EventArgs e)
        {
            MoveAndAttack(2,0);
        }
        private void Clicked21(object sender, EventArgs e)
        {
            MoveAndAttack(2,1);
        }
        private void Clicked22(object sender, EventArgs e)
        {
            MoveAndAttack(2,2);
        }
        private void Clicked23(object sender, EventArgs e)
        {
            MoveAndAttack(2,3);
        }
        private void Clicked24(object sender, EventArgs e)
        {
            MoveAndAttack(2,4);
        }
        private void Clicked25(object sender, EventArgs e)
        {
            MoveAndAttack(2,5);
        }




        private void Start_clicked(object sender, EventArgs e)
        {
            L1.Text = "C1 hit M1\n M1 is dead \n C1 get XP of 100";
        }

        private async void End_clicked(object sender, EventArgs e)
        {
           // L1.Text = "End the Game";
            await Navigation.PushAsync(new BattleOver());
        }
	}
}