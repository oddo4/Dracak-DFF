using MainDFF.Classes.Exploration.Storyboards;
using MainDFF.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MainDFF.Classes.ControlActions.MoveActions
{
    public class PlayerMoveAction : AMoveAction
    {
        public Key Direction { get; set; }
        public PlayerMoveAction(Point pos)
        {
            StoryboardAnimation = new PlayerExploreStoryboardAnimation();
            Pos = pos;
        }
        public override int GetDirection(Key key, int max)
        {
            
            switch(key)
            {
                case Key.Up:
                    if (Pos.Y != MoveCharacter(0, (int)Pos.Y, max))
                    {
                        SetLastPos();
                        Pos.Y = MoveCharacter(0, (int)Pos.Y, max);
                        Direction = key;
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                case Key.Down:
                    if (Pos.Y != MoveCharacter(1, (int)Pos.Y, max))
                    {
                        SetLastPos();
                        Pos.Y = MoveCharacter(1, (int)Pos.Y, max);
                        Direction = key;
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                case Key.Left:
                    if (Pos.X != MoveCharacter(0, (int)Pos.X, max))
                    {
                        SetLastPos();
                        Pos.X = MoveCharacter(0, (int)Pos.X, max);
                        Direction = key;
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                case Key.Right:
                    if (Pos.X != MoveCharacter(1, (int)Pos.X, max))
                    {
                        SetLastPos();
                        Pos.X = MoveCharacter(1, (int)Pos.X, max);
                        Direction = key;
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                case Key.Enter:
                    return -2;
                case Key.Back:
                    NavigateToPage = new PartyMenuPage();
                    return -3;
                default:
                    return 0;
            }
            return -1;
        }
    }
}
