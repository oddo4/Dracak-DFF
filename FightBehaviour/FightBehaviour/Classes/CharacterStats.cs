using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightBehaviour.Classes
{
    class CharacterStats
    {
        public double HP { get; set; }
        public double MP { get; set; }
        public double ATK { get; set; }
        public double DEF { get; set; }
        public double MAG_ATK { get; set; }
        public double MAG_DEF { get; set; }
        public double SPD { get; set; }
        public double FIR_RES { get; set; }
        public double ICE_RES { get; set; }
        public double TND_RES { get; set; }
        public double WAT_RES { get; set; }

        public CharacterStats(double hp = 0, double mp = 0, double atk = 0, double def = 0, double mag_atk = 0, double mag_def = 0, double spd = 0, double fir_res = 0, double ice_res = 0, double tnd_res = 0, double wat_res = 0)
        {
            HP = hp;
            MP = mp;
            ATK = atk;
            DEF = def;
            MAG_ATK = mag_atk;
            MAG_DEF = mag_def;
            SPD = spd;
            FIR_RES = fir_res;
            ICE_RES = ice_res;
            TND_RES = tnd_res;
            WAT_RES = wat_res;
        }
    }
}
