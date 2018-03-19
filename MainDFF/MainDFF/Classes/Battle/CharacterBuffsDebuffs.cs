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
        public List<double> BuffsDebuffsValueList = new List<double>();
        public List<int> BuffsDebuffsTurnList = new List<int>();

        public CharacterBuffsDebuffs()
        {
            for (int i = 0; i < 7; i++)
            {
                BuffsDebuffsValueList.Add(0);
                BuffsDebuffsTurnList.Add(0);
            }
        }

        public void SetDefense(CharacterStats stats)
        {
            BuffsDebuffsValueList[0] = stats.DEF;
            BuffsDebuffsTurnList[0] = 1;
        }

        public void CheckBuffsDebuffs()
        {
            for (int i = 0; i < BuffsDebuffsTurnList.Count; i++)
            {
                BuffsDebuffsTurnList[i]--;

                if (BuffsDebuffsTurnList[i] <= 0)
                {
                    BuffsDebuffsValueList[i] = 0;
                    BuffsDebuffsTurnList[i] = 0;
                }
            }
        }
    }
}
