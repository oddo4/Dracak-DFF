using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainDFF.Classes.Battle
{
    public class CharacterBuffsDebuffs
    {
        /// <summary>
        /// 0 - Defense
        /// 1 - Temper
        /// 2 - Protect
        /// 3 - Break
        /// 4 - Haste
        /// 5 - Slow
        /// 6 - Regen
        /// </summary>
        public List<int> BuffsDebuffsValueList = new List<int>();
        public List<int> BuffsDebuffsTurnList = new List<int>();

        public CharacterBuffsDebuffs()
        {
            for (int i = 0; i < 7; i++)
            {
                BuffsDebuffsValueList.Add(1);
                BuffsDebuffsTurnList.Add(0);
            }
        }
    }
}
