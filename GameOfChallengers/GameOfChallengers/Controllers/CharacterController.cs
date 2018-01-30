using GameOfChallengers.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfChallengers.Controllers
{
    class CharacterController
    {

        public void EquipItem(Creature character, int itemLoc)
        {
            //equip items at the end of the round to a body location
        }

        public void DropItems(Creature character)
        {
            //drop all items when dead
        }

        public int GetBaseAttack(Creature character)
        {
            int baseAttack = 0;//this will be based on the character stats
            return baseAttack;
        }

        public int GetBaseDamage(Creature character)
        {
            int baseDamage = 0;//this will be based on the character stats
            return baseDamage;
        }

        public void TestForLevelUp(Creature character, int xp)
        {
            //if character can be leveled up call private healper
        }

        private void LevelUp(Creature character, int newLevel)
        {
            //level up the character
        }

        public void TakeDamage(Creature character, int amount)
        {
            //character takes damage and checks for death
        }

    }
}
