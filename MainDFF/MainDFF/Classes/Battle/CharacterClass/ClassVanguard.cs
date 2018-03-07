using MainDFF.Classes.Battle;
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
            stats.ATK += rand.Next(2, 5);
            stats.DEF += rand.Next(2, 5);
            stats.MAGATK -= rand.Next(2, 5);
            stats.SPD -= rand.Next(2, 5);
        }

        public override void UpgradeStats(CharacterStats stats, Random rand)
        {
            throw new NotImplementedException();
        }
    }
}
