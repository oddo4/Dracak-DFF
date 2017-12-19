using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FightBehaviour.Interfaces;

namespace FightBehaviour.Behaviors
{
    class BasicAttackBehavior : IAttackBehavior
    {
        private string attackName = "Attack";

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

        public int Attack(Classes.Character attacker, Classes.Character target)
        {
            int damage = (int)Math.Floor(attacker.Stats.ATK) - (int)Math.Floor(target.Stats.DEF);

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

            return damage;
        }
    }
}
