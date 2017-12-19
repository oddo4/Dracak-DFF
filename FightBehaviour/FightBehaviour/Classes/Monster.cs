using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FightBehaviour.Behaviors;

namespace FightBehaviour.Classes
{
    class Monster : Character
    {
        public Monster(string name)
        {
            this.Name = name;
            this.Stats = new CharacterStats(
                300, //HP 
                30, //MP
                30, //ATK
                10, //DEF
                5, //MAG ATK
                5, //MAG DEF
                10, //SPD
                1.9, //FIRE R
                -1.9, //ICE R
                1, //THUNDER R
                1 //WATER R
                );
            this.Lives = (int)Stats.HP;
        }

        public void LivesCheck()
        {
            if (Lives < (Stats.HP * 0.75))
            {
                AttackBehavior = new SpecialAttackBehavior();
            }
        }
    }
}
