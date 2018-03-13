using MainDFF.Classes.Exploration.EnemySettings;
using MainDFF.Classes.Exploration.Storyboards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MainDFF.Classes.ControlActions.MoveActions
{
    class EnemyMoveAction : AMoveAction
    {
        public EnemyMoveSettings MoveSettings;
        public EnemyMoveAction(Point pos, Random rand)
        {
            StoryboardAnimation = new EnemyExploreStoryboardAnimation();
            MoveSettings = new EnemyMoveSettings(rand);
            Pos = pos;
        }
        public override int GetDirection(Key key, int max)
        {
            SetLastPos();
            switch (key)
            {
                case Key.Up:        
                    Pos.Y = MoveCharacter(0, (int)Pos.Y, max);
                    return 1;
                case Key.Down:
                    Pos.Y = MoveCharacter(1, (int)Pos.Y, max);
                    return 1;
                case Key.Left:
                    Pos.X = MoveCharacter(0, (int)Pos.X, max);
                    return 1;
                case Key.Right:
                    Pos.X = MoveCharacter(1, (int)Pos.X, max);
                    return 1;
                default:
                    return 0;
            }
            return -1;
        }
    }
}
