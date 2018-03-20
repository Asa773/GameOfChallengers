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
            game.Start(false); //check to start the game
            RefreshBattleScreen();
		}

        //refresh the battle screen to add the images to the gameboard
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

        //gets the image for the creatures
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

       
        //Move is done depending on the gridcell the player wanted to move the creature
        public void MoveAndAttack(int i, int j)
        {
            game.battle.SelectedGridCellI = i;
            game.battle.SelectedGridCellJ = j;
            int result = game.NextTarget(); //checks for the result from the game
            if(result == 0)
            {
                GameOver(); //when 0 then the game is over
                return;
            }else if(result == 1)
            {
                BattleOver();//when 1 then the battle is over which means only round is done
                return;
            }
            RefreshBattleScreen();
        }

        //responses when battle has over asks to continue to proceed to the next battle
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


        //reponses to the game over and gives the score for that game
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
        //Movement for the creature and attacks the target
        private void Clicked00(object sender, EventArgs e)
        {
            MoveAndAttack(0, 0); //MoveAndAttack(row,column) where the creature can be moved depending on the row and column position
        }

        private void Clicked01(object sender, EventArgs e)
        {
            MoveAndAttack(0,1);//MoveAndAttack(row,column) where the creature can be moved depending on the row and column position
        }
        private void Clicked02(object sender, EventArgs e)
        {
            MoveAndAttack(0,2);//MoveAndAttack(row,column) where the creature can be moved depending on the row and column position
        }
        private void Clicked03(object sender, EventArgs e)
        {
            MoveAndAttack(0,3);//MoveAndAttack(row,column) where the creature can be moved depending on the row and column position
        }
        private void Clicked04(object sender, EventArgs e)
        {
            MoveAndAttack(0,4);//MoveAndAttack(row,column) where the creature can be moved depending on the row and column position
        }
        private void Clicked05(object sender, EventArgs e)
        {
            MoveAndAttack(0,5);//MoveAndAttack(row,column) where the creature can be moved depending on the row and column position
        }
        private void Clicked10(object sender, EventArgs e)
        {
            MoveAndAttack(1,0);//MoveAndAttack(row,column) where the creature can be moved depending on the row and column position
        }
        private void Clicked11(object sender, EventArgs e)
        {
            MoveAndAttack(1,1);//MoveAndAttack(row,column) where the creature can be moved depending on the row and column position
        }
        private void Clicked12(object sender, EventArgs e)
        {
            MoveAndAttack(1,2);//MoveAndAttack(row,column) where the creature can be moved depending on the row and column position
        }
        private void Clicked13(object sender, EventArgs e)
        {
            MoveAndAttack(1,3);//MoveAndAttack(row,column) where the creature can be moved depending on the row and column position
        }
        private void Clicked14(object sender, EventArgs e)
        {
            MoveAndAttack(1,4);//MoveAndAttack(row,column) where the creature can be moved depending on the row and column position
        }
        private void Clicked15(object sender, EventArgs e)
        {
            MoveAndAttack(1,5);//MoveAndAttack(row,column) where the creature can be moved depending on the row and column position
        }
        private void Clicked20(object sender, EventArgs e)
        {
            MoveAndAttack(2,0);//MoveAndAttack(row,column) where the creature can be moved depending on the row and column position
        }
        private void Clicked21(object sender, EventArgs e)
        {
            MoveAndAttack(2,1);//MoveAndAttack(row,column) where the creature can be moved depending on the row and column position
        }
        private void Clicked22(object sender, EventArgs e)
        {
            MoveAndAttack(2,2);//MoveAndAttack(row,column) where the creature can be moved depending on the row and column position
        }
        private void Clicked23(object sender, EventArgs e)
        {
            MoveAndAttack(2,3);//MoveAndAttack(row,column) where the creature can be moved depending on the row and column position
        }
        private void Clicked24(object sender, EventArgs e)
        {
            MoveAndAttack(2,4);//MoveAndAttack(row,column) where the creature can be moved depending on the row and column position
        }
        private void Clicked25(object sender, EventArgs e)
        {
            MoveAndAttack(2,5);//MoveAndAttack(row,column) where the creature can be moved depending on the row and column position
        }


        
	}
}