using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace MainDFF.Classes
{
    public class SpriteAnimation
    {
        public Point SpritePos = new Point();
        public void CreateSprite(Key direction, Image SpriteImage)
        {
            DispatcherTimer SpriteTimer = new DispatcherTimer();
            SpriteTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            SpriteTimer.Tick += (sender, args) => { PlaySprite(SpriteImage, SpriteTimer); };

            switch(direction)
            {
                case Key.Up:
                    SpritePos.Y = SpriteImage.ActualHeight * -0.25;
                    break;
                case Key.Down:
                    SpritePos.Y = SpriteImage.ActualHeight * 0;
                    break;
                case Key.Left:
                    SpritePos.Y = SpriteImage.ActualHeight * -0.5;
                    break;
                case Key.Right:
                    SpritePos.Y = SpriteImage.ActualHeight * -0.75;
                    break;
                default:
                    return;
            }
            Canvas.SetTop(SpriteImage, SpritePos.Y);
            SpriteTimer.Start();
        }

        public void PlaySprite(Image SpriteImage, DispatcherTimer SpriteTimer)
        {
            var offsetX = SpriteImage.ActualWidth / 4;

            SpritePos.X -= offsetX;

            Canvas.SetLeft(SpriteImage, SpritePos.X);

            if (SpritePos.X == -SpriteImage.ActualWidth)
            {
                SpritePos.X = 0;

                Canvas.SetLeft(SpriteImage, SpritePos.X);

                SpriteTimer.Stop();
            }
            else if (SpritePos.X == -SpriteImage.ActualWidth / 2)
            {
                Canvas.SetLeft(SpriteImage, SpritePos.X);

                SpriteTimer.Stop();
            }
        }
    }
}
