using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MainDFF.Pages;

namespace MainDFF.Classes.ControlActions.MenuActions
{
    public class PartySelectAction : AMenuSelectAction
    {
        public override int ConfirmSelection()
        {
            switch(CurrentIndex)
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
                case Key.Left:
                    return MoveCursor(0, max);
                case Key.Right:
                    return MoveCursor(1, max);
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
