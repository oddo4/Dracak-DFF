using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MainDFF.Classes.ControlActions.MenuActions
{
    class BattlePageSubMenuAction : AMenuSelectAction
    {
        public int CursorRow = 0;
        public override int ConfirmSelection()
        {
            return -2;
        }

        public override int GetDirection(Key key, int max, int min = 0)
        {
            switch (key)
            {
                case Key.Up:
                    SetCursorRow(0, max, min);
                    return MoveCursor(0, max, min);
                case Key.Down:
                    SetCursorRow(1, max, min);
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
        private void SetCursorRow(int direction, int max, int min = 0)
        {
            switch (direction)
            {
                case 0:
                    if (CurrentIndex > min)
                    {
                        CursorRow--;
                    }
                    else
                    {
                        CursorRow = max;
                    }
                    break;
                case 1:
                    if (CurrentIndex < max)
                    {
                        CursorRow++;
                    }
                    else
                    {
                        CursorRow = min;
                    }
                    break;
            }
        }
    }
}
