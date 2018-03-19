using MainDFF.Interface.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainDFF.Classes.Items.Equipable.Armors
{
    public class Robe : AArmor, IStatMAGDEF
    {
        public double MAGDEFValue { get; set; }
    }
}
