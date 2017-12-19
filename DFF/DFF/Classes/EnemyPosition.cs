using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using WpfAnimatedGif;

namespace DFF.Classes
{
    class EnemyPosition
    {
        public int PosX = 0;
        public int PosY = 0;
        public bool EnemyAniCmp = true;
        public bool EnemyAlert = false;
        public int Time = 0;
        public int Direction = 0;
        public int Type = 0;
        public int Steps = 0;
        public int StepsCtr = 0;
        public bool BattleStart = false;
        private double spriteX = 0;
        private double spriteY = 0;

        public EnemyPosition(int posX, int posY, int direction, int time, int type, int steps)
        {
            this.PosX = posX;
            this.PosY = posY;
            this.Direction = direction;
            this.Time = time;
            this.Type = type;
            this.Steps = steps;
            this.StepsCtr = steps;
        }

        public void MoveDirection(Image EnemyImage)
        {
            spriteX = 0;

            double offset = EnemyImage.ActualHeight / 4;
            switch (Direction)
            {
                case 0:
                    offset = offset * -1;
                    Canvas.SetLeft(EnemyImage, 0);
                    Canvas.SetTop(EnemyImage, offset);
                    break;
                case 1:
                    offset = offset * 0;
                    Canvas.SetLeft(EnemyImage, 0);
                    Canvas.SetTop(EnemyImage, offset);
                    break;
                case 2:
                    offset = offset * -2;
                    Canvas.SetLeft(EnemyImage, 0);
                    Canvas.SetTop(EnemyImage, offset);
                    break;
                case 3:
                    offset = offset * -3;
                    Canvas.SetLeft(EnemyImage, 0);
                    Canvas.SetTop(EnemyImage, offset);
                    break;
                default:
                    break;
            }

            SwitchDirection();
        }

        public bool Move(Canvas EnemyCanvas, Image EnemyImage, TileMap LevelMap)
        {
            switch (Direction)
            {
                case 0:
                    if (PosY - 1 >= 1)
                    {
                        SpriteMove(EnemyImage);
                        LevelMap.EnemyClearAround(PosX, PosY);
                        PosY -= 1;
                        ZIndexChange(EnemyCanvas);
                        return true;
                    }
                    else
                    {
                        Steps = StepsCtr;
                        SwitchDirection();
                    }
                    break;
                case 1:
                    if (PosY + 1 <= LevelMap.Map.GetLength(0) - 1)
                    {
                        SpriteMove(EnemyImage);
                        LevelMap.EnemyClearAround(PosX, PosY);
                        PosY += 1;
                        ZIndexChange(EnemyCanvas);
                        return true;
                    }
                    else
                    {
                        Steps = StepsCtr;
                        SwitchDirection();
                    }
                    break;
                case 2:
                    if (PosX - 1 >= 1)
                    {
                        SpriteMove(EnemyImage);
                        LevelMap.EnemyClearAround(PosX, PosY);
                        PosX -= 1;
                        return true;
                    }
                    else
                    {
                        Steps = StepsCtr;
                        SwitchDirection();
                    }
                    break;
                case 3:
                    if (PosX + 1 <= LevelMap.Map.GetLength(0) - 1)
                    {
                        SpriteMove(EnemyImage);
                        LevelMap.EnemyClearAround(PosX, PosY);
                        PosX += 1;
                        return true;
                    }
                    else
                    {
                        Steps = StepsCtr;
                        SwitchDirection();
                    }
                    break;
                default:
                    break;
            }

            return false;
        }

        public void SpriteMove(Image EnemyImage)
        {
            DispatcherTimer SpriteTimer = new DispatcherTimer();
            SpriteTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            SpriteTimer.Tick += (sender, args) => { EnemySprite(EnemyImage, SpriteTimer); };

            switch (Direction)
            {
                case 0:
                    spriteY = EnemyImage.ActualHeight * -0.25;
                    Canvas.SetTop(EnemyImage, spriteY);
                    break;
                case 1:
                    spriteY = EnemyImage.ActualHeight * 0;
                    Canvas.SetTop(EnemyImage, spriteY);
                    break;
                case 2:
                    spriteY = EnemyImage.ActualWidth * -0.5;
                    Canvas.SetTop(EnemyImage, spriteY);
                    break;
                case 3:
                    spriteY = EnemyImage.ActualWidth * -0.75;
                    Canvas.SetTop(EnemyImage, spriteY);
                    break;
                default:
                    break;
            }

            EnemyAniCmp = false;
            SpriteTimer.Start();
        }

