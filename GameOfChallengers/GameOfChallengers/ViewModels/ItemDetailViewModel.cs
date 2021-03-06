﻿using System;

using GameOfChallengers.Models;

namespace GameOfChallengers.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {

        public string LocationString => Data.Location.ToString();

        public Item Data { get; set; }
        public ItemDetailViewModel(Item item = null)
        {
            //setting the title
            Title = item?.Name;
            Data = item;
        }
    }
}
