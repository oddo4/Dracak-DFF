using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace MainDFF.Classes.Battle
{
    public class DamageInfoVisibility
    {
        public ACharacter Target;
        public DispatcherTimer DamageTimer;
        public DispatcherTimer BattleTimer;
        public TextBlock TxtBlk;
        public int Ctr = 0;
        public int Offset = 1;
        public DamageInfoVisibility(ACharacter target)
        {
            Target = target;
            Ctr = 0;
            DamageTimer = new DispatcherTimer(DispatcherPriority.Send);
            DamageTimer.Interval = new TimeSpan(0, 0, 0, 0, 75);
        }
        public void SetTickTimer(TextBlock txtBlk, DispatcherTimer battleTimer)
        {
            BattleTimer = battleTimer;
            TxtBlk = txtBlk;
            DamageTimer.Tick += (sender, args) => { ShowDamageInfo(); };
        }
        public void ShowDamageInfo()
        {
            if (Ctr >= Offset + 5)
            {
                DamageStop();
            }
            else if (Ctr >= Offset)
            {
                TxtBlk.Visibility = Visibility.Visible;
            }

            Ctr++;
        }

        public void DamageStop()
        {
            TxtBlk.Visibility = Visibility.Hidden;
            DamageTimer.Stop();
            BattleTimer.Start();
        }
    }
}
