﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MainDFF.Pages;
using MainDFF.Pages.PartyMenuPages;

namespace MainDFF.Classes.ControlActions.MenuActions
{
    public class PartyMenuAction : AMenuSelectAction
    {
        public override int GetDirection(Key key, int max, int min = 0)
        {
            switch (key)
            {
                case Key.Up:
                    return MoveCursor(0, max, min);
                case Key.Down:
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
        public override int ConfirmSelection()
        {
            switch (CurrentIndex)
            {
                case 0:
                    NavigateToPage = new PartyStatusPage();
                    return -2;
                case 1:
                    NavigateToPage = null;
                    return CurrentIndex;
                case 2:
                    NavigateToPage = null;
                    return CurrentIndex;
                case 3:
                    NavigateToPage = null;
                    return CurrentIndex;
                case 4:
                    return -2;
                default:
                    return CurrentIndex;
            }
            return -1;
        }
    }
}
