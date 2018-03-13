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
        public DispatcherTimer DamageTimer;
        public int Ctr = 0;
        public int Offset = 0;
        public DamageInfoVisibility(int time, TextBlock txtBlk)
        {
            Ctr = 0;
            Offset = time;
            DamageTimer = new DispatcherTimer(DispatcherPriority.Send);
            DamageTimer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            DamageTimer.Tick += (sender, args) => { ShowDamageInfo(txtBlk); };

            DamageTimer.Start();
        }
        public void ShowDamageInfo(TextBlock txtBlk)
        {
            if (Ctr >= Offset + 1)
            {
                txtBlk.Visibility = Visibility.Hidden;
                DamageTimer.Stop();
            }
            else if (Ctr >= Offset)
            {
                txtBlk.Visibility = Visibility.Visible;
            }

            Ctr++;
        }
    }
}
