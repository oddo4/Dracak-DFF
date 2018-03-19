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
    public class PlayerExploreStoryboardAnimation : AExploreStoryboardAnimation
    {
        public override void CreateStoryboard(Key direction, Canvas Canvas, bool invert = false)
        {
            Point CanvasPos = new Point(Canvas.GetLeft(Canvas), Canvas.GetTop(Canvas));

            if (invert)
            {
                direction = InvertDirection(direction);
            }

            DoubleAnimation CanvasAnim;

            int tileWidth = 26;

            switch (direction)
            {
                case Key.Up:
                    CanvasAnim = new DoubleAnimation(CanvasPos.Y, CanvasPos.Y + tileWidth, TimeSpan.FromSeconds(0.2));
                    AddToStoryboard(CanvasAnim, Canvas, 0);
                    break;
                case Key.Down:
                    CanvasAnim = new DoubleAnimation(CanvasPos.Y, CanvasPos.Y - tileWidth, TimeSpan.FromSeconds(0.2));
                    AddToStoryboard(CanvasAnim, Canvas, 0);
                    break;
                case Key.Left:
                    CanvasAnim = new DoubleAnimation(CanvasPos.X, CanvasPos.X + tileWidth, TimeSpan.FromSeconds(0.2));
                    AddToStoryboard(CanvasAnim, Canvas, 1);
                    break;
                case Key.Right:
                    CanvasAnim = new DoubleAnimation(CanvasPos.X, CanvasPos.X - tileWidth, TimeSpan.FromSeconds(0.2));
                    AddToStoryboard(CanvasAnim, Canvas, 1);
                    break;
                default:
                    break;
            }
        }

        private Key InvertDirection(Key direction)
        {
            Dictionary<Key, Key> switchDirection = new Dictionary<Key, Key>() { { Key.Up, Key.Down }, { Key.Down, Key.Up }, { Key.Left, Key.Right }, { Key.Right, Key.Left } };

            return switchDirection[direction];
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
