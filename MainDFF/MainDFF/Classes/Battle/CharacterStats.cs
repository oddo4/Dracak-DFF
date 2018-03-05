using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MainDFF.Classes.Battle
{
    public class CharacterStats
    {
        public int LVL { get; set; }
        public int CurrentEXP { get; set; }
        public int NextEXP { get; set; }
        public double HP { get; set; }
        public double MP { get; set; }
        public double SP { get; set; }
        public double ATK { get; set; }
        public double DEF { get; set; }
        public double MAGATK { get; set; }
        public double MAGDEF { get; set; }
        public double SPD { get; set; }
        public double RESFIRE { get; set; }
        public double RESICE { get; set; }
        public double RESTHUNDER { get; set; }
        public double RESWATER { get; set; }
        public CharacterStats(
            double HP,
            double MP,
            double SP,
            double ATK, 
            double DEF, 
            double MAGATK, 
            double MAGDEF, 
            double SPD, 
            double RESFIRE, 
            double RESICE, 
            double RESTHUNDER, 
            double RESWATER 
            )
        {
            this.HP = HP;
            this.MP = MP;
            this.SP = SP;
            this.ATK = ATK;
            this.DEF = DEF;
            this.MAGATK = MAGATK;
            this.MAGDEF = MAGDEF;
            this.SPD = SPD;
            this.RESFIRE = RESFIRE;
            this.RESICE = RESICE;
            this.RESTHUNDER = RESTHUNDER;
            this.RESWATER = RESWATER;
        }
    }
}