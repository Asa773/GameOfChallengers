using GameOfChallengers.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfChallengers.Controllers
{
    class CharacterController
    {

        public void EquipItem(Creature character, int itemLoc, Item item)
        {
            //equip items at the end of the round to a body location
        }

        public void DropItems(Creature character)
        {
            //drop all items when dead
        }

        public int GetBaseAttack(Creature character)
        {
            int baseAttack = 0;//this will be based on the character stats + item boosts
            return baseAttack;
        }

        public int GetBaseDamage(Creature character)
        {
            int baseDamage = 0;//this will be based on the character stats including item boosts
            return baseDamage;
        }

        public int GetBaseSpeed(Creature character)
        {
            int baseSpeed = 0;//this will be based on the character stats + any item boosts
            return baseSpeed;
        }

        public int GetBaseDefense(Creature character)
        {
            int baseDefense = 0;//this will be based on the character stats including item boosts
            return baseDefense;
        }

        public void TestForLevelUp(Creature character, int xp)
        {
            //if character can be leveled up call private helper
            //this method will test the character's xp against a data table with the xp numbers stored
        }

        private void LevelUp(Creature character, int newLevel)
        {
            //level up the character
            //TestForLevelUp will call this method with a new level to change the character to
            //the characters stats will be reset based on a data table with the base stats stored
        }

        public void TakeDamage(Creature character, int amount)
        {
            //character takes damage and checks for death
        }

    }
}
