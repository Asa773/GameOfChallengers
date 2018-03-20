using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GameOfChallengers.ViewModels;
using GameOfChallengers.Models;
using GameOfChallengers.Controllers;
using GameOfChallengers.Views.Scores;
using GameOfChallengers.Views.Items;

namespace GameOfChallengers.Views.Battle
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BattleScreen : ContentPage
	{
        GameScoreController game = new GameScoreController();
        public Creature Data { get; set; }
		public BattleScreen ()
		{
			InitializeComponent ();
            game.Start(false);
            RefreshBattleScreen();
		}

        public void RefreshBattleScreen()
        {
            TurnImage.Source = game.battle.GetCreatureTurnImage();
            cell00.Image = GetImage(game.battle.GameBoard[0, 0]);
            cell01.Image = GetImage(game.battle.GameBoard[0, 1]);
            cell02.Image = GetImage(game.battle.GameBoard[0, 2]);
            cell03.Image = GetImage(game.battle.GameBoard[0, 3]);
            cell04.Image = GetImage(game.battle.GameBoard[0, 4]);
            cell05.Image = GetImage(game.battle.GameBoard[0, 5]);
            cell10.Image = GetImage(game.battle.GameBoard[1, 0]);
            cell11.Image = GetImage(game.battle.GameBoard[1, 1]);
            cell12.Image = GetImage(game.battle.GameBoard[1, 2]);
            cell13.Image = GetImage(game.battle.GameBoard[1, 3]);
            cell14.Image = GetImage(game.battle.GameBoard[1, 4]);
            cell15.Image = GetImage(game.battle.GameBoard[1, 5]);
            cell20.Image = GetImage(game.battle.GameBoard[2, 0]);
            cell21.Image = GetImage(game.battle.GameBoard[2, 1]);
            cell22.Image = GetImage(game.battle.GameBoard[2, 2]);
            cell23.Image = GetImage(game.battle.GameBoard[2, 3]);
            cell24.Image = GetImage(game.battle.GameBoard[2, 4]);
            cell25.Image = GetImage(game.battle.GameBoard[2, 5]);

        }

        private FileImageSource GetImage(Creature creature)
        {

            if (creature == null)
                return "BlankImage.jpeg";
            else if (creature.ImageURI == "icon.png")
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
            game.battle.SelectedGridCellI = i;
            game.battle.SelectedGridCellJ = j;
            int result = game.NextTarget();
            if(result == 0)
            {
                GameOver();
                return;
            }else if(result == 1)
            {
                BattleOver();
                return;
            }
            RefreshBattleScreen();
        }

        private async void BattleOver()
        {

            var outputString = "Battle Over! Current Score " + game.GameScore.TotalXP;
            var action = await DisplayActionSheet(outputString,
                "Cancel",
                null,
                "Continue");
            if (action == "Continue")
            {
                //await Navigation.PushAsync(new ScoreDetailPage(new ScoreDetailViewModel(game.GameScore)));
                RefreshBattleScreen();
            }

        }


        private async void GameOver()
        {


            var outputString = "Game Over! Score " + game.GameScore.TotalXP;
            var action = await DisplayActionSheet(outputString,
                "Cancel",
                null,
                "View Score");
            if (action == "View Score")
            {
                await Navigation.PushAsync(new ScoreDetailPage(new ScoreDetailViewModel(game.GameScore)));
                Navigation.RemovePage(this);
            }
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


        
	}
}