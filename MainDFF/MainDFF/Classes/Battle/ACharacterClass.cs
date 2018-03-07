using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainDFF.Classes.Battle
{
    public abstract class ACharacterClass
    {
        public string Name { get; set; }
        public abstract void SetStats(CharacterStats stats, Random rand);
        public abstract void UpgradeStats(CharacterStats stats, Random rand);
    }
}