        private void EnemySprite(Image EnemyImage, DispatcherTimer SpriteTimer)
        {
            double offsetX = EnemyImage.ActualWidth / 4;

            spriteX -= offsetX;

            Canvas.SetLeft(EnemyImage, spriteX);

            if (spriteX == -EnemyImage.ActualWidth)
            {
                spriteX = 0;

                Canvas.SetLeft(EnemyImage, spriteX);

                SpriteTimer.Stop();
            }
            else if (spriteX == -EnemyImage.ActualWidth / 2)
            {
                Canvas.SetLeft(EnemyImage, spriteX);
                SpriteTimer.Stop();
            }
        }

        private void SwitchDirection()
        {
            switch (Type)
            {
                case 0:
                    switch (Direction)
                    {
                        case 0:
                            Direction = 1;
                            break;
                        case 1:
                            Direction = 0;
                            break;
                        case 2:
                            Direction = 3;
                            break;
                        case 3:
                            Direction = 2;
                            break;
                    }
                    break;
                case 1:
                    switch (Direction)
                    {
                        case 0:
                            Direction = 3;
                            break;
                        case 1:
                            Direction = 2;
                            break;
                        case 2:
                            Direction = 0;
                            break;
                        case 3:
                            Direction = 1;
                            break;
                    }
                    break;
                case 2:
                    switch (Direction)
                    {
                        case 0:
                            Direction = 2;
                            break;
                        case 1:
                            Direction = 3;
                            break;
                        case 2:
                            Direction = 1;
                            break;
                        case 3:
                            Direction = 0;
                            break;
                    }
                    break;
            }
        }

        public void MoveAnimation(Storyboard Storyboard, Canvas EnemyCanvas)
        {
            var EnemyTop = Canvas.GetTop(EnemyCanvas);
            var EnemyLeft = Canvas.GetLeft(EnemyCanvas);
            DoubleAnimation EnemyAnim;

            switch (Direction)
            {
                case 0:
                    EnemyAnim = new DoubleAnimation(EnemyTop, EnemyTop - 24, TimeSpan.FromSeconds(0.2));
                    AddToStoryboard(Storyboard, EnemyAnim, EnemyCanvas, 0);
                    break;
                case 1:
                    EnemyAnim = new DoubleAnimation(EnemyTop, EnemyTop + 24, TimeSpan.FromSeconds(0.2));
                    AddToStoryboard(Storyboard, EnemyAnim, EnemyCanvas, 0);
                    break;
                case 2:
                    EnemyAnim = new DoubleAnimation(EnemyLeft, EnemyLeft - 24, TimeSpan.FromSeconds(0.2));
                    AddToStoryboard(Storyboard, EnemyAnim, EnemyCanvas, 1);
                    break;
                case 3:
                    EnemyAnim = new DoubleAnimation(EnemyLeft, EnemyLeft + 24, TimeSpan.FromSeconds(0.2));
                    AddToStoryboard(Storyboard, EnemyAnim, EnemyCanvas, 1);
                    break;
                default:
                    break;
            }
        }

        private void AddToStoryboard(Storyboard Storyboard, DoubleAnimation Anim, Canvas Canvas, int Direction)
        {
            switch (Direction)
            {
                case 0:
                    Storyboard.SetTarget(Anim, Canvas);
                    Storyboard.SetTargetProperty(Anim, new PropertyPath(Canvas.TopProperty));
                    Storyboard.Children.Add(Anim);
                    break;
                case 1:
                    Storyboard.SetTarget(Anim, Canvas);
                    Storyboard.SetTargetProperty(Anim, new PropertyPath(Canvas.LeftProperty));
                    Storyboard.Children.Add(Anim);
                    break;
                default:
                    break;
            }

        }

        public void ZIndexChange(Canvas Change)
        {
            Canvas.SetZIndex(Change, PosY);
        }
    }
}
