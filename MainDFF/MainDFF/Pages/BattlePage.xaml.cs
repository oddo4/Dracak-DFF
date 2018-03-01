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
    /// Interakční logika pro BattlePage.xaml
    /// </summary>
    public partial class BattlePage : Page
    {
        BattlePageMenuAction menuAction = new BattlePageMenuAction();
        public BattlePage()
        {
            InitializeComponent();
        }
        private void MenuKey_Loaded(object sender, EventArgs e)
        {
            App.window.KeyDown += MenuKeyDown;
        }

        private void MenuKeyDown(object sender, KeyEventArgs e)
        {
            var selected = menuAction.GetDirection(e.Key, 0);
            if (selected > -1)
            {

            }
            else if (selected == -1)
            {
                Debug.WriteLine("E");
            }
            else if (selected < -1)
            {
                NavigationService.GoBack();
            }
        }
    }
}
