using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfChallengers.Models
{
    public enum Attributes
    {
        //for if something goes wrong
        unknown = 0,
        //the attack attribute
        Attack = 3,
        //the defence attribute
        Defence = 5,
        //the speed attribute
        Speed = 7,
    }

    public static class AttributeList
    {

        // Returns a list of strings of the enum for Attribute
        // Removes the attributes that are not changable by Items such as Unknown, MaxHealth
        public static List<string> GetListItem
        {
            get
            {
                var myList = Enum.GetNames(typeof(Attributes)).ToList();
                var myReturn = myList.Where(a => 
                                            a.ToString() != Attributes.unknown.ToString()
                                            ).ToList();
                return myReturn;
            }
        }

        //// Returns a list of strings of the enum for Attribute
        //// Removes the unknown
        //public static List<string> GetListCharacter
        //{
        //    get
        //    {
        //        var myList = Enum.GetNames(typeof(Attribute)).ToList();
        //        var myReturn = myList.Where(a => 
        //                                    a.ToString() != Attributes.Unknown.ToString()
        //                                    ).ToList();
        //        return myReturn;
        //    }
        //}

        // Given the String for an enum, return its value.  That allows for the enums to be numbered 2,4,6 rather than 1,2,3
        public static Attributes ConvertStringToEnum(string value)
        {
            return (Attributes)Enum.Parse(typeof(Attributes), value);
        }
    }
}
