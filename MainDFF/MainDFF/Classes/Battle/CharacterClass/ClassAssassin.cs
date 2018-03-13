using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainDFF.Classes.Battle.CharacterClass
{
    public class ClassAssassin : ACharacterClass
    {
        public ClassAssassin()
        {
            Name = "Assassin";
        }
        public override void SetStats(CharacterStats stats, Random rand)
        {
            stats.HP += rand.Next(-20, 20);
            stats.MP += rand.Next(-10, 11);
            stats.ATK -= rand.Next(4, 8);
            stats.DEF += rand.Next(-2, 3);
            stats.MAGATK -= rand.Next(4, 8);
            stats.MAGDEF += rand.Next(-2, 3);
            stats.SPD += rand.Next(4, 11);
        }

        public override void UpgradeStats(CharacterStats stats, Random rand)
        {
            throw new NotImplementedException();
        }
    }
}
