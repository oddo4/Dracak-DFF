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

namespace MainDFF.Pages.PartyMenuPages
{
    /// <summary>
    /// Interakční logika pro PartySelectPage.xaml
    /// </summary>
    public partial class PartySelectPage : Page
    {
        AMenuSelectAction lastAction;
        AMenuSelectAction menuAction = new PartySelectAction();
        SwitchCharacterImage switchImage = new SwitchCharacterImage();
        public PartySelectPage(bool NewGame)
        {
            InitializeComponent();
            if (NewGame)
            {
                App.dataFileLists.playerCurrentPartyIDList = new List<string>() { "01", "07", "10" };
                menuAction.NavigateToPage = new MainMenuPage(true);
            }
            switchImage.LoadCharacters(PartyMembers, 0);
        }
        private void MenuKey_Loaded(object sender, RoutedEventArgs e)
        {
            App.window.KeyDown += MenuKeyDown;
        }
        private void MenuKeyDown(object sender, KeyEventArgs e)
        {
            var max = SetMax();
            var selected = GetSelected(e.Key, max);
            if (selected > -1)
            {
                MenuSelect(selected);
            }
            else if (selected == -1)
            {
                Debug.WriteLine("E3101");
            }
            else
            {
                MenuConfirm(selected);
            }
        }
        private void MenuSelect(int selected)
        {
            if (menuAction is PartySelectAction)
            {
                for (int i = 0; i < PartyMembers.Children.Count; i++)
                {
                    var grid = (Grid)PartyMembers.Children[i];
                    var canvas = (Canvas)grid.Children[0];
                    var image = (Image)canvas.Children[0];

                    if (i == selected)
                    {
                        Canvas.SetLeft(image, 0);
                    }
                    else
                    {
                        Canvas.SetLeft(image, -146);
                    }
                }
                menuAction.CurrentIndex = selected;
            }
            else if (menuAction is PartySwitchCharacterAction)
            {
                if (selected == 0)
                {
                    selected++;
                }

                var grid = (Grid)PartyMembers.Children[lastAction.CurrentIndex];
                var canvas = (Canvas)grid.Children[0];
                var image = (Image)canvas.Children[0];

                var player = App.dataFileLists.playerBasicData[selected];

                switchImage.SetCharacterImage(grid, player);

                menuAction.CurrentIndex = selected;

                if (!CheckSelectedCharacter(player.CharacterID))
                {
                    Canvas.SetLeft(image, -146);
                }
                else
                {
                    Canvas.SetLeft(image, 0);
                }
            }
        }
        private void MenuConfirm(int selected)
        {
            if (menuAction is PartySelectAction)
            {
                if (selected != -2)
                {
                    NavigateToPage(menuAction.NavigateToPage);
                }
                else
                {
                    lastAction = menuAction;
                    var charID = int.Parse(App.dataFileLists.playerCurrentPartyIDList[lastAction.CurrentIndex]);
                    menuAction = new PartySwitchCharacterAction() { CurrentIndex = charID };
                }
            }
            else if (menuAction is PartySwitchCharacterAction)
            {
                if (selected != -2)
                {
                    var charID = int.Parse(App.dataFileLists.playerCurrentPartyIDList[lastAction.CurrentIndex]);
                    var grid = (Grid)PartyMembers.Children[lastAction.CurrentIndex];
                    var canvas = (Canvas)grid.Children[0];
                    var image = (Image)canvas.Children[0];
                    var player = App.dataFileLists.playerBasicData[charID];

                    switchImage.SetCharacterImage(grid, player);

                    menuAction = lastAction;
                    Canvas.SetLeft(image, 0);
                }
                else
                {
                    var charID = App.dataFileLists.playerBasicData[menuAction.CurrentIndex].CharacterID;
                    if (CheckSelectedCharacter(charID))
                    {
                        App.dataFileLists.playerCurrentPartyIDList[lastAction.CurrentIndex] = charID;

                        menuAction = lastAction;
                    }
                }
            }
        }

        private void NavigateToPage(Page page)
        {
            if (page != null)
            {
                NavigationService.Navigate(page);
            }
            else
            {
                NavigationService.GoBack();
            }

            ResetEvent();
        }

        private bool CheckSelectedCharacter(string charID)
        {
            var currentPlayer = App.dataFileLists.playerCurrentPartyIDList[lastAction.CurrentIndex];
            var playerIDList = App.dataFileLists.playerCurrentPartyIDList.Where(i => i != currentPlayer);
            foreach (string ID in playerIDList)
            {
                if (ID == charID)
                {
                    return false;
                }
            }
            return true;
        }

        private int SetMax()
        {
            if (menuAction is PartySelectAction)
            {
                return PartyMembers.Children.Count - 1;
            }
            else if (menuAction is PartySwitchCharacterAction)
            {
                return App.dataFileLists.playerBasicData.Count - 1;
            }

            return -1;
        }
        private int GetSelected(Key key, int max)
        {
            if (menuAction is PartySelectAction)
            {
                return menuAction.GetDirection(key, max);
            }
            else if (menuAction is PartySwitchCharacterAction)
            {
                return menuAction.GetDirection(key, max, 1);
            }

            return -1;
        }
        private void ResetEvent()
        {
            App.window.KeyDown -= MenuKeyDown;
            menuAction = new PartySelectAction();
        }
    }
}
