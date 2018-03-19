using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace MainDFF.Classes.Exploration.Storyboards
{
    public abstract class AExploreStoryboardAnimation
    {
        public Storyboard MainStoryboard = new Storyboard();
        public bool AnimationComplete = true;
        public abstract void CreateStoryboard(Key direction, Canvas Canvas, bool Invert = false);
        public abstract void AddToStoryboard(DoubleAnimation Anim, Canvas Canvas, int Direction);
        public void AnimationCompleted()
        {
            MainStoryboard = new Storyboard();
            AnimationComplete = true;
        }
        public void SetCanvasZIndex(int Y, Canvas Canvas)
        {
            Panel.SetZIndex(Canvas, Y);
        }
    }
}
