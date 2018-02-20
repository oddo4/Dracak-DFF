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

namespace MainDFF
{
    /// <summary>
    /// Interakční logika pro MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Page
    {
        Window window;
        MainMenuSelectAction menuAction = new MainMenuSelectAction();
        public MainMenu()
        {
            InitializeComponent();
        }
        private void MenuKey_Loaded(object sender, RoutedEventArgs e)
        {
            window = Window.GetWindow(this);
            window.KeyDown += MenuKeyDown;
        }
        private void MenuKeyDown(object sender, KeyEventArgs e)
        {
            var current = Grid.GetRow(MenuCursor);
            var max = gridMenu.RowDefinitions.Count - 1;
            var selected = menuAction.GetCursor(e.Key, current, max, window);
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
                if (menuAction.NavigateToPage != null)
                {
                    NavigationService.Navigate(menuAction.NavigateToPage);
                }
            }
        }
    }
}
