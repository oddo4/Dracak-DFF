﻿using MainDFF.Classes.Battle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainDFF.Interface.BattleBehavior
{
    public interface IAttackBehavior : IBehavior
    {
        void SetZeroHP(ACharacter defender);
    }
}
