using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainDFF.Classes.Battle.CharacterClass
{
    public class ClassMarksman : ACharacterClass
    {
        public ClassMarksman()
        {
            Name = "Marksman";
        }
        public override void SetStats(CharacterStats stats, Random rand)
        {
            stats.HP -= rand.Next(100, 151);
            stats.MP -= rand.Next(20, 31);
            stats.ATK += rand.Next(-2, 3);
            stats.DEF -= rand.Next(4, 8);
            stats.MAGATK -= rand.Next(4, 8);
            stats.MAGDEF -= rand.Next(4, 8);
            stats.SPD += rand.Next(4, 11);
        }

        public override void UpgradeStats(CharacterStats stats, Random rand)
        {
            throw new NotImplementedException();
        }
    }
}
