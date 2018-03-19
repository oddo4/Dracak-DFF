using MainDFF.Interface.BattleBehavior;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainDFF.Classes.Battle.MagicBehaviors
{
    public class ThunderMagicBehavior : IAttackBehavior
    {
        public string Name { get; set; }
        public int Cost { get; set; }
        public bool IsUsableSkill { get; set; }

        public int Action(ACharacter attacker, ACharacter defender)
        {
            var attack = attacker.CharacterStats.MAGATK * attacker.CharacterStats.RESTHUNDER;
            var defense = defender.CharacterStats.MAGDEF * defender.CharacterStats.RESTHUNDER;

            var damage = (int)Math.Round((attack - defense));

            if (damage <= 0)
            {
                damage = 1;
            }
            else if (defender.CharacterStats.RESTHUNDER >= 2)
            {
                damage = 0;
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
        public ThunderMagicBehavior()
        {
            Cost = 20;
        }
        public ThunderMagicBehavior(string name)
        {
            Name = name;
        }
    }
}
