using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightBehaviour.Interfaces
{
    interface IAttackBehavior
    {
        string AttackName { get; set; }
        int Attack(Classes.Character attacker, Classes.Character target);
    }
}
