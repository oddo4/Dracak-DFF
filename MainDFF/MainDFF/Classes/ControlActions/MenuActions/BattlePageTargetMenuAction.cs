﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MainDFF.Classes.ControlActions.MenuActions
{
    class BattlePageTargetMenuAction : AMenuSelectAction
    {
        public override int ConfirmSelection()
        {
            switch (CurrentIndex)
            {
                case 0:
                    return -2;
                case 1:
                    return -2;
                case 2:
                    return -2;
                default:
                    return CurrentIndex;
            }
            return -1;
        }

        public override int GetDirection(Key key, int max, int min = 0)
        {
            switch (key)
            {
                case Key.Up:
                case Key.Left:
                    return MoveCursor(0, max, min);
                case Key.Down:
                case Key.Right:
                    return MoveCursor(1, max, min);
                case Key.Enter:
                    return ConfirmSelection();
                case Key.Back:
                    return -3;
                default:
                    return CurrentIndex;
            }
            return -1;
        }
    }
}
