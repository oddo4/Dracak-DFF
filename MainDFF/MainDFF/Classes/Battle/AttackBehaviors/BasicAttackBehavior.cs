using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MainDFF.Classes.Battle;
using MainDFF.Interface;

namespace MainDFF.Classes.Battle.AttackBehaviors
{
    class BasicAttackBehavior : IAttackBehavior
    {
        public string Name { get; set; }

        public int Attack(ACharacter attacker, ACharacter defender)
        {
            var damage = attacker.CharacterStats.ATK - defender.CharacterStats.DEF;
            defender.CharacterStatus.CurrentHP -= damage;

            return (int)damage;
        }
        public BasicAttackBehavior(string name)
        {
            Name = name;
        }
    }
}
