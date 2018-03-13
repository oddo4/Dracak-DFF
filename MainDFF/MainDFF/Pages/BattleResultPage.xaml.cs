using MainDFF.Classes.ControlActions.MenuActions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MainDFF.Pages
{
    /// <summary>
    /// Interakční logika pro BattleResultPage.xaml
    /// </summary>
    public partial class BattleResultPage : Page
    {
        BattleResultPageMenuAction menuAction = new BattleResultPageMenuAction();
        public BattleResultPage()
        {
            InitializeComponent();
        }
        private void MenuKey_Loaded(object sender, RoutedEventArgs e)
        {
            App.window.KeyDown += MenuKeyDown;
        }
        private void MenuKeyDown(object sender, KeyEventArgs e)
        {
            //menuAction.CurrentIndex = Grid.GetRow(MenuCursor);
            var max = 0;
            var selected = menuAction.GetDirection(e.Key, max);
            if (selected > -1)
            {
                
            }
            else if (selected == -1)
            {
                Debug.WriteLine("E0001");
            }
            else
            {
                switch (selected)
                {
                    case -2:
                        if (menuAction.NavigateToPage != null)
                        {
                            NavigationService.Navigate(menuAction.NavigateToPage);
                            ResetEvent();
                        }
                        break;
                    case -3:
                        App.window.Close();
                        break;
                    default:
                        break;
                }
            }
        }
        private void ResetEvent()
        {
            App.window.KeyDown -= MenuKeyDown;
            menuAction = new BattleResultPageMenuAction();
        }
    }
}
