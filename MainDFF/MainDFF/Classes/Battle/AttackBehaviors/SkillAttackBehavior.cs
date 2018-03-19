using MainDFF.Classes.Battle.CharacterClass;
using MainDFF.Interface;
using MainDFF.Interface.BattleBehavior;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainDFF.Classes.Battle.AttackBehaviors
{
    public class SkillAttackBehavior : IAttackBehavior
    {
        public string Name { get; set; }
        public int Cost { get; set; }
        public bool IsUsableSkill { get; set; }

        public int Action(ACharacter attacker, ACharacter defender)
        {
            var attackerTemperBuff = attacker.CharacterBuffsDebuff.BuffsDebuffsValueList[0];
            var attackerBreakBuff = attacker.CharacterBuffsDebuff.BuffsDebuffsValueList[3];
            var attack = attacker.CharacterStats.ATK + attackerTemperBuff - attackerBreakBuff;

            var defenderProtectBuff = defender.CharacterBuffsDebuff.BuffsDebuffsValueList[2];
            var defenderBreakBuff = defender.CharacterBuffsDebuff.BuffsDebuffsValueList[3];
            var defense = defender.CharacterStats.DEF + defenderProtectBuff - defenderProtectBuff;

            var player = (PlayerCharacter)attacker;

            if (player.CharacterClass is ClassVanguard)
            {
                attack *= 1.25;
            }
            else if (player.CharacterClass is ClassAssassin)
            {
                attack *= 2;
            }
            else if (player.CharacterClass is ClassMarksman)
            {
                attack *= 1.75;
            }
            else
            {
                attack *= 1.5;
            }

            var damage = (int)Math.Round((attack - defense));
            if (damage <= 0)
            {
                damage = 1;
            }

            attacker.CharacterStatus.CurrentMP -= Cost;
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
        public SkillAttackBehavior()
        {
            Cost = 20;
        }
        public SkillAttackBehavior(string name)
        {
            Name = name;
        }
    }
}
