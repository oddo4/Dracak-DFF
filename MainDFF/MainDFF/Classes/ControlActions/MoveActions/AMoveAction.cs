﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MainDFF.Classes.ControlActions.MoveActions
{
    public abstract class AMoveAction
    {
        public Point BegPos = new Point(131, 77);
        public Point Pos = new Point();
        public SpriteAnimation SpriteAnimation = new SpriteAnimation();
        public PlayerStoryboardAnimation PlayerStoryboard = new PlayerStoryboardAnimation();
        public int MoveCharacter(int direction, int current, int max)
        {
            switch (direction)
            {
                case 0:
                    if (current - 1 >= 0 && current - 1 <= max)
                    {
                        return current - 1;
                    }
                    else
                    {
                        return current;
                    }
                case 1:
                    if (current + 1 >= 0 && current + 1 <= max)
                    {
                        return current + 1;
                    }
                    else
                    {
                        return current;
                    }
                default:
                    return current;
            }
        }
        public abstract int GetDirection(Key key, int max);
    }
}