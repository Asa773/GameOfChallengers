using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfChallengers.Models
{
    class Creature
    {
        public int Id { get; set; }// unique id for each character and monster
        public int Type { get; set; }// character or monster
        public string Name { get; set; }// 25 char max
        public int Level { get; set; }// 1-20
        public int Attack { get; set; }// creature's base attack stat
        public int Damage { get; set; }// creature's base damage stat
        public int Speed { get; set; }// creature's base speed stat
        public int XP { get; set; }// 0-355000
        public int MaxHealth { get; set; }// creature's current max health
        public int CurrHealth { get; set; }// creature's current health
        public bool Alive { get; set; }// 1 is alive, 0 is dead
        public int Loc { get; set; }// location on game board

        public CreatureInventory CInventory { get; set; }// inventory of item equiped to this creature

    }
}
