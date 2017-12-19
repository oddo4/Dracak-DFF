using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FightBehaviour.Interfaces;

namespace FightBehaviour.Behaviors
{
    class SpecialAttackBehavior : IAttackBehavior
    {
        private string attackName = "Scorpion Tail";

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
            int damage = ((int)Math.Floor(attacker.Stats.ATK) * 2) - (int)Math.Floor(target.Stats.DEF);

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
