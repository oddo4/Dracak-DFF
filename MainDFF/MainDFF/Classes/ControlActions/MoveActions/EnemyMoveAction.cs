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
        public EnemySetMovement SetMovement { get; set; }
        public EnemyMoveSettings MoveSettings = new EnemyMoveSettings();
        public EnemyMoveAction(Point pos)
        {
            StoryboardAnimation = new EnemyStoryboardAnimation();
            Pos = pos;
        }
        public override int GetDirection(Key key, int max)
        {
            switch (key)
            {
                case Key.Up:
                    if (Pos.Y != MoveCharacter(0, (int)Pos.Y, max))
                    {
                        Pos.Y = MoveCharacter(0, (int)Pos.Y, max);
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                case Key.Down:
                    if (Pos.Y != MoveCharacter(1, (int)Pos.Y, max))
                    {
                        Pos.Y = MoveCharacter(1, (int)Pos.Y, max);
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                case Key.Left:
                    if (Pos.X != MoveCharacter(0, (int)Pos.X, max))
                    {
                        Pos.X = MoveCharacter(0, (int)Pos.X, max);
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                case Key.Right:
                    if (Pos.X != MoveCharacter(1, (int)Pos.X, max))
                    {
                        Pos.X = MoveCharacter(1, (int)Pos.X, max);
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                case Key.Back:
                    return -3;
                default:
                    return 0;
            }
            return -1;
        }
    }
}
