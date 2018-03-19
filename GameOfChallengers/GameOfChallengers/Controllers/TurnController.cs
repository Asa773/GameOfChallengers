using GameOfChallengers.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfChallengers.Controllers
{
    class TurnController
    {
        //there will be one controller per type and the specific creature will be passed in to the controller methods
        MonsterController mc = new MonsterController() ;
        CharacterController cc = new CharacterController(); 


        public bool Attack(Creature creature1, Creature creature2) //Attack method
        {
            int score1, score2 = 0;
            bool succeeded = false;
            int dateSeed = DateTime.Now.Millisecond;
            Random roll = new Random(dateSeed);
            int rollValue = roll.Next(1, 21);//Dice roll between 1 and 21
            if (rollValue == 1)
            {
                return false;//returns false when its 1 which means it a miss
            }
            if (rollValue == 20)
            {
                return true;//returns true when its 20 which means it a hit
            }
            //run the attack of creature1 on creature2, return if it succeeded or not
            //attack is successful(true) if creature1 score > creature2 score
            if (creature1.Type == 0) //checks if creature type is character or monster 
            {
                score1 = rollValue + creature1.Level + cc.GetBaseAttack(creature1); //score1 will be given to the character when creature1 is 0
                score2 = mc.GetBaseDefense(creature2) + creature2.Level;//score2 will be given to the monster when creature1 is 0
            }
            else
            {
                score1 = rollValue + creature2.Level + mc.GetBaseAttack(creature2);//score1 will be given to the monster when creature1 is not 0
                score2 = cc.GetBaseDefense(creature1) + creature1.Level;//score2 will be given to the character when creature1 is not 0
            }

            if (score1 > score2)
            {
                succeeded = true; //when the attacking creature has higher score the attack is a success

            }
            return succeeded;
        }
            
           
        public int DamageToDo(Creature creature) //Damage done by the creature
        {
            //get the 
            int finalDamage = 0;
            if (creature.Type == 0)
            {
                finalDamage = cc.GetBaseDamage(creature) + (int)Math.Ceiling((double)creature.Level / 4); //damage calculate for the character if the creature is 0
            }
            else
            {
                finalDamage = mc.GetBaseDamage(creature) + (int)Math.Ceiling((double)creature.Level / 4);//damage calculate for the character if the creature is not 0
            }
            return finalDamage;
        }

    }
}
