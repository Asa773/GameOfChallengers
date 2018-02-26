using GameOfChallengers.Models;
using System;
using System.Collections.Generic;
using System.Text;
using GameOfChallengers.Controllers;

namespace GameOfChallengers.Controllers
{
    class TurnController
    {
        MonsterController mc = new MonsterController() ;
        CharacterController cc = new CharacterController(); 

        public Creature[,] Move(Creature creature, int loc, Creature[,] gameBoard)
        {
            //bool canMove = MoveTest(creature, loc);
            //move the character if they are allowed to move there(loc)
            return gameBoard;
        }

        

        public bool Attack(Creature creature1, Creature creature2)
        {
            int score1 ,score2 = 0;
            bool attack = false;

            //run the attack of creature1 on creature2, return if it succeeded or not
            for (int roll = 1; roll <= 20; roll++)
            {
                if (creature1.Type == 0)

                {
                    score1 = roll + creature1.Level + cc.GetBaseAttack(creature1);
                    score2 = mc.GetBaseDefense(creature2) + creature2.Level;
                }
                else
                {
                    score1 = roll + creature2.Level + mc.GetBaseAttack(creature2);
                    score2 = cc.GetBaseDefense(creature1) + creature1.Level;
                }

                if (score1 > score2)
                {
                     attack = true;
                    
                }
                return attack;
                //creature1 score = roll(1-20) + level + (base attack + attack boosts)
                //creature2 score = (base defense + defense boosts) + level
                //attack is successful(true) if creature1 score > creature2 score
            }
            return attack;
        }

        public int DoDamage(Creature creature, int baseDamage)
        {
            mc.GetBaseDamage(creature);
            cc.GetBaseDamage(creature);
            //getBaseDamage(creature)
            //get how much damage the character/monster will do, use getBaseDamage(creature) and a random value
            //    int TotalDamage = GetBaseDamage(creature) + baseDamage;

            //    //final damage done will be weapon damage + 1/4 level, baseDamage, + a roll(1-20)
            int finalDamage = 0;
            //    for (int roll =1; roll >=20; roll ++)
            //    {
            //        finalDamage = GetBaseDamage.creature.weapondamage + (1 / 4) * creature.Level * baseDamage + roll;
            //    }
            return finalDamage;
        }

        public Creature AutoTarget(int side)
        {
            Creature c = new Creature();
            return c;
        }
    }
}
