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
using System.Windows.Threading;

namespace DFF.Pages
{
    /// <summary>
    /// Interakční logika pro NormalBattle.xaml
    /// </summary>
    public partial class NormalBattle : Page
    {
        Window window;
        LevelExplore PrevLevel;
        // 0 - normal
        // 1 - player adv
        // 2 - enemy adv
        int Advantage = 0;
        bool BattleStart = false;
        DispatcherTimer MainTimer = new DispatcherTimer();
        DispatcherTimer timer = new DispatcherTimer();

        public NormalBattle()
        {
            InitializeComponent();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += new EventHandler(BattleStatus);
            timer.Start();
        }

        public NormalBattle(int advantage, bool battleStart, DispatcherTimer mainTimer, LevelExplore prevLevel)
        {
            InitializeComponent();
            Advantage = advantage;
            MainTimer = mainTimer;
            PrevLevel = prevLevel;
            //BattleStart = battleStart;

            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += new EventHandler(BattleStatus);
            timer.Start();
        }

        private void BattleStatus(object sender, EventArgs e)
        {
            if (BattleStart)
            {
                this.NavigationService.GoBack();
            }
        }

        private void BattleEndKey(object sender, RoutedEventArgs e)
        {
            window = Window.GetWindow(this);
            window.KeyDown += BattleEnd;
        }

        private void BattleEnd(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                timer.Stop();
                BattleStart = false;
                window.KeyDown -= BattleEnd;
                this.NavigationService.Navigate(PrevLevel);
                MainTimer.Start();
            }
        }


    }
}
