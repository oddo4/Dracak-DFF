using MainDFF.Classes;
using MainDFF.Classes.Battle;
using MainDFF.Classes.Battle.AttackBehaviors;
using MainDFF.Classes.ControlActions.MenuActions;
using MainDFF.Classes.FileHelper;
using MainDFF.Classes.Items;
using MainDFF.Interface;
using MainDFF.Interface.BattleBehavior;
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
        AMenuSelectAction mainMenuAction = null;
        AMenuSelectAction lastAction = null;
        SetCharacterOnField setOnField = new SetCharacterOnField();
        DamageInfoVisibility damageInfo;
        DispatcherTimer BattleTimer = new DispatcherTimer();
        DispatcherTimer WinTimer;
        int tick = 0;
        int pause = 0;
        bool PortalKey;
        bool Boss = false;
        public BattlePage(string enemyElement, bool portalKey = false)
        {
            InitializeComponent();
            //App.fileHelper.LoadData();
            PortalKey = portalKey;
            if (enemyElement != "Boss")
            {
                App.fileHelper.LoadEnemyData(enemyElement);
                App.charactersLists.EnemyList = App.dataFileLists.AssembleEnemyCharacter();
            }
            else
            {
                App.fileHelper.LoadEnemyBossData();
                App.charactersLists.EnemyList = App.dataFileLists.AssembleBossEnemyCharacter(App.dataFileLists.CompletedChapters);
                Boss = true;
            }
            App.charactersLists.CreateOrder();

            setBackground(enemyElement);
            setOnField.SetPlayerOnField(App.charactersLists.PlayerList, PlayerField, PlayerMenu);
            setOnField.SetEnemyOnField(App.charactersLists.EnemyList, MonsterField, EnemyMenu, Boss);
            setOnField.SetCharacterOrder(App.charactersLists, CharacterOrder);

            BattleTimer = new DispatcherTimer(DispatcherPriority.Send);
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

                if ((row < 0 && selected != SubMenu.Items.Count - 1) || (row > 4 && selected == 0))
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
                txtBlkInformation.Text = menuAction.CurrentIndex.ToString();
            }
            else if (menuAction is BattlePagePlayerMenuAction)
            {
                Grid.SetRow(PlayerMenuCursor, selected);
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
                if(CheckLimitBreak())
                {
                    return BattleMenu.RowDefinitions.Count - 1;
                }
                return BattleMenu.RowDefinitions.Count - 2;
            }
            else if (menuAction is BattlePageSubMenuAction)
            {
                return SubMenu.Items.Count - 1;
            }
            else if (menuAction is BattlePagePlayerMenuAction)
            {
                return PlayerMenu.RowDefinitions.Count - 1;
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
            SwitchMenuAction switchMenu = new SwitchMenuAction();
            if (menuAction is BattlePageMenuAction)
            {
                if (selected == -2)
                {
                    var player = App.charactersLists.CharacterOrder.First();
                    var behaviorList = player.BehaviorList;

                    mainMenuAction = menuAction;
                    lastAction = menuAction;
                    if (menuAction.CurrentIndex == 2)
                    {
                        PlayerAction(player);
                    }
                    else
                    {
                        menuAction = switchMenu.GetMenuAction(mainMenuAction.CurrentIndex, EnemyMenuCursor, SubMenuGrid, behaviorList, null, player);
                    }
                }
            }
            else if (menuAction is BattlePageSubMenuAction)
            {
                if (selected != -2)
                {
                    SubMenuGrid.Visibility = Visibility.Hidden;

                    menuAction = mainMenuAction;
                }
                else
                {
                    var player = App.charactersLists.CharacterOrder.First();
                    var behaviorIndex = SubMenu.SelectedIndex + 2;
                    var behavior = player.BehaviorList[behaviorIndex];
                    if (player.CharacterStatus.CurrentMP >= behavior.Cost)
                    {
                        lastAction = menuAction;
                        menuAction = switchMenu.GetSubMenuAction(behavior, EnemyMenuCursor, PlayerMenuCursor);
                    }
                    
                }
            }
            else if (menuAction is BattlePagePlayerMenuAction)
            {
                if (selected != -2)
                {
                    PlayerMenuCursor.Visibility = Visibility.Hidden;

                    menuAction = lastAction;
                }
                else
                {
                    var player = (PlayerCharacter)App.charactersLists.CharacterOrder.First();
                    var target = App.charactersLists.PlayerList[menuAction.CurrentIndex];
                    if (CheckPlayerTarget(target))
                    {
                        PlayerAction(player);
                    }
                }
            }
            else if (menuAction is BattlePageTargetMenuAction)
            {
                if (selected != -2)
                {
                    EnemyMenuCursor.Visibility = Visibility.Hidden;

                    menuAction = lastAction;
                }
                else
                {
                    var player = (PlayerCharacter)App.charactersLists.CharacterOrder.First();
                    if (CheckEnemyTarget())
                    {
                        PlayerAction(player);
                    }
                } 
            }
            else
            {
                Debug.WriteLine("Here!");
            }
        }

        private void PlayerAction(ACharacter activePlayer)
        {
            var player = (PlayerCharacter)activePlayer;
            string enemyCursor = Grid.GetRow(EnemyMenuCursor).ToString() + Grid.GetColumn(EnemyMenuCursor).ToString();

            if (lastAction is BattlePageMenuAction)
            {
                var playerRow = App.charactersLists.GetPlayerRow(player);
                var enemy = App.charactersLists.EnemyList[TargetTranslate(enemyCursor)];
                damageInfo = new DamageInfoVisibility(enemy);
                var playerCanvas = (Canvas)PlayerField.Children[playerRow];
                Grid.SetRow(playerCanvas, Grid.GetRow(EnemyMenuCursor));
                Grid.SetColumn(playerCanvas, 0);

                int damage = -1;

                if (lastAction.CurrentIndex == 0)
                {
                    var behavior = player.BehaviorList[0];

                    damage = behavior.Action(player, enemy);
                    player.SwitchAnimation(playerCanvas, 2, App.resourcePaths.GetPlayerPath(player.CharacterID), damageInfo.DamageTimer, 0, false);
                    ShowEnemyDamage(damage.ToString(), enemy);
                }
                else if (lastAction.CurrentIndex == 2)
                {
                    Grid.SetRow(playerCanvas, App.charactersLists.GetPlayerRow(player));
                    Grid.SetColumn(playerCanvas, 1);
                    player.CharacterBuffsDebuff.SetDefense(player.CharacterStats);
                    player.SwitchAnimation(playerCanvas, 7, App.resourcePaths.GetPlayerPath(player.CharacterID), damageInfo.DamageTimer, 0, false);
                    ShowSkillName("Defense Up", true);
                    ShowPlayerDamage("", player);
                }
                else if (lastAction.CurrentIndex == 4)
                {
                    var behavior = player.BehaviorList[1];

                    damage = behavior.Action(player, enemy);
                    ShowSkillName(behavior.Name, true);
                    player.SwitchAnimation(playerCanvas, 4, App.resourcePaths.GetPlayerPath(player.CharacterID), damageInfo.DamageTimer, 0, false);
                    player.CharacterStatus.ResetSP();
                    setOnField.SetPlayerStatus(App.charactersLists.GetPlayerRow(player), player, PlayerMenu);
                    ShowEnemyDamage(damage.ToString(), enemy);
                }

                ResetTurn();
            }
            else if (lastAction is BattlePageSubMenuAction)
            {
                var playerRow = App.charactersLists.GetPlayerRow(player);
                var behaviorList = player.BehaviorList.GetRange(2, player.BehaviorList.Count - 2);
                var playerCanvas = (Canvas)PlayerField.Children[playerRow];
                var behavior = behaviorList[lastAction.CurrentIndex];
                ShowSkillName(behavior.Name, true);
                var damage = 0;
                if (menuAction is BattlePageTargetMenuAction)
                {
                    var enemy = App.charactersLists.EnemyList[TargetTranslate(enemyCursor)];
                    damageInfo = new DamageInfoVisibility(enemy);
                    Grid.SetRow(playerCanvas, Grid.GetRow(EnemyMenuCursor));
                    Grid.SetColumn(playerCanvas, 0);

                    damage = behavior.Action(player, enemy);

                    if (behavior is SkillAttackBehavior)
                    {
                        player.SwitchAnimation(playerCanvas, 2, App.resourcePaths.GetPlayerPath(player.CharacterID), damageInfo.DamageTimer, 0, false);
                    }
                    else
                    {
                        player.SwitchAnimation(playerCanvas, 3, App.resourcePaths.GetPlayerPath(player.CharacterID), damageInfo.DamageTimer, 4);
                    }

                    setOnField.SetPlayerStatus(App.charactersLists.GetPlayerRow(player), player, PlayerMenu);

                    ShowEnemyDamage(damage.ToString(), enemy);
                }
                else if (menuAction is BattlePagePlayerMenuAction)
                {
                    var target = App.charactersLists.PlayerList[menuAction.CurrentIndex];
                    damageInfo = new DamageInfoVisibility(target);
                    
                    damage = behavior.Action(player, target);

                    player.SwitchAnimation(playerCanvas, 3, App.resourcePaths.GetPlayerPath(player.CharacterID), damageInfo.DamageTimer, 4);

                    setOnField.SetPlayerStatus(App.charactersLists.GetPlayerRow(target), target, PlayerMenu);

                    ShowPlayerDamage(damage.ToString(), target);
                }
                ResetTurn();
            }
        }

        private void CheckCharacterStatus(bool status, ACharacter last)
        {
            if (status)
            {
                if (last is PlayerCharacter)
                {
                    var enemy = (EnemyCharacter)damageInfo.Target;
                    App.charactersLists.CharacterOrder.Remove(enemy);
                    setOnField.DeleteEnemyElement(enemy, App.charactersLists.EnemyList, MonsterField, EnemyMenu);
                    setOnField.SetCharacterOrder(App.charactersLists, CharacterOrder);
                }
                else if (last is EnemyCharacter)
                {
                    var player = (PlayerCharacter)damageInfo.Target;
                    var playerCanvas = (Canvas)PlayerField.Children[App.charactersLists.GetPlayerRow(player)];
                    player.SwitchAnimation(playerCanvas, 6, App.resourcePaths.GetPlayerPath(player.CharacterID), null, 0, false);
                    App.charactersLists.CharacterOrder.Remove(player);
                    setOnField.SetCharacterOrder(App.charactersLists, CharacterOrder);
                }
            }

            damageInfo.DamageStop();
        }

        private void CheckTurn()
        {
            var last = App.charactersLists.CharacterOrder.Last();
            if (damageInfo != null)
            {
                CheckCharacterStatus(damageInfo.Target.CharacterStatus.CheckAlive(damageInfo.Target, App.charactersLists.CharacterOrder), last);
            }
            if (App.charactersLists.CheckEnemyAlive())
            { 
                SetIdleLastCharacter(last);
                ShowSkillName();

                var first = App.charactersLists.CharacterOrder.First();
                first.CharacterBuffsDebuff.CheckBuffsDebuffs();
                
                if (first is PlayerCharacter)
                {
                    CheckLimitBreak();
                    first.CharacterBuffsDebuff.CheckBuffsDebuffs();
                    PlayerTurn();
                }
                else if (first is EnemyCharacter)
                {
                    EnemyTurn();
                }
            }
            else
            {
                SetIdleLastCharacter(last);
                BattleTimer.Stop();
                tick = 0;
                pause = 0;
                WinTimer = new DispatcherTimer(DispatcherPriority.Send);
                WinTimer.Interval = new TimeSpan(0, 0, 1);
                WinTimer.Tick += (sender, args) => { WinPose(); };

                var alive = App.charactersLists.GetPlayerAlive();
                var i = 0;
                if (alive[0].CurrentAnimation != 7 && alive[0].CurrentAnimation != 8)
                {
                    foreach (PlayerCharacter player in alive)
                    {
                        var canvas = (Canvas)PlayerField.Children[i];
                        player.SwitchAnimation(canvas, 7, App.resourcePaths.GetPlayerPath(player.CharacterID), WinTimer, 0, false);

                        var animLenght = (int)Math.Ceiling((double)((player.GetCurrentAnimation().SpriteFramesCount) * 75 / 1000)) + tick;

                        if (animLenght > pause)
                        {
                            pause = animLenght;
                        }

                        i++;
                    }

                }
            }

            tick++;
            txtBlkInformation.Text = tick.ToString();
        }

        private void WinPose()
        {
            var alive = App.charactersLists.GetPlayerAlive();
            var i = 0;
            if (tick >= 3)
            {
                WinTimer.Stop();
                App.MainFrame.NavigationService.Navigate(new BattleResultPage(PortalKey, Boss));
                ResetEvent();
            }
            else
            {
                foreach (PlayerCharacter player in alive)
                {
                    var canvas = (Canvas)PlayerField.Children[i];
                    player.SwitchAnimation(canvas, 8, App.resourcePaths.GetPlayerPath(player.CharacterID), WinTimer, 0, true);
                    i++;
                }
            }

            tick++;
        }

        private void ResetEvent()
        {
            App.window.KeyDown -= MenuKeyDown;
            BattleTimer.Stop();
        }
        private bool CheckPlayerTarget(PlayerCharacter target)
        {
            if (target.CharacterStatus.CurrentHP > 0)
            {
                return true;
            }
            return false;
        }

        private bool CheckEnemyTarget()
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
        private void SetIdleLastCharacter(ACharacter c)
        {
            var animID = 0;
            int Row = 0;
            Canvas Canvas = null;
            string path = "";
            if (c is PlayerCharacter)
            {
                var player = (PlayerCharacter)c;
                Row = App.charactersLists.GetPlayerRow(player);
                Canvas = (Canvas)PlayerField.Children[Row];
                path = App.resourcePaths.GetPlayerPath(c.CharacterID);
                Grid.SetRow(Canvas, Row);
                Grid.SetColumn(Canvas, 1);

                if (player.CharacterStatus.CheckDying(player))
                {
                    animID = 5;
                }

            }
            else if (c is EnemyCharacter)
            {
                var enemy = (EnemyCharacter)c;
                Row = App.charactersLists.GetEnemyRow(enemy);
                Canvas = (Canvas)MonsterField.Children[Row];
                path = App.resourcePaths.GetEnemyPath(c.CharacterID);
            }

            c.SwitchAnimation(Canvas, animID, path);
        }
        private void ShowEnemyDamage(string damage, EnemyCharacter enemy)
        {
            TextBlock txtBlk = null;

            var i = App.charactersLists.EnemyList.IndexOf(enemy);
            var canvas = (Canvas)MonsterField.Children[i];
            var grid = (Grid)canvas.Children[1];
            txtBlk = (TextBlock)grid.Children[0];
            txtBlk.Text = damage;

            damageInfo.SetTickTimer(txtBlk, BattleTimer);
        }
        private void ShowPlayerDamage(string damage, PlayerCharacter player)
        {
            TextBlock txtBlk = null;

            var i = App.charactersLists.PlayerList.IndexOf(player);
            var canvas = (Canvas)PlayerField.Children[i];
            var grid = (Grid)canvas.Children[1];
            txtBlk = (TextBlock)grid.Children[0];
            txtBlk.Text = damage;

            damageInfo.SetTickTimer(txtBlk, BattleTimer);
        }

        private void PlayerTurn()
        {
            BattleTimer.Stop();
            BattleMenuCursor.Visibility = Visibility.Visible;

            menuAction = new BattlePageMenuAction();
        }
        private void EnemyTurn()
        {
            Random rand = new Random();            
            var enemy = App.charactersLists.CharacterOrder.First();
            var enemyRow = App.charactersLists.GetEnemyRow((EnemyCharacter)enemy);
            var player = App.charactersLists.GetPlayerTarget(rand);
            damageInfo = new DamageInfoVisibility(player);

            var enemyCanvas = (Canvas)MonsterField.Children[enemyRow];
            var behavior = enemy.BehaviorList[0];

            var damage = behavior.Action(enemy, player);
            enemy.SwitchAnimation(enemyCanvas, 1, App.resourcePaths.GetEnemyPath(enemy.CharacterID), damageInfo.DamageTimer, 4);

            if (player.CharacterStatus.CheckDying(player))
            {
                SetIdleLastCharacter(player);
            }

            player.CharacterStatus.AddSP(player.CharacterStats.SP, damage);

            setOnField.SetPlayerStatus(App.charactersLists.GetPlayerRow(player), player, PlayerMenu);

            ShowPlayerDamage(damage.ToString(), player);
            ResetTurn();
        }
        private void ResetTurn()
        {
            BattleMenuCursor.Visibility = Visibility.Hidden;
            Grid.SetRow(BattleMenuCursor, 0);
            PlayerMenuCursor.Visibility = Visibility.Hidden;
            Grid.SetRow(PlayerMenuCursor, 0);
            EnemyMenuCursor.Visibility = Visibility.Hidden;
            Grid.SetRow(EnemyMenuCursor, 0);
            Grid.SetColumn(EnemyMenuCursor, 0);
            SubMenuGrid.Visibility = Visibility.Hidden;
            Grid.SetRow(SubMenuCursor, 0);
            BattleLimitBreak.Foreground = Brushes.Gray;

            mainMenuAction = null;
            lastAction = null;
            menuAction = null;
            ResetOrder();
        }
        private void ResetOrder()
        {
            App.charactersLists.ReOrder(CharacterOrder);
        }

        private void setBackground(string enemyElement)
        {
            var path = App.resourcePaths.GetBattleBackground(enemyElement);
            BitmapImage source = new BitmapImage();
            source.BeginInit();
            source.UriSource = new Uri(path);
            source.EndInit();
            MainGrid.Background = new ImageBrush() { ImageSource = source };
        }

        private bool CheckLimitBreak()
        {
            var player = App.charactersLists.CharacterOrder.First();
            if (player.CharacterStatus.CurrentSP == player.CharacterStats.SP)
            {
                BattleLimitBreak.Foreground = Brushes.WhiteSmoke;
                return true;
            }
            else
            {
                BattleLimitBreak.Foreground = Brushes.Gray;
            }
            return false;
        }
        private void ShowSkillName(string name = "", bool show = false)
        {
            if(show)
            {
                txtBlkSkillName.Text = name;
                gridSkillName.Visibility = Visibility.Visible;
            }
            else
            {
                gridSkillName.Visibility = Visibility.Hidden;
            }
        }
    }
}
