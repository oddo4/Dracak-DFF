using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FightBehaviour.Behaviors;

namespace FightBehaviour.Classes
{
    class Player : Character
    {
        public Player(string name)
        {
            this.Name = name;
            this.Stats = new CharacterStats(
                500, //HP 
                50, //MP
                40, //ATK
                15, //DEF
                20, //MAG ATK
                10, //MAG DEF
                7, //SPD
                1, //FIRE R
                1, //ICE R
                1, //THUNDER R
                1 //WATER R
                );
            this.Lives = (int)Stats.HP;
        }

        public void LivesCheck()
        {
            if (Lives < (int)Math.Floor(Stats.HP * 0.99))
            {
                if (Stats.MP < 30)
                {
                    AttackBehavior = new IceMagicBehavior();
                }
                else
                {
                    AttackBehavior = new FireMagicBehavior();
                }
            }
        }
    }
}
