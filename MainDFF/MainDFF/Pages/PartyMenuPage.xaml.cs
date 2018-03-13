using MainDFF.Classes.Battle;
using MainDFF.Classes.ControlActions.MenuActions;
using MainDFF.Classes.PartyMenu;
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
        AMenuSelectAction lastAction;
        AMenuSelectAction menuAction = new PartyMenuAction();
        SwitchCharacterImage switchImage = new SwitchCharacterImage();
        int type;
        public PartyMenuPage(int type = 0)
        {
            InitializeComponent();
            this.type = type;
            switchImage.LoadCharacters(PartyMembers, 1);
            SetLastMenuItem();
        }
        private void MenuKey_Loaded(object sender, RoutedEventArgs e)
        {
            App.window.KeyDown += MenuKeyDown;
        }
        private void MenuKeyDown(object sender, KeyEventArgs e)
        {
            var max = SetMax();
            var selected = menuAction.GetDirection(e.Key, max);
            if (selected > -1)
            {
                MenuSelect(selected);
            }
            else if (selected == -1)
            {
                Debug.WriteLine("E2101");
            }
            else
            {
                MenuConfirm(selected);
            }
            txtBlkInformation.Text = menuAction.CurrentIndex.ToString();
        }

        private int SetMax()
        {
            if (menuAction is PartyMenuAction)
            {
                return gridPartyMenu.Children.Count - 2;
            }
            else if (menuAction is PartySelectAction)
            {
                return PartyMembers.Children.Count - 1;
            }

            return -1;
        }

        private void MenuSelect(int selected)
        {
            if (menuAction is PartyMenuAction)
            {
                Grid.SetRow(MenuCursor, selected);
                menuAction.CurrentIndex = selected;
            }
            else if(menuAction is PartySelectAction)
            {
                HighlightMember(selected);
                menuAction.CurrentIndex = selected;
            }
            
        }

        private void MenuConfirm(int selected)
        {
            if (menuAction is PartyMenuAction)
            {
                if (selected != -2)
                {
                    NavigationService.GoBack();
                    ResetEvent();
                }
                else
                {
                    if (menuAction.CurrentIndex >= 0 && menuAction.CurrentIndex <= 2)
                    {
                        lastAction = menuAction;

                        menuAction = new PartySelectAction();

                        HighlightMember(menuAction.CurrentIndex);
                    }
                    else
                    {
                        NavigationService.Navigate(menuAction.NavigateToPage);
                        ResetEvent();
                    }
                }
            }
            else if (menuAction is PartySelectAction)
            {
                if (selected != -2)
                {
                    HighlightMember(0, true);
                    menuAction = lastAction;
                }
                else
                {

                }
            }
        }
        private void HighlightMember(int selected, bool all = false)
        {
            for (int i = 0; i < PartyMembers.Children.Count; i++)
            {
                var gridMember = (Grid)PartyMembers.Children[i];
                var canvas = (Canvas)gridMember.Children[0];
                var image = (Image)canvas.Children[0];

                if (i == selected)
                {
                    Canvas.SetLeft(image, 0);
                }
                else
                {
                    Canvas.SetLeft(image, -146);
                }
                if (all)
                {
                    selected++;
                }
            }
        }
        private void SetLastMenuItem()
        {
            var lastItem = (TextBlock)gridPartyMenu.Children[gridPartyMenu.Children.Count - 1];

            switch (type)
            {
                case 0:
                    lastItem.Text = "Member";
                    break;
                case 1:
                    lastItem.Text = "Abort";
                    break;
            }
        }

        private void ResetEvent()
        {
            App.window.KeyDown -= MenuKeyDown;
            menuAction = new PartyMenuAction();
        }
    }
}
