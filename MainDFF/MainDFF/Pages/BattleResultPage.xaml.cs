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
using System.Windows.Threading;

namespace MainDFF.Pages
{
    /// <summary>
    /// Interakční logika pro BattleResultPage.xaml
    /// </summary>
    public partial class BattleResultPage : Page
    {
        BattleResultPageMenuAction menuAction = new BattleResultPageMenuAction();
        DispatcherTimer ExpTimer;
        int confirmCount = 0;
        int tick = 1;
        public BattleResultPage(bool portalKey, bool Boss)
        {
            InitializeComponent();
            LoadCharacters();
            if (Boss)
            {
                HelpInfo.Text = "Chapter Completed!";
            }
            if (portalKey)
            {
                txtBlkReward.Text = "Crystal fragment aquired!";
            }
            else
            {
                txtBlkReward.Text = "EXP " + GetExp();
                confirmCount = 0;
            }
        }
        private void MenuKey_Loaded(object sender, RoutedEventArgs e)
        {
            App.window.KeyDown += MenuKeyDown;
            App.MainFrame.NavigationService.RemoveBackEntry();
        }
        private void MenuKeyDown(object sender, KeyEventArgs e)
        {
            //menuAction.CurrentIndex = Grid.GetRow(MenuCursor);
            var max = 0;
            var selected = menuAction.GetDirection(e.Key, max);
            if (selected > -1)
            {
                
            }
            else if (selected == -1)
            {
                Debug.WriteLine("E0001");
            }
            else
            {
                if (confirmCount == 0)
                {
                    txtBlkReward.Text = "EXP " + GetExp();
                    CreateExpTimer();
                }
                else if (confirmCount == 1)
                {
                    SetExp();
                }
                else if (confirmCount >= 2)
                {
                    App.charactersLists.ResetLists();
                    App.MainFrame.NavigationService.GoBack();
                    ResetEvent();
                }

                confirmCount++;
            }
        }

        private void CreateExpTimer()
        {
            ExpTimer = new DispatcherTimer();
            ExpTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            ExpTimer.Tick += new EventHandler(AddExp);

            ExpTimer.Start();
        }

        private void AddExp(object sender, EventArgs e)
        {
            if (GetExp() == tick)
            {
                ExpTimer.Stop();
                confirmCount = 2;
            }
            for (int i = 0; i < 3; i++)
            {
                var player = App.charactersLists.PlayerList[i];
                var gridMember = (Grid)PartyMembers.Children[i];
                var lvl = (TextBlock)((StackPanel)gridMember.Children[2]).Children[1];
                var exp = (TextBlock)((StackPanel)((StackPanel)gridMember.Children[3]).Children[0]).Children[1];
                var bar = (ProgressBar)((StackPanel)gridMember.Children[3]).Children[1];

                player.CharacterStats.CurrentEXP++;

                exp.Text = (GetExp() - tick).ToString();

                bar.Value = player.CharacterStats.CurrentEXP;
                if (bar.Value == bar.Maximum)
                {
                    player.CharacterStats.CurrentEXP = 0;
                    player.CharacterStats.LVL++;
                    player.CharacterStats.NextEXP *= 2;
                    bar.Maximum = player.CharacterStats.NextEXP;
                    bar.Value = player.CharacterStats.CurrentEXP;
                    lvl.Text = player.CharacterStats.LVL.ToString();
                }
            }
            tick++;
        }

        private void SetExp()
        {
            ExpTimer.Stop();
            for (int i = 0; i < 3; i++)
            {
                var player = App.charactersLists.PlayerList[i];
                var gridMember = (Grid)PartyMembers.Children[i];
                var lvl = (TextBlock)((StackPanel)gridMember.Children[2]).Children[1];
                var exp = (TextBlock)((StackPanel)((StackPanel)gridMember.Children[3]).Children[0]).Children[1];
                var bar = (ProgressBar)((StackPanel)gridMember.Children[3]).Children[1];

                exp.Text = "0";

                player.CharacterStats.CurrentEXP += GetExp();

                while (player.CharacterStats.CurrentEXP >= player.CharacterStats.NextEXP)
                {
                    var offset = player.CharacterStats.CurrentEXP - player.CharacterStats.NextEXP;
                    player.CharacterStats.CurrentEXP = 0;
                    player.CharacterStats.LVL++;
                    player.CharacterStats.NextEXP *= 2;
                    player.CharacterStats.CurrentEXP += offset;
                }

                bar.Maximum = player.CharacterStats.NextEXP;
                bar.Value = player.CharacterStats.CurrentEXP;
                lvl.Text = player.CharacterStats.LVL.ToString();
            }

            confirmCount = 2;
        }

        private int GetExp()
        {
            var exp = 0;
            foreach (EnemyCharacter enemy in App.charactersLists.EnemyList)
            {
                exp += enemy.CharacterStats.CurrentEXP;
            }

            return exp;
        }
        private void LoadCharacters()
        {
            for (int i = 0; i < 3; i++)
            {
                var player = App.charactersLists.PlayerList[i];
                var gridMember = (Grid)PartyMembers.Children[i];
                var image = (Image)((Canvas)gridMember.Children[0]).Children[0];
                var name = (TextBlock)gridMember.Children[1];
                var lvl = (TextBlock)((StackPanel)gridMember.Children[2]).Children[1];
                var exp = (TextBlock)((StackPanel)((StackPanel)gridMember.Children[3]).Children[0]).Children[1];
                var bar = (ProgressBar)((StackPanel)gridMember.Children[3]).Children[1];

                BitmapImage source = new BitmapImage();
                source.BeginInit();
                source.UriSource = new Uri(App.resourcePaths.GetPlayerImagePath(player.CharacterID));
                source.EndInit();

                image.Source = source;

                name.Text = player.Name;

                lvl.Text = player.CharacterStats.LVL.ToString();

                exp.Text = GetExp().ToString();

                bar.Value = player.CharacterStats.CurrentEXP;
                bar.Maximum = player.CharacterStats.NextEXP;
            }
        }
        private void ResetEvent()
        {
            App.window.KeyDown -= MenuKeyDown;
            menuAction = new BattleResultPageMenuAction();
        }
    }
}
