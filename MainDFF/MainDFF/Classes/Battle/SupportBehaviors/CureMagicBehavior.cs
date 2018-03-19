using MainDFF.Interface.BattleBehavior;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainDFF.Classes.Battle.SupportBehaviors
{
    public class CureMagicBehavior : ISupportBehavior
    {
        public string Name { get; set; }
        public int Cost { get; set; }
        public bool IsUsableSkill { get; set; }

        public int Action(ACharacter attacker, ACharacter defender)
        {
            var recovery = (int)Math.Round(attacker.CharacterStats.MAGATK + attacker.CharacterStats.LVL);

            attacker.CharacterStatus.CurrentMP -= Cost;
            defender.CharacterStatus.CurrentHP += recovery;

            SetMaxHP(defender);

            return recovery;
        }

        public void SetMaxHP(ACharacter defender)
        {
            if (defender.CharacterStatus.CurrentHP > defender.CharacterStats.HP)
            {
                defender.CharacterStatus.CurrentHP = defender.CharacterStats.HP;
            }
        }

        public CureMagicBehavior()
        {
            Cost = 10;
        }
    }
}
