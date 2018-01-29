using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfChallengers.Controllers
{
    class CharacterController
    {
        public void EquipItem(int id, int itemLoc)
        {
            //equip items at the end of the round to a body location
        }

        public void DropItems(int id)
        {
            //drop all items when dead
        }

        public void Attack(int id, int target)
        {
            //run the attack on the selected monster
        }

        public void Move(int id, int loc)
        {
            bool canMove = MoveTest(id, loc);
            //move the character if they are allowed to move there
        }
        private bool MoveTest(int id, int loc)
        {
            //test if a character can move to the selected sqare on the board
            return true;
        }

        public int DoDamage(int id, int baseDamage)
        {
            //get how much damage the character will do
            int finalDamage = GetBaseDamage(id);
            return finalDamage;
        }

        private int GetBaseDamage(int id)
        {
            int baseDamage = 0;//this will be based on the character stats
            return baseDamage;
        }

        public void TestForLevelUp(int id, int xp)
        {
            //if character can be leveled up call private healper
        }
        private void LevelUp(int id, int newLevel)
        {
            //level up the character
        }

        public void TakeDamage(int id, int amount)
        {
            //character takes damage and checks for death
        }

    }
}
