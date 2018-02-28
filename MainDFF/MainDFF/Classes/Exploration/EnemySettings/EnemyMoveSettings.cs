using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MainDFF.Classes.Exploration.EnemySettings
{
    public class EnemyMoveSettings
    {
        public Key Direction { get; set; }
        public int MaxSteps { get; set; }
        public int StepsCount = 0;
        public IMoveType Type { get; set; }
        public int Time { get; set; }

        public EnemyMoveSettings(Random rand)
        {
            Dictionary<int, Key> KeyDirection = new Dictionary<int, Key>() { { 0, Key.Up }, { 1, Key.Down }, { 2, Key.Left }, { 3, Key.Right } };
            Dictionary<int, IMoveType> MoveType = new Dictionary<int, IMoveType>() { { 0, new LinearType() }, { 1, new ClockwiseType() }, { 2, new CounterClockwiseType() } };
            Direction = KeyDirection[rand.Next(0, KeyDirection.Count)];
            MaxSteps = rand.Next(2, 4);
            Type = MoveType[rand.Next(0, MoveType.Count)];
            Time = rand.Next(1, 2);
        }
        public bool CheckSteps(int timer)
        {
            if (timer % Time != 0)
            {
                return true;
            }
            else if (StepsCount == MaxSteps)
            {
                Direction = Type.SwitchDirection(Direction);
                StepsCount = 0;
                return true;
            }
            return false;
        }
    }
}
