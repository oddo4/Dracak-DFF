using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FightBehaviour.Interfaces;
using FightBehaviour.Behaviors;

namespace FightBehaviour.Classes
{
    class Character
    {
        public string Name;
        public int Lives;
        public CharacterStats Stats;
        private IAttackBehavior attackBehavior = new BasicAttackBehavior();

        public IAttackBehavior AttackBehavior
        {
            get { return attackBehavior; }
            set { attackBehavior = value; }
        }

        public int AttackCommand(Character attacker, Character target)
        {
            return AttackBehavior.Attack(attacker, target);
        }

        public override string ToString()
        {
            return this.Name;
        }


    }
}
