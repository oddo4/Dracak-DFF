using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MainDFF.Classes.Battle;
using MainDFF.Interface;
using MainDFF.Interface.BattleBehavior;

namespace MainDFF.Classes.Battle.AttackBehaviors
{
    class BasicAttackBehavior : IAttackBehavior
    {
        public string Name { get; set; }
        public int Cost { get; set; }
        public bool IsUsableSkill { get; set; }

        public int Action(ACharacter attacker, ACharacter defender)
        {
            var attackerTemperBuff = attacker.CharacterBuffsDebuff.BuffsDebuffsValueList[0];
            var attackerBreakBuff = attacker.CharacterBuffsDebuff.BuffsDebuffsValueList[3];
            var attack = attacker.CharacterStats.ATK + attackerTemperBuff - attackerBreakBuff;

            var defenderDefenseBuff = defender.CharacterBuffsDebuff.BuffsDebuffsValueList[0];
            var defenderProtectBuff = defender.CharacterBuffsDebuff.BuffsDebuffsValueList[2];
            var defenderBreakBuff = defender.CharacterBuffsDebuff.BuffsDebuffsValueList[3];
            var defense = defender.CharacterStats.DEF + defenderDefenseBuff + defenderProtectBuff - defenderProtectBuff;

            var damage = (int)Math.Round(attack - defense);
            if (damage <= 0)
            {
                damage = 1;
            }
            defender.CharacterStatus.CurrentHP -= damage;

            SetZeroHP(defender);

            return damage;
        }
        public void SetZeroHP(ACharacter defender)
        {
            if (defender.CharacterStatus.CurrentHP <= 0)
            {
                defender.CharacterStatus.CurrentHP = 0;
            }
        }
        public BasicAttackBehavior(string name)
        {
            Name = name;
        }
    }
}
