using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using GameOfChallengers.ViewModels;
using Xamarin.Forms;
using GameOfChallengers.Models;
using GameOfChallengers.Controllers;

namespace GameOfChallengers.Views.Items
{
    public partial class AssignItemPage : ContentPage
    {
        private TeamViewModel _viewModel;
        public ObservableCollection<Item> _Dataset { get; set; }//Items collection
        public Creature Data { get; set; }
        public ObservableCollection<Creature> Dataset { get; set; }//Items will be assigned to this list of character

        public AssignItemPage()
        {
            Data = new Creature
            {
                //default attributes
                Name = "Character name",
                Id = Guid.NewGuid().ToString(),
                Type = 0,
                Level = 1,
                XP = 0,
                MaxHealth = 10,
                CurrHealth = 10,
                Alive = true,

            };

            // Set the data binding for the page         
            BindingContext = _viewModel = TeamViewModel.Instance;

        }
    }
}
