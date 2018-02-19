using System;
using SQLite;

namespace GameOfChallengers.Models
{
    public class Item
    {
        [PrimaryKey]
        public string Id { get; set; }// unique id for each item

        public string Name { get; set; }// 25 char max

        public int Value { get; set; }// how much the attribute is increased
        
        public bool Range { get; set; }// if the item allows ranged attacks

        public Attributes Att { get; set; }// enum for what creature attribute the item effects

        public Locations Loc { get; set; }// enum for what creature location the item is attached to

        public void Update(Item newData)
        {
            if (newData == null)
            {
                return;
            }

            // Update all the fields in the Data, except for the Id
            Name = newData.Name;
            Value = newData.Value;
            Range = newData.Range;
            Att = newData.Att;
            Loc = newData.Loc;
        }
    }
}