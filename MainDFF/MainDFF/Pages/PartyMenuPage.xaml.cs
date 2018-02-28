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
    /// Interakční logika pro PartyMenuPage.xaml
    /// </summary>
    public partial class PartyMenuPage : Page
    {
        PartyMenuAction menuAction = new PartyMenuAction();
        public PartyMenuPage()
        {
            InitializeComponent();
        }
        /*public PartyMenuPage()
        {
            InitializeComponent();
        }*/
        private void MenuKey_Loaded(object sender, RoutedEventArgs e)
        {
            App.window.KeyDown += MenuKeyDown;
        }
        private void MenuKeyDown(object sender, KeyEventArgs e)
        {
            menuAction.CurrentIndex = Grid.GetRow(MenuCursor);
            var max = gridPartyMenu.Children.Count - 1;
            var selected = menuAction.GetDirection(e.Key, max);
            if (selected > -1)
            {
                Grid.SetRow(MenuCursor, selected);
            }
            else if (selected == -1)
            {
                Debug.WriteLine("E2101");
            }
            else
            {
                switch (selected)
                {
                    case -2:
                        break;
                    case -3:
                        NavigationService.GoBack();
                        ResetEvent();
                        break;
                    default:
                        break;
                }
            }
        }
        private void ResetEvent()
        {
            App.window.KeyDown -= MenuKeyDown;
            menuAction = new PartyMenuAction();
        }
    }
}
