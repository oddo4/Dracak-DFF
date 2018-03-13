using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace MainDFF.Classes.ControlActions.MenuActions
{
    public abstract class AMenuSelectAction
    {
        public Page NavigateToPage = null;
        public int CurrentIndex = 0;
        public virtual int MoveCursor(int direction, int max, int min)
        {
            switch (direction)
            {
                case 0:
                    if (CurrentIndex > min)
                    {
                        return CurrentIndex - 1;
                    }
                    else
                    {
                        return max;
                    }
                case 1:
                    if (CurrentIndex < max)
                    {
                        return CurrentIndex + 1;
                    }
                    else
                    {
                        return min;
                    }
                default:
                    return CurrentIndex;
            }
        }
        public abstract int GetDirection(Key key, int max, int min = 0);
        public abstract int ConfirmSelection();
    }
}
