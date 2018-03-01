﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MainDFF.Classes.ControlActions.MenuActions
{
    class BattlePageMenuAction : AMenuSelectAction
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

        public override int GetDirection(Key key, int max)
        {
            switch (key)
            {
                case Key.Up:
                    return MoveCursor(0, max);
                case Key.Down:
                    return MoveCursor(1, max);
                case Key.Enter:
                    return ConfirmSelection();
                default:
                    return CurrentIndex;
            }
            return -1;
        }
    }
}