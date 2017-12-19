using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using FightBehaviour.Classes;
using FightBehaviour.Behaviors;

namespace FightBehaviour
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer BattleTimer = new DispatcherTimer();
        Random rand = new Random();
        ObservableCollection<Character> orderList = new ObservableCollection<Character>();
        ObservableCollection<Character> playerList = new ObservableCollection<Character>();
        ObservableCollection<Character> enemyList = new ObservableCollection<Character>();
        Player p1 = new Player("Player1");
        Player p2 = new Player("Player2");
        Player p3 = new Player("Player3");
        Monster s1 = new Monster("Scorpion1");
        Monster s2 = new Monster("Scorpion2");
        Monster s3 = new Monster("Scorpion3");
        Monster s4 = new Monster("Scorpion4");
        Monster s5 = new Monster("Scorpion5");
        Monster s6 = new Monster("Scorpion6");
        int turn = 0;

        public MainWindow()
        {
            InitializeComponent();
            p1.Stats.SPD = rand.Next(0, 20);
            p2.Stats.SPD = rand.Next(0, 20);
            p3.Stats.SPD = rand.Next(0, 20);
            playerList.Add(p1);
            //p2.Lives = 0;
            playerList.Add(p2);
            //p3.Lives = 0;
            playerList.Add(p3);
            s1.Stats.SPD = rand.Next(0, 20);
            s2.Stats.SPD = rand.Next(0, 20);
            s3.Stats.SPD = rand.Next(0, 20);
            s4.Stats.SPD = rand.Next(0, 20);
            s5.Stats.SPD = rand.Next(0, 20);
            s6.Stats.SPD = rand.Next(0, 20);
            enemyList.Add(s1);
            enemyList.Add(s2);
            enemyList.Add(s3);
            enemyList.Add(s4);
            enemyList.Add(s5);
            enemyList.Add(s6);
            AddToOrder(enemyList);
            AddToOrder(playerList);
            orderList = new ObservableCollection<Character>(orderList.OrderByDescending(c => c.Stats.SPD));
            listViewOrder.ItemsSource = orderList;

            BattleTimer.Interval = new TimeSpan(0,0,2);
            BattleTimer.Tick += TimerAction;
            BattleTimer.Start();
            
        }

        private void TimerAction(object sender, EventArgs e)
        {
            if (turn != 0)
            {
                ReorderOrderList();
            }
            turn++;
            labelTurn.Content = turn;
            WhoseTurn();
            ValuesP(playerList);
            ValuesS(enemyList);

            foreach (Character c in orderList)
            {
                if (c.Lives <= 0)
                {
                    BattleTimer.Stop();
                    labelTurn.Content = c.Name + " died";
                }
            }
        }

        private void AddToOrder(ObservableCollection<Character> list)
        {
            foreach (Character c in list)
            {
                orderList.Add(c);
            }
        }

        private void ReorderOrderList()
        {
            Character last = orderList[0];
            orderList.RemoveAt(0);
            orderList.Add(last);
        }

        private void WhoseTurn()
        {
            if (enemyList.Contains(orderList[0]))
            {
                EnemyAction((Monster)orderList[0]);
            }
            else if (playerList.Contains(orderList[0]))
            {
                PlayerAction((Player)orderList[0]);
                //BattleTimer.Stop();
            }
        }

        private void PlayerAction(Player p)
        {
            int target = rand.Next(0, enemyList.Count);
            while (true)
            {
                if (enemyList[target].Lives > 0)
                {
                    p.LivesCheck();
                    p.AttackCommand(p, enemyList[target]);
                    labelAction.Content = p.Name + " uses " + p.AttackBehavior.AttackName + " on " + enemyList[target].Name;
                    break;
                }
                else
                {
                    target = rand.Next(0, enemyList.Count);
                }
            }
        }

        private void EnemyAction(Monster s)
        {
            int target = rand.Next(0, playerList.Count);
            while (true)
            {
                if (playerList[target].Lives > 0)
                {
                    s.LivesCheck();
                    s.AttackCommand(s, playerList[target]);
                    labelAction.Content = s.Name + " uses " + s.AttackBehavior.AttackName + " on " + playerList[target].Name;
                    break;
                }
                else
                {
                    target = rand.Next(0, playerList.Count);
                }
            }
        }

        private void ValuesP(ObservableCollection<Character> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                StackPanel panel = (StackPanel)gridPlayer.Children[i];
                Label name = (Label)panel.Children[0];
                Label nLives = (Label)panel.Children[1];
                ProgressBar pbLives = (ProgressBar)panel.Children[2];
                Label action = (Label)panel.Children[3];

                name.Content = list[i].Name;
                nLives.Content = list[i].Stats.HP + "/" + list[i].Lives;
                if (pbLives.Maximum == 0)
                {
                    pbLives.Maximum = list[i].Stats.HP;
                }
                pbLives.Value = list[i].Lives;
                action.Content = list[i].AttackBehavior.AttackName;
            }
            
        }

        private void ValuesS(ObservableCollection<Character> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                StackPanel panel = (StackPanel)gridEnemy.Children[i];
                Label name = (Label)panel.Children[0];
                Label nLives = (Label)panel.Children[1];
                ProgressBar pbLives = (ProgressBar)panel.Children[2];
                Label action = (Label)panel.Children[3];

                name.Content = list[i].Name;
                nLives.Content = list[i].Stats.HP + "/" + list[i].Lives;
                if (pbLives.Maximum == 0)
                {
                    pbLives.Maximum = list[i].Stats.HP;
                }
                pbLives.Value = list[i].Lives;
                action.Content = list[i].AttackBehavior.AttackName;
            }
        }
    }
}
