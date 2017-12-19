using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FightBehaviour.Interfaces;

namespace FightBehaviour.Behaviors
{
    class ScorpionAttackBehavior : IAttackBehavior
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

        public void Attack(Classes.Character attacker, Classes.Character target)
        {
            target.Stats.HP -= (attacker.Stats.ATK * 2) - target.Stats.DEF;
        }
    }
}
