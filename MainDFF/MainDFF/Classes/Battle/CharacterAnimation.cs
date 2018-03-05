using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace MainDFF.Classes.Battle
{
    public class CharacterAnimation : ASpriteAnimation
    {
        public string SpriteFileName { get; set; }
        public Point SpriteRowCol { get; set; }
        public int SpriteFramesCount { get; set; }
        [JsonIgnore]
        public int CurrentFrame = 0;
        DispatcherTimer SpriteTimer;
        public void CreateSprite(Image SpriteImage)
        {
            SpriteTimer = new DispatcherTimer();
            SpriteTimer.Interval = new TimeSpan(0, 0, 0, 0, 200);
            SpriteTimer.Tick += (sender, args) => { PlaySprite(SpriteImage, SpriteTimer); };

            StartSprite();
        }
        public void StartSprite()
        {
            SpriteTimer.Start();
        }

        public void PlaySprite(Image SpriteImage, DispatcherTimer SpriteTimer)
        {
            if (CurrentFrame != SpriteFramesCount)
            {
                var offsetX = SpriteImage.ActualWidth / SpriteRowCol.X;

                SpritePos.X -= offsetX;

                Canvas.SetLeft(SpriteImage, SpritePos.X);

                if (SpritePos.X == -SpriteImage.ActualWidth && SpritePos.Y != -SpriteImage.ActualHeight)
                {
                    var offsetY = SpriteImage.ActualHeight / SpriteRowCol.Y;

                    SpritePos.X = 0;
                    SpritePos.Y -= offsetY;

                    Canvas.SetLeft(SpriteImage, SpritePos.X);
                    Canvas.SetTop(SpriteImage, SpritePos.Y);
                }

                CurrentFrame++;
            }
            else
            {
                SpritePos = new Point(0, 0);
                CurrentFrame = 0;

                Canvas.SetLeft(SpriteImage, SpritePos.X);
                Canvas.SetTop(SpriteImage, SpritePos.Y);
            }  
        }

        public void StopSprite()
        {
            SpriteTimer.Stop();
        }
    }
}
