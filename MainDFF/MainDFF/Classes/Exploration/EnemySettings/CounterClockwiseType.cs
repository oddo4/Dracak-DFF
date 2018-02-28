using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MainDFF.Classes.Exploration.EnemySettings
{
    public class CounterClockwiseType : IMoveType
    {
        public Key SwitchDirection(Key Direction)
        {
            switch (Direction)
            {
                case Key.Up:
                    return Direction = Key.Left;
                case Key.Down:
                    return Direction = Key.Right;
                case Key.Left:
                    return Direction = Key.Down;
                case Key.Right:
                    return Direction = Key.Up;
                default:
                    return Key.None;
            }
        }
    }
}
