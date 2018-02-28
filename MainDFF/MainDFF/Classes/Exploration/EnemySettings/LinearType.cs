using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MainDFF.Classes.Exploration.EnemySettings
{
    public class LinearType : IMoveType
    {
        public Key SwitchDirection(Key Direction)
        {
            switch (Direction)
            {
                case Key.Up:
                    return Direction = Key.Down;
                case Key.Down:
                    return Direction = Key.Up;
                case Key.Left:
                    return Direction = Key.Right;
                case Key.Right:
                    return Direction = Key.Left;
                default:
                    return Key.None;
            }
        }
    }
}
