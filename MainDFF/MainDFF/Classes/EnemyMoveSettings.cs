using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MainDFF.Classes
{
    public class EnemyMoveSettings
    {
        public Key Direction { get; set; }
        public int MaxSteps { get; set; }
        public int StepsCount = 0;
        public int Type { get; set; }
        public int Time { get; set; }

        public EnemyMoveSettings()
        {
            Direction = Key.Down;
            MaxSteps = 4;
            Type = 0;
            Time = 2;
        }
        public bool CheckSteps(int timer)
        {
            if (StepsCount == MaxSteps)
            {
                SwitchDirection();
                return true;
            }
            else if (timer % Time != 0)
            {
                return true;
            }
            return false;
        }
        public void SwitchDirection()
        {
            switch (Direction)
            {
                case Key.Up:
                    Direction = Key.Down;
                    break;
                case Key.Down:
                    Direction = Key.Up;
                    break;
                case Key.Left:
                    Direction = Key.Right;
                    break;
                case Key.Right:
                    Direction = Key.Left;
                    break;
                default:
                    break;
            }
            StepsCount = 0;
        }
    }
}
