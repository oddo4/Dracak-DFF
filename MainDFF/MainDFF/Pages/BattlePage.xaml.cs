using MainDFF.Classes.Battle;
using MainDFF.Classes.ControlActions.MenuActions;
using MainDFF.Classes.FileHelper;
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
using System.Windows.Threading;

namespace MainDFF.Pages
{
    /// <summary>
    /// Interakční logika pro BattlePage.xaml
    /// </summary>
    public partial class BattlePage : Page
    {
        AMenuSelectAction menuAction = null;
        AMenuSelectAction lastAction;
        SetCharacterOnField setOnField = new SetCharacterOnField();
        CharactersLists charactersLists = new CharactersLists();
        DispatcherTimer BattleTimer = new DispatcherTimer();
        public BattlePage(string enemyElement)
        {
            InitializeComponent();
            App.fileHelper.LoadData();
            App.fileHelper.LoadEnemyData(enemyElement);
            charactersLists.PlayerList = App.dataFileLists.AssemblePartyCharacter();
            charactersLists.EnemyList = App.dataFileLists.AssembleEnemyCharacter();
            charactersLists.CreateOrder();
            charactersLists.PlayerList[0].CharacterStatus.CurrentHP -= 20;
            setOnField.SetPlayerOnField(charactersLists.PlayerList, PlayerField, PlayerMenu);
            setOnField.SetEnemyOnField(charactersLists.EnemyList, MonsterField, EnemyMenu);
            setOnField.SetCharacterOrder(charactersLists, CharacterOrder); 
            ShowHP();

            BattleTimer = new DispatcherTimer();
            BattleTimer.Interval = new TimeSpan(0, 0, 1);
            BattleTimer.Tick += (sender, args) => { CheckTurn(); };
            BattleTimer.Start();
        }
        private void MenuKey_Loaded(object sender, EventArgs e)
        {
            App.window.KeyDown += MenuKeyDown;
        }

        private void MenuKeyDown(object sender, KeyEventArgs e)
        {
            if (menuAction != null)
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
                var player = charactersLists.CharacterOrder.First();
                string cursor = Grid.GetRow(EnemyMenuCursor).ToString() + Grid.GetColumn(EnemyMenuCursor).ToString();
                if (CheckTarget())
                {
                    var enemy = charactersLists.EnemyList[TargetTranslate(cursor)];
                    player.AttackBehavior.Attack(player, enemy);
                    ResetTurn();
                    ShowHP();
                }
            }
        }
        private void CheckTurn()
        {
            if(charactersLists.CharacterOrder.First() is PlayerCharacter)
            {
                PlayerTurn();
            }
            else
            {
                EnemyTurn();
            }
        }
        private bool CheckTarget()
        {
            var children = EnemyMenu.Children;
            for (int i = 2; i < EnemyMenu.Children.Count; i++)
            {
                if (Grid.GetRow((StackPanel)children[i]) == Grid.GetRow(EnemyMenuCursor) && Grid.GetColumn((StackPanel)children[i])-1 == Grid.GetColumn(EnemyMenuCursor) && ((TextBlock)(((StackPanel)children[i]).Children[1])).Text != "")
                {
                    return true;
                }
            }
            return false;
        }
        private int TargetTranslate(string cursor)
        {
            switch (cursor)
            {
                case "00":
                    return 0;
                case "10":
                    return 1;
                case "20":
                    return 2;
                case "02":
                    return 3;
                case "12":
                    return 4;
                case "22":
                    return 5;
            }
            return -1;
        }
        private void ShowHP()
        {
            for (int i = 0; i < charactersLists.EnemyList.Count; i++)
            {
                var enemy = charactersLists.EnemyList[i];
                var canvas = (Canvas)MonsterField.Children[i];
                var grid = (Grid)canvas.Children[1];
                var txtBlk = (TextBlock)grid.Children[0];
                txtBlk.Text = enemy.CharacterStatus.CurrentHP.ToString();
            }
        }
        private void PlayerTurn()
        {
            BattleMenuCursor.Visibility = Visibility.Visible;

            menuAction = new BattlePageMenuAction();
            BattleTimer.Stop();
        }
        private void EnemyTurn()
        {

        }
        private void ResetTurn()
        {
            BattleMenuCursor.Visibility = Visibility.Hidden;
            PlayerMenuCursor.Visibility = Visibility.Hidden;
            EnemyMenuCursor.Visibility = Visibility.Hidden;

            menuAction = null;
            ResetOrder();
            BattleTimer.Start();
        }
        private void ResetOrder()
        {
            charactersLists.ReOrder(CharacterOrder);
        }
        private void CheckLimitBreak()
        {

        }
    }
}
