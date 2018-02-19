using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfChallengers.Models
{
    public class CreatureInventory
    {
        public Item Head { get; set; }// item slot for head location
        public Item Body { get; set; }// item slot for body location
        public Item Feet { get; set; }// item slot for feet location
        public Item LHand { get; set; }// item slot for left hand location
        public Item RHand { get; set; }// item slot for right hand location
        public Item LFinger { get; set; }// item slot for left finger location
        public Item RFinger { get; set; }// item slot for right finger location

    }
}
