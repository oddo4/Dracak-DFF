using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MainDFF
{
    public class MainMenuSelectAction : AMenuSelectAction
    {
        public Page NavigateToPage = null;
        public int GetCursor(Key key, int current, int max, Window window = null)
        {
            switch (key)
            {
                case Key.Up:
                    return MoveCursor(0, current, max);
                case Key.Down:
                    return MoveCursor(1, current, max);
                /// <summary>
                /// Window resolution change
                /// </summary>
                case Key.NumPad0:
                    window.Width = 640;
                    window.Height = 480;
                    break;
                case Key.NumPad1:
                    window.Width = 1024;
                    window.Height = 768;
                    break;
                case Key.Enter:
                    ConfirmSelection(current);
                    return -2;
                default:
                    return current;
            }

            return -1;
        }
        private void ConfirmSelection(int current)
        {
            switch (current)
            {
                case 0:
                    NavigateToPage = new LevelSelectPage();
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                default:
                    break;
            }
        }
    }
}
