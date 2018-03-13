using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MainDFF.Pages;

namespace MainDFF.Classes.ControlActions.MenuActions
{
    public class MainMenuSelectAction : AMenuSelectAction
    {
        public override int GetDirection(Key key, int max, int min = 0)
        {
            switch (key)
            {
                case Key.Left:
                    return MoveCursor(0, max, min);
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
        public override int ConfirmSelection()
        {
            switch (CurrentIndex)
            {
                case 0:
                    NavigateToPage = new LevelSelectPage();
                    return -2;
                case 1:
                    NavigateToPage = new PartyMenuPage();
                    return -2;
                case 2:
                    return -2;
                case 3:
                    App.fileHelper.SaveData();
                    return -2;
                case 4:
                    return -4;
                default:
                    return CurrentIndex;
            }
            return -1;
        }

    }
}
