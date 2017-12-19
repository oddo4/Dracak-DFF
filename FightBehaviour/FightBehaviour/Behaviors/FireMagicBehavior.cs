using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FightBehaviour.Classes;
using FightBehaviour.Interfaces;

namespace FightBehaviour.Behaviors
{
    class FireMagicBehavior : IAttackBehavior
    {
        private string attackName = "Fire";
        public string AttackName
        {
            get
            {
                return attackName;
            }
            set
            {
                attackName = value;
            }
        }

        public int Attack(Character attacker, Character target)
        {
            int damage;
            if (target.Stats.FIR_RES >= 2)
            {
                damage = 0;
            }
            else
            {
                damage = (int)Math.Floor(attacker.Stats.MAG_ATK) - ((int)Math.Floor(target.Stats.MAG_DEF) * (int)Math.Floor(target.Stats.FIR_RES));

                if (damage < 0)
                {
                    damage = 0;
                }

                if (target.Lives - damage < 0)
                {
                    target.Lives = 0;
                }
                else
                {
                    target.Lives -= damage;
                }
            }

            if (attacker.Stats.MP - 10 < 0)
            {
                attacker.Stats.MP = 0;
                attacker.AttackBehavior = new BasicAttackBehavior();
            }
            else
            {
                attacker.Stats.MP -= 10;
            }

            return damage;
        }
    }
}
