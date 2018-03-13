using MainDFF.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MainDFF.Classes.ControlActions.MenuActions
{
    public class BattleResultPageMenuAction : AMenuSelectAction
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
                default:
                    return CurrentIndex;
            }

            return -1;
        }

        public override int ConfirmSelection()
        {
            return -1;
        }
    }
}
