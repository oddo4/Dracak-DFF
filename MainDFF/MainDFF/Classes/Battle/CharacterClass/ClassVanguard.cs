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
            stats.HP += rand.Next(100, 151);
            stats.MP -= rand.Next(20, 31);
            stats.ATK += rand.Next(6, 13);
            stats.DEF += rand.Next(4, 11);
            stats.MAGATK -= rand.Next(4, 8);
            stats.MAGDEF += rand.Next(-2, 3);
            stats.SPD -= rand.Next(2, 4);
        }

        public override void UpgradeStats(CharacterStats stats, Random rand)
        {
            throw new NotImplementedException();
        }
    }
}
