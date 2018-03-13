using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MainDFF.Classes.ControlActions.MenuActions
{
    class BattlePageMenuAction : AMenuSelectAction
    {
        public override int ConfirmSelection()
        {
            return -2;
        }

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
    }
}
