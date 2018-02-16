using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfChallengers.Models
{
    class GameItem
    {
        public string Name { get; set; }// 25 char max

        public int Value { get; set; }// how much the attribute is increased
        
        public bool Range { get; set; }// if the item allows ranged attacks

        public Attributes Att { get; set; }// enum for what creature attribute the item effects

        public Locations Loc { get; set; }// enum for what creature location the item is attached to
    }
}
