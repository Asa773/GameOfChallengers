using System;
using System.Collections.Generic;
using SQLite;
using GameOfChallengers.Controllers;
using GameOfChallengers.Models;
using Newtonsoft.Json;
using GameOfChallengers.ViewModels;


namespace GameOfChallengers.Models
{
    public class Creature
    {
        [PrimaryKey]
        public string Id { get; set; }// unique id for each character and monster
        public int Type { get; set; }// character(0) or monster(1)
        public string Name { get; set; }// 25 char max
        public int Level { get; set; }// 1-20
        public bool OnTeam { get; set; }// if it is a character, is it on the team
        public int Attack { get; set; }// creature's base attack stat
        public int Defense { get; set; }// creature's base defense stat
        public int Speed { get; set; }// creature's base speed stat
        public int XP { get; set; }// 0-355000
        public int MaxHealth { get; set; }// creature's current max health
        public int CurrHealth { get; set; }// creature's current health
        public bool Alive { get; set; }// 1 is alive, 0 is dead
        public int Loc { get; set; }// location on game board
        public string ImageURI { get; set; }// image to be inserted
        public string AttributeString { get; set; }
        public string UniqueItem { get; set; }
        public int Damage { get; set; }


        public string CInventory { get; set; }// inventory of items equipped to this creature

        public string Head { get; set; }
        public string Necklass { get; set; }
        public string Feet { get; set; }
        public string OffHand { get; set; }
        public string PrimaryHand { get; set; }
        public string LeftFinger { get; set; }
        public string RightFinger { get; set; }


        public Creature()
        {
            //                      ***need id preset?***
            Alive = true;
            Level = 1;
            XP = 0;
            OnTeam = false;
            Head = null;
            Necklass = null;
            Feet = null;
            OffHand = null;
            PrimaryHand = "bow";//              ***TEMP***
            LeftFinger = null;
            RightFinger = null;
           

        }

        public void Update(Creature newData)
        {
            if (newData == null)
            {
                return;
            }

            // Update all the fields in the Data, except for the Id
            Type = newData.Type;
            Name = newData.Name;
            Level = newData.Level;
            Attack = newData.Attack;
            Defense = newData.Defense;
            Speed = newData.Speed;
            XP = newData.XP;
            MaxHealth = newData.MaxHealth;
            CurrHealth = newData.CurrHealth;
            Alive = newData.Alive;
            Loc = newData.Loc;
            Head = newData.Head;
            Necklass = newData.Necklass;
            Feet = newData.Feet;
            OffHand = newData.OffHand;
            PrimaryHand = newData.PrimaryHand;
            LeftFinger = newData.LeftFinger;
            RightFinger = newData.RightFinger;
            ImageURI = newData.ImageURI;
            AttributeString = newData.AttributeString;
           

        }

        public List<string> GetItemIDs()
        {
            List<string> itemIds = new List<string>();
            if (Head != null)
            {
                itemIds.Add(Head);
            }
            if (Necklass != null)
            {
                itemIds.Add(Necklass);
            }
            if (Feet != null)
            {
                itemIds.Add(Feet);
            }
            if (OffHand != null)
            {
                itemIds.Add(OffHand);
            }
            if (PrimaryHand != null)
            {
                itemIds.Add(PrimaryHand);
            }
            if (LeftFinger != null)
            {
                itemIds.Add(LeftFinger);
            }
            if (RightFinger != null)
            {
                itemIds.Add(RightFinger);
            }
            return itemIds;
        }

        public List<string> GetHandIDs()
        {
            List<string> itemIds = new List<string>();
            if (OffHand != null)
            {
                itemIds.Add(OffHand);
            }
            if (PrimaryHand != null)
            {
                itemIds.Add(PrimaryHand);
            }
            return itemIds;
        }







        #region Items
        // Gets the unique item (if any) from this monster when it dies...
        public Item GetUniqueItem()
        {
            var myReturn = ItemsViewModel.Instance.GetItem(UniqueItem);

            return myReturn;
        }

