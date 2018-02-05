using System;
using System.Collections.Generic;
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
        public MainMenu()
        {
            InitializeComponent();
        }
        private void MenuKeyDown(object sender, RoutedEventArgs e)
        {
            window = Window.GetWindow(this);
            window.KeyDown += MenuKey;
        }
        private void MenuKey(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up:
                    if (Grid.GetRow(MenuCursor) > 0)
                    {
                        Grid.SetRow(MenuCursor, Grid.GetRow(MenuCursor) - 1);
                    }
                    else
                    {
                        Grid.SetRow(MenuCursor, 3);
                    }
                    break;
                case Key.Down:
                    if (Grid.GetRow(MenuCursor) < 3)
                    {
                        Grid.SetRow(MenuCursor, Grid.GetRow(MenuCursor) + 1);
                    }
                    else
                    {
                        Grid.SetRow(MenuCursor, 0);
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
