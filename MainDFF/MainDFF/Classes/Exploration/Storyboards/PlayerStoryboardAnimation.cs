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
    public class PlayerStoryboardAnimation : AStoryboardAnimation
    {
        public override void CreateStoryboard(Key direction, Canvas MapCanvas)
        {
            Point MapPos = new Point(Canvas.GetLeft(MapCanvas), Canvas.GetTop(MapCanvas));

            DoubleAnimation MapAnim;

            int tileWidth = 26;

            switch (direction)
            {
                case Key.Up:
                    MapAnim = new DoubleAnimation(MapPos.Y, MapPos.Y + tileWidth, TimeSpan.FromSeconds(0.2));
                    AddToStoryboard(MapAnim, MapCanvas, 0);
                    break;
                case Key.Down:
                    MapAnim = new DoubleAnimation(MapPos.Y, MapPos.Y - tileWidth, TimeSpan.FromSeconds(0.2));
                    AddToStoryboard(MapAnim, MapCanvas, 0);
                    break;
                case Key.Left:
                    MapAnim = new DoubleAnimation(MapPos.X, MapPos.X + tileWidth, TimeSpan.FromSeconds(0.2));
                    AddToStoryboard(MapAnim, MapCanvas, 1);
                    break;
                case Key.Right:
                    MapAnim = new DoubleAnimation(MapPos.X, MapPos.X - tileWidth, TimeSpan.FromSeconds(0.2));
                    AddToStoryboard(MapAnim, MapCanvas, 1);
                    break;
                default:
                    break;
            }
        }
        public override void AddToStoryboard(DoubleAnimation Anim, Canvas Canvas, int Direction)
        {
            switch (Direction)
            {
                case 0:
                    Storyboard.SetTarget(Anim, Canvas);
                    Storyboard.SetTargetProperty(Anim, new PropertyPath(Canvas.TopProperty));
                    MainStoryboard.Children.Add(Anim);
                    break;
                case 1:
                    Storyboard.SetTarget(Anim, Canvas);
                    Storyboard.SetTargetProperty(Anim, new PropertyPath(Canvas.LeftProperty));
                    MainStoryboard.Children.Add(Anim);
                    break;
                default:
                    break;
            }

            AnimationComplete = false;
        }
    }
}
