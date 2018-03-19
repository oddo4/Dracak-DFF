using MainDFF.Interface.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainDFF.Classes.Items.Equipable.Weapons
{
    public class Staff : AWeapon, IStatMAGATK
    {
        public double MAGATKValue { get; set; }
    }
}
