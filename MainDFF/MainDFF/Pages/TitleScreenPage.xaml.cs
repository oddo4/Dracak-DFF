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
using MainDFF.Classes.ControlActions.MenuActions;

namespace MainDFF.Pages
{
    /// <summary>
    /// Interakční logika pro TitleScreenPage.xaml
    /// </summary>
    public partial class TitleScreenPage : Page
    {
        TitleScreenSelectAction menuAction = new TitleScreenSelectAction();
        public TitleScreenPage()
        {
            InitializeComponent();
        }
        private void MenuKey_Loaded(object sender, RoutedEventArgs e)
        {
            App.window.KeyDown += MenuKeyDown;
        }
        private void MenuKeyDown(object sender, KeyEventArgs e)
        {
            menuAction.CurrentIndex = Grid.GetRow(MenuCursor);
            var max = gridMenu.RowDefinitions.Count - 1;
            var selected = menuAction.GetDirection(e.Key, max);
            if (selected > -1)
            {
                Grid.SetRow(MenuCursor, selected);
            }
            else if (selected == -1)
            {
                Debug.WriteLine("E0001");
            }
            else
            {
                if (selected != -2)
                {
                    App.window.Close();
                }
                else
                {
                    if (menuAction.NavigateToPage != null)
                    {
                        App.MainFrame.NavigationService.Navigate(menuAction.NavigateToPage);
                        ResetEvent();
                    }
                }
            }
        }
        private void ResetEvent()
        {
            App.window.KeyDown -= MenuKeyDown;
            menuAction = new TitleScreenSelectAction();
        }
    }
}
