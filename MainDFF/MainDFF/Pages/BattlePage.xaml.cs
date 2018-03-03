using MainDFF.Classes.Battle;
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
        AMenuSelectAction menuAction = new BattlePageMenuAction();
        AMenuSelectAction lastAction;
        SetCharacterOnField setOnField = new SetCharacterOnField();
        CharactersLists characterLists = new CharactersLists();
        public BattlePage()
        {
            InitializeComponent();
            characterLists.PlayerList.Add(new PlayerCharacter() { FileID = 15 });
            setOnField.SetPlayerOnField(characterLists.PlayerList, PlayerField);
            //SkillMenu.SelectedIndex = 0;
        }
        private void MenuKey_Loaded(object sender, EventArgs e)
        {
            App.window.KeyDown += MenuKeyDown;
        }

        private void MenuKeyDown(object sender, KeyEventArgs e)
        {
            var max = SetMax(e.Key);
            var selected = menuAction.GetDirection(e.Key, max);
            if (selected > -1)
            {
                MenuSelect(selected, e.Key);
            }
            else if (selected == -1)
            {
                Debug.WriteLine("E");
            }
            else if (selected < -1)
            {
                MenuConfirm(selected);
            }
        }
        private void MenuSelect(int selected, Key key)
        {
            menuAction.CurrentIndex = selected;

            if (menuAction is BattlePageMenuAction)
            {
                Grid.SetRow(BattleMenuCursor, selected);
            }
            else if (menuAction is BattlePageSubMenuAction)
            {
                var row = ((BattlePageSubMenuAction)menuAction).CursorRow;

                if ( (row < 0 && selected != SubMenu.Items.Count - 1) || (row > 4 && selected == 0))
                {
                    Grid.SetRow(SubMenuCursor, 0);
                    ((BattlePageSubMenuAction)menuAction).CursorRow = 0;
                }
                else if ((row > 4 && selected != 0) || (row < 0 && selected == SubMenu.Items.Count - 1))
                {
                    Grid.SetRow(SubMenuCursor, 4);
                    ((BattlePageSubMenuAction)menuAction).CursorRow = 4;
                }
                else
                {
                    Grid.SetRow(SubMenuCursor, row);
                }

                SubMenu.SelectedIndex = selected;
                SubMenu.ScrollIntoView(SubMenu.SelectedItem);
            }
            else if (menuAction is BattlePageTargetMenuAction)
            {
                switch (key)
                {
                    case Key.Up:
                    case Key.Down:
                        Grid.SetRow(EnemyMenuCursor, selected);
                        break;
                    case Key.Left:
                    case Key.Right:
                        Grid.SetColumn(EnemyMenuCursor, selected * 2);
                        break;
                }
            }
            else
            {
                Debug.WriteLine("Wut?!");
            }
        }
        private int SetMax(Key key)
        {
            if (menuAction is BattlePageMenuAction)
            {
                return BattleMenu.RowDefinitions.Count - 1;
            }
            else if (menuAction is BattlePageSubMenuAction)
            {
                return SubMenu.Items.Count - 1;
            }
            else if (menuAction is BattlePageTargetMenuAction)
            {
                switch (key)
                {
                    case Key.Up:
                    case Key.Down:
                        menuAction.CurrentIndex = Grid.GetRow(EnemyMenuCursor);
                        return EnemyMenu.RowDefinitions.Count - 1;
                    case Key.Left:
                        menuAction.CurrentIndex = Grid.GetColumn(EnemyMenuCursor) - 1;
                        return EnemyMenu.ColumnDefinitions.Count - 3;
                    case Key.Right:
                        menuAction.CurrentIndex = Grid.GetColumn(EnemyMenuCursor);
                        return EnemyMenu.ColumnDefinitions.Count - 3;
                }
            }

            return 0;
        }
        private void MenuConfirm(int selected)
        {
            if (menuAction is BattlePageMenuAction)
            {
                lastAction = menuAction;
                menuAction = new BattlePageTargetMenuAction();

                EnemyMenuCursor.Visibility = Visibility.Visible;
            }
            else if (menuAction is BattlePageSubMenuAction)
            {

            }
            else if (menuAction is BattlePageTargetMenuAction)
            {

            }
        }
        private void CheckLimitBreak()
        {

        }
    }
}
