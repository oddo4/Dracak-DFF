using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MainDFF.Pages;
using MainDFF.Pages.PartyMenuPages;

namespace MainDFF.Classes.ControlActions.MenuActions
{
    public class TitleScreenSelectAction : AMenuSelectAction
    {
        public override int GetDirection(Key key, int max, int min = 0)
        {
            switch (key)
            {
                case Key.Up:
                    return MoveCursor(0, max, min);
                case Key.Down:
                    return MoveCursor(1, max, min);
                /// <summary>
                /// Window resolution change
                /// </summary>
                /*case Key.NumPad0:
                    window.Width = 640;
                    window.Height = 480;
                    break;
                case Key.NumPad1:
                    window.Width = 1024;
                    window.Height = 768;
                    break;*/
                case Key.Enter:
                    return ConfirmSelection();
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
                    NavigateToPage = new PartySelectPage(true);
                    return -2;
                case 1:
                    App.fileHelper.LoadData();
                    NavigateToPage = new MainMenuPage();
                    return -2;
                case 2:
                    return -2;
                case 3:
                    return -3;
                default:
                    return CurrentIndex;
            }
            return -1;
        }
    }
}
