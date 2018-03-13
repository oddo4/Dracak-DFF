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
        public int Cost { get; set; }
        public bool IsUsable { get; set; }

        public int Action(ACharacter attacker, ACharacter defender)
        {
            var attackerTemperBuff = attacker.CharacterBuffsDebuff.BuffsDebuffsValueList[0];
            var attackerBreakBuff = attacker.CharacterBuffsDebuff.BuffsDebuffsValueList[3];
            var attack = attacker.CharacterStats.ATK + attackerTemperBuff - attackerBreakBuff;

            var defenderProtectBuff = defender.CharacterBuffsDebuff.BuffsDebuffsValueList[2];
            var defenderBreakBuff = defender.CharacterBuffsDebuff.BuffsDebuffsValueList[3];
            var defense = defender.CharacterStats.DEF + defenderProtectBuff - defenderProtectBuff;

            var damage = (int)(attack - defense);
            if (damage <= 0)
            {
                damage = 1;
            }
            defender.CharacterStatus.CurrentHP -= damage;

            return damage;
        }
        public BasicAttackBehavior(string name)
        {
            Name = name;
        }
    }
}
