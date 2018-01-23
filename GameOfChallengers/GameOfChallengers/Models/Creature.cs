using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfChallengers.Models
{
    class Creature
    {
        public int Id { get; set; }  //unique id for each character and monster
        public int Type { get; set; }  //character or monster
        public string Name { get; set; } //25 char max
        public int Level { get; set; } //1-20
        public int Attack { get; set; }
        public int Damage { get; set; }
        public int Speed { get; set; }
        public int XP { get; set; } //0-355000
        public int MaxHealth { get; set; }
        public int CurrHealth { get; set; }
        public bool Alive { get; set; } // 1 is alive, 0 is dead
        public int Loc { get; set; }  //location on game board

        //*This Item class exists but is WRONG, will be updated for the item assignment*
        public Item Head { get; set; }
        public Item Body { get; set; }
        public Item Feet { get; set; }
        public Item LHand { get; set; }
        public Item RHand { get; set; }
        public Item LFinger { get; set; }
        public Item RFinger { get; set; }

    }
}
