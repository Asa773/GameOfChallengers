using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfChallengers.Models
{
    class GameItem
    {
        public string Name { get; set; }
        
        public int Value { get; set; }
        
        public bool Range { get; set; }

        public Attributes Att { get; set; }

        public Locations Loc { get; set; }
    }
}
