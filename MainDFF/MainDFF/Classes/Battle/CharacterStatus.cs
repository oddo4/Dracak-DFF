﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainDFF.Classes.Battle
{
    public class CharacterStatus
    {
        public double CurrentHP { get; set; }
        public double CurrentMP { get; set; }
        public double CurrentSP { get; set; }

        public CharacterStatus(CharacterStats stats)
        {
            this.CurrentHP = stats.HP;
            this.CurrentMP = stats.MP;
            this.CurrentSP = 0;
        }
    }
}
