using MainDFF.Interface.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainDFF.Classes.Items.Equipable.Armors
{
    class Armor : AArmor, IStatDEF
    {
        public double DEFValue { get; set; }
    }
}
