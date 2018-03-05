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
        
        public int Range { get; set; }// if the item allows ranged attacks

        public Attributes Att { get; set; }// enum for what creature attribute the item effects

        public Locations Loc { get; set; }// enum for what creature location the item is attached to

        public string ImageURI { get; set; }// image to be inserted

        public Item()
        {
            Name = "unknown";
            Id = null;
            Range = 0;
            Value = 0;

            Loc = Locations.unknown;
            Att = Attributes.unknown;

            //ImageURI = null;

        }

        public Item(string name, string guid, int range, int value, Locations location, Attributes attribute, string imageuri)
        {
            Name = name;
            Id = guid;
            ImageURI = imageuri;

            Range = range;
            Value = value;

            Loc = location;
            Att = attribute;
        }


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
            ImageURI = newData.ImageURI;
        }
    }
}