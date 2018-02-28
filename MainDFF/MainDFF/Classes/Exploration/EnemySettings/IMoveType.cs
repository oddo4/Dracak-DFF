using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MainDFF.Classes.Exploration.EnemySettings
{
    public interface IMoveType
    {
        Key SwitchDirection(Key Direction);
    }
}
