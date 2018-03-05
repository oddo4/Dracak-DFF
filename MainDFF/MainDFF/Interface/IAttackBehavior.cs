using MainDFF.Classes.Battle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainDFF
{
    public interface IAttackBehavior
    {
        int Attack(ACharacter attacker, ACharacter defender);
    }
}
