using MainDFF.Interface.BattleBehavior;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainDFF.Classes.Battle.MagicBehaviors
{
    public class FireMagicBehavior : IAttackBehavior
    {
        public string Name { get; set; }
        public int Cost { get; set; }
        public bool IsUsableSkill { get; set; }

        public int Action(ACharacter attacker, ACharacter defender)
        {
            var attack = attacker.CharacterStats.MAGATK * attacker.CharacterStats.RESFIRE;
            var defense = defender.CharacterStats.MAGDEF * defender.CharacterStats.RESFIRE;

            var damage = (int)Math.Round((attack - defense));

            if (damage <= 0)
            {
                damage = 1;
            }
            else if (defender.CharacterStats.RESFIRE >= 2)
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
        public FireMagicBehavior()
        {
            Cost = 20;
        }
        public FireMagicBehavior(string name)
        {
            Name = name;
        }
    }
}
