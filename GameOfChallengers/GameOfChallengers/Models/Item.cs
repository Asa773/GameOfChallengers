using System;

namespace GameOfChallengers.Models
{
    public class Item
    {
        public string Name { get; set; }
        public string Attribute { get; set; }
        public int Value { get; set; }
        public string Location { get; set; }
        public bool Range { get; set; }
    }
}