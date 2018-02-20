using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using GameOfChallengers.Views.Monsters;
using GameOfChallengers.ViewModels;
using GameOfChallengers.Models;

namespace GameOfChallengers.ViewModels
{
	public class MonsterDetailViewModel : BaseViewModel
	{

        public Creature Data{ get; set; }
        public MonsterDetailViewModel(Creature data= null)
        {
            Title = data?.Name;
            Data = data;
        }


    }
}