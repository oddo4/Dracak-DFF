using MainDFF.Classes.Battle;
using MainDFF.Interface.BattleBehavior;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainDFF.Interface
{
    public interface IBehavior : IUsableSkill
    {
        string Name { get; set; }
        int Cost { get; set; }
        int Action(ACharacter attacker, ACharacter defender);
    }
}
