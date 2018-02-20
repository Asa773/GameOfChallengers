using System;
using System.Collections.Generic;
using System.Text;
using System.Linq; 

namespace GameOfChallengers.Models
{
    public enum Locations
    {
        //for if something goes wrong
        unknown = 0,
        //the head location
        Head = 3,
        //the body location
        Body = 5,
        //the feet location
        Feet = 7,
        //the right hand location
        RHand = 10,
        //the left hand location
        Lhand = 13,
        //the right finger location
        RFinger = 15,
        //the left finger location
        LFinger = 17,
    }


    public static class ItemLocationList
    {
        // Gets the lsit of locations that an Item can have.
        // Does not include the Left and Right Finger
        public static List<string> GetListItem
        {
            get
            {
                var myList = Enum.GetNames(typeof(Locations)).ToList();
                var myReturn = myList.Where(a =>
                                            a.ToString() != Locations.unknown.ToString() 
                                            ).ToList();
                return myReturn;
            }
        }

        // Gets the list of locations a character can use
        // Removes Finger for example, and allows for left and right finger
        //public static List<string> GetListCharacter
        //{
        //    get
        //    {
        //        var myList = Enum.GetNames(typeof(Location)).ToList();
        //        var myReturn = myList.Where(a => 
        //                                    a.ToString() != Location.Finger.ToString()
        //                                    ).ToList();
        //        return myReturn;
        //    }
        //}

        // Given the String for an enum, return its value.  That allows for the enums to be numbered 2,4,6 rather than 1,2,3
        public static Locations ConvertStringToEnum(string value)
        {
            return (Locations)Enum.Parse(typeof(Locations), value);
        }

    }
}
