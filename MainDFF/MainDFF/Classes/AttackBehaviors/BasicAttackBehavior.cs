using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MainDFF.Classes.Battle;

namespace MainDFF.Classes.AttackBehaviors
{
    class BasicAttackBehavior : IAttackBehavior
    {
        public int Attack(ACharacter attacker, ACharacter defender)
        {
            var damage = attacker.CharacterStats.ATK - defender.CharacterStats.DEF;
            defender.CharacterStatus.CurrentHP -= damage;

            return (int)damage;
        }
    }
}
