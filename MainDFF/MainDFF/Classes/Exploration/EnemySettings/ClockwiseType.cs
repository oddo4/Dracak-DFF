using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MainDFF.Classes.Exploration.EnemySettings
{
    class ClockwiseType : IMoveType
    {
        public Key SwitchDirection(Key Direction)
        {
            switch (Direction)
            {
                case Key.Up:
                    return Direction = Key.Right;
                case Key.Down:
                    return Direction = Key.Left;
                case Key.Left:
                    return Direction = Key.Up;
                case Key.Right:
                    return Direction = Key.Down;
                default:
                    return Key.None;
            }
        }
    }
}
