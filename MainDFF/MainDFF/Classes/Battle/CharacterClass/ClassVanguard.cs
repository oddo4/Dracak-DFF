using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainDFF.Classes.Battle.CharacterClass
{
    class ClassVanguard : ACharacterClass
    {
        public ClassVanguard()
        {
            Name = "Vanguard";
        }
        public override void SetStats(CharacterStats stats, Random rand)
        {
            stats.HP += rand.Next(30, 51);
            stats.MP -= rand.Next(20, 31);
            stats.ATK += rand.Next(6, 13);
            stats.DEF += rand.Next(4, 11);
            stats.MAGATK -= rand.Next(4, 8);
            stats.MAGDEF += rand.Next(-2, 3);
            stats.SPD -= rand.Next(2, 4);
        }

        public override void UpgradeStats(CharacterStats stats, Random rand)
        {
            var lvl = (int)Math.Round(stats.LVL / 2.0);
            stats.HP += (int)Math.Round((double)rand.Next(50, 71) * lvl);
            stats.MP += (int)Math.Round((double)rand.Next(10, 26) * lvl);
            stats.ATK += (int)Math.Round((double)rand.Next(6, 13) * lvl);
            stats.DEF += (int)Math.Round((double)rand.Next(4, 11) * lvl);
            stats.MAGATK += (int)Math.Round((double)rand.Next(4, 6) * lvl);
            stats.MAGDEF += (int)Math.Round((double)rand.Next(4, 8) * lvl);
            stats.SPD += (int)Math.Round((double)rand.Next(1, 3) * lvl);
        }
    }
}
