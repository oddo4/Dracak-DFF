using MainDFF.Classes.Battle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainDFF.Interface.BattleBehavior
{
    public interface ISupportBehavior : IBehavior
    {
        void SetMaxHP(ACharacter defender);
    }
}