        // Drop all the items the monster has
        public List<Item> DropAllItems()
        {
            var myReturn = new List<Item>();

            // Drop all Items
            Item myItem;

            myItem = ItemsViewModel.Instance.GetItem(UniqueItem);
            if (myItem != null)
            {
                myReturn.Add(myItem);
            }
            return myReturn;
        }

        #endregion Items

        public string FormatOutputc(Creature creature)
        {
            var myReturn = string.Empty;


            Creature c = new Creature();
            CharacterController cc = new CharacterController();
            




            myReturn += creature.Name;
            myReturn += " , " + creature.Type;
            myReturn += " , Level : " + creature.Level;
            myReturn += " , Total Experience : " + creature.XP;
            //myReturn += " , Items : " + ItemSlotsFormatOutput();

            myReturn += " Damage : " + cc.GetBaseDamage(creature);
           

            myReturn += "Speed : " + creature.Speed;
            myReturn += " , Defense : " + creature.Defense;
            myReturn += " , Attack : " + creature.Attack;
            myReturn += " , CurrentHealth : " + creature.CurrHealth;
            myReturn += " , MaxHealth : " + creature.MaxHealth;
            return myReturn;
        }
        public string FormatOutputm(Creature creature)
        {
            var myReturn = string.Empty;


            Creature c = new Creature();
            
            MonsterController mc = new MonsterController();




            myReturn += creature.Name;
            myReturn += " , " + creature.Type;
            myReturn += " , Level : " + creature.Level;
            myReturn += " , Total Experience : " + creature.XP;
            //myReturn += " , Items : " + ItemSlotsFormatOutput();

          
            myReturn += " Damage : " + mc.GetBaseDamage(creature);

            myReturn += "Speed : " + creature.Speed;
            myReturn += " , Defense : " + creature.Defense;
            myReturn += " , Attack : " + creature.Attack;
            myReturn += " , CurrentHealth : " + creature.CurrHealth;
            myReturn += " , MaxHealth : " + creature.MaxHealth;
            return myReturn;
        }

        public Item AddItem(ItemLocationEnum itemlocation, string itemID)
        {
            Item myReturn;

            switch (itemlocation)
            {
                case ItemLocationEnum.Feet:
                    myReturn = GetItem(Feet);
                    Feet = itemID;
                    break;

                case ItemLocationEnum.Head:
                    myReturn = GetItem(Head);
                    Head = itemID;
                    break;

                case ItemLocationEnum.Necklass:
                    myReturn = GetItem(Necklass);
                    Necklass = itemID;
                    break;

                case ItemLocationEnum.PrimaryHand:
                    myReturn = GetItem(PrimaryHand);
                    PrimaryHand = itemID;
                    break;

                case ItemLocationEnum.OffHand:
                    myReturn = GetItem(OffHand);
                    OffHand = itemID;
                    break;

                case ItemLocationEnum.RightFinger:
                    myReturn = GetItem(RightFinger);
                    RightFinger = itemID;
                    break;

                case ItemLocationEnum.LeftFinger:
                    myReturn = GetItem(LeftFinger);
                    LeftFinger = itemID;
                    break;

                default:
                    myReturn = null;
                    break;
            }

            return myReturn;
        }

        public Item GetItem(string itemString)
        {
            return ItemsViewModel.Instance.GetItem(itemString);
        }

        public Item GetItemByLocation(ItemLocationEnum itemLocation)
        {
            switch (itemLocation)
            {
                case ItemLocationEnum.Head:
                    return GetItem(Head);

                case ItemLocationEnum.Necklass:
                    return GetItem(Necklass);

                case ItemLocationEnum.PrimaryHand:
                    return GetItem(PrimaryHand);

                case ItemLocationEnum.OffHand:
                    return GetItem(OffHand);

                case ItemLocationEnum.RightFinger:
                    return GetItem(RightFinger);

                case ItemLocationEnum.LeftFinger:
                    return GetItem(LeftFinger);

                case ItemLocationEnum.Feet:
                    return GetItem(Feet);
            }

            return null;
        }


    }
}
