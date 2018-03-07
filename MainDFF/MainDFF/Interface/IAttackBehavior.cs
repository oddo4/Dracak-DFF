using MainDFF.Classes.Battle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainDFF.Interface
{
    public interface IAttackBehavior
    {
        string Name { get; set; }
        int Attack(ACharacter attacker, ACharacter defender);
    }
}
