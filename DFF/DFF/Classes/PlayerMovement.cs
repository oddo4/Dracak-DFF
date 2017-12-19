using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using WpfAnimatedGif;

namespace DFF.Classes
{
    class PlayerMovement
    {
        public int PosX = 0;
        public int PosY = 0;
        public bool HeroAniCmp = true;
        public bool BattleStart = false;
        public int Step = 0;
        public Key LastKey = Key.Down;
        private double spriteX = 0;
        private double spriteY = 0;

        public PlayerMovement()
        {

        }

        public PlayerMovement(int posX, int posY)
        {
            this.PosX = posX;
            this.PosY = posY;
        }

        public void MoveDirection(Key InputKey, Image HeroImage)
        {
            spriteX = 0;

            double offset = HeroImage.ActualHeight / 4;
            switch (InputKey)
            {
                case Key.Up:
                    offset = offset * -1;
                    Canvas.SetLeft(HeroImage, 0);
                    Canvas.SetTop(HeroImage, offset);
                    break;
                case Key.Down:
                    offset = offset * 0;
                    Canvas.SetLeft(HeroImage, 0);
                    Canvas.SetTop(HeroImage, offset);
                    break;
                case Key.Left:
                    offset = offset * -2;
                    Canvas.SetLeft(HeroImage, 0);
                    Canvas.SetTop(HeroImage, offset);
                    break;
                case Key.Right:
                    offset = offset * -3;
                    Canvas.SetLeft(HeroImage, 0);
                    Canvas.SetTop(HeroImage, offset);
                    break;
                case Key.Y:
                    break;
                default:
                    break;
            }
            LastKey = InputKey;
        }

        public void Move(Key InputKey, Image HeroImage, Canvas HeroCanvas)
        {
            switch (InputKey)
            {
                case Key.Up:
                    SpriteMove(InputKey, HeroImage);
                    PosY -= 1;
                    ZIndexChange(HeroCanvas);
                    break;
                case Key.Down:
                    SpriteMove(InputKey, HeroImage);
                    PosY += 1;
                    ZIndexChange(HeroCanvas);
                    break;
                case Key.Left:
                    SpriteMove(InputKey, HeroImage);
                    PosX -= 1;
                    break;
                case Key.Right:
                    SpriteMove(InputKey, HeroImage);
                    PosX += 1;
                    break;
                default:
                    break;
            }
            StepSwitch();
            LastKey = InputKey;
        }

        private void StepSwitch()
        {
            switch (Step)
            {
                case 0:
                    Step = 1;
                    break;
                case 1:
                    Step = 0;
                    break;
            }
        }

        public bool CheckMap(Key InputKey, Canvas MapCanvas, Canvas HeroCanvas, TileMap LevelMap, Storyboard Storyboard, List<Classes.EnemyPosition> EnemyList)
        {
            switch (InputKey)
            {
                case Key.Up:
                    if (PosY - 1 >= 0)
                    {
                        if (LevelMap.CheckConflict(0, PosX, PosY))
                        {
                            BattleStart = true;
                        }
                            MoveAnimation(Storyboard, MapCanvas, HeroCanvas, 0, EnemyList);
                        return true;
                    }
                    break;
                case Key.Down:
                    if (PosY + 1 < LevelMap.Map.GetLength(0))
                    {
                        if (LevelMap.CheckConflict(1, PosX, PosY))
                        {
                            BattleStart = true;
                        }
                            MoveAnimation(Storyboard, MapCanvas, HeroCanvas, 1, EnemyList);
                        return true;
                    }
                    break;
                case Key.Left:
                    if (PosX - 1 >= 0)
                    {
                        if (LevelMap.CheckConflict(2, PosX, PosY))
                        {
                            BattleStart = true;
                        }
                            MoveAnimation(Storyboard, MapCanvas, HeroCanvas, 2, EnemyList);
                        return true;
                    }
                    break;
                case Key.Right:
                    if (PosX + 1 < LevelMap.Map.GetLength(0))
                    {
                        if (LevelMap.CheckConflict(3, PosX, PosY))
                        {
                            BattleStart = true;
                        }
                            MoveAnimation(Storyboard, MapCanvas, HeroCanvas, 3, EnemyList);
                        return true;
                    }
                    break;
                default:
                    break;
            }

            return false;
        }

        private void MoveAnimation(Storyboard Storyboard, Canvas MapCanvas, Canvas HeroCanvas, int Direction, List<Classes.EnemyPosition> EnemyList)
        {
            var MapTop = Canvas.GetTop(MapCanvas);
            var MapLeft = Canvas.GetLeft(MapCanvas);
            var HeroTop = Canvas.GetTop(HeroCanvas);
            var HeroLeft = Canvas.GetLeft(HeroCanvas);
            DoubleAnimation MapAnim;
            DoubleAnimation HeroAnim;

            switch (Direction)
            {
                case 0:
                    MapAnim = new DoubleAnimation(MapTop, MapTop + 24, TimeSpan.FromSeconds(0.2));
                    AddToStoryboard(Storyboard, MapAnim, MapCanvas, 0);

                    HeroAnim = new DoubleAnimation(HeroTop, HeroTop - 24, TimeSpan.FromSeconds(0.2));
                    AddToStoryboard(Storyboard, HeroAnim, HeroCanvas, 0);
                    break;
                case 1:
                    MapAnim = new DoubleAnimation(MapTop, MapTop - 24, TimeSpan.FromSeconds(0.2));
                    AddToStoryboard(Storyboard, MapAnim, MapCanvas, 0);

                    HeroAnim = new DoubleAnimation(HeroTop, HeroTop + 24, TimeSpan.FromSeconds(0.2));
                    AddToStoryboard(Storyboard, HeroAnim, HeroCanvas, 0);
                    break;
                case 2:
                    MapAnim = new DoubleAnimation(MapLeft, MapLeft + 24, TimeSpan.FromSeconds(0.2));
                    AddToStoryboard(Storyboard, MapAnim, MapCanvas, 1);

                    HeroAnim = new DoubleAnimation(HeroLeft, HeroLeft - 24, TimeSpan.FromSeconds(0.2));
                    AddToStoryboard(Storyboard, HeroAnim, HeroCanvas, 1);
                    break;
                case 3:
                    MapAnim = new DoubleAnimation(MapLeft, MapLeft - 24, TimeSpan.FromSeconds(0.2));
                    AddToStoryboard(Storyboard, MapAnim, MapCanvas, 1);

                    HeroAnim = new DoubleAnimation(HeroLeft, HeroLeft + 24, TimeSpan.FromSeconds(0.2));
                    AddToStoryboard(Storyboard, HeroAnim, HeroCanvas, 1);
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

        public void SpriteMove(Key Direction, Image HeroImage)
        {
            DispatcherTimer SpriteTimer = new DispatcherTimer();
            SpriteTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            SpriteTimer.Tick += (sender, args) => { HeroSprite(HeroImage, SpriteTimer); };

            switch (Direction)
            {
                case Key.Up:
                    spriteY = HeroImage.ActualHeight * -0.25;
                    Canvas.SetTop(HeroImage, spriteY);
                    break;
                case Key.Down:
                    spriteY = HeroImage.ActualHeight * 0;
                    Canvas.SetTop(HeroImage, spriteY);
                    break;
                case Key.Left:
                    spriteY = HeroImage.ActualWidth * -0.5;
                    Canvas.SetTop(HeroImage, spriteY);
                    break;
                case Key.Right:
                    spriteY = HeroImage.ActualWidth * -0.75;
                    Canvas.SetTop(HeroImage, spriteY);
                    break;
                default:
                    break;
            }

            HeroAniCmp = false;
            SpriteTimer.Start();
        }

        private void HeroSprite(Image HeroImage, DispatcherTimer SpriteTimer)
        {
            double offsetX = HeroImage.ActualWidth / 4;

            spriteX -= offsetX;

            Canvas.SetLeft(HeroImage, spriteX);

            if (spriteX == -HeroImage.ActualWidth)
            {
                spriteX = 0;

                Canvas.SetLeft(HeroImage, spriteX);

                SpriteTimer.Stop();
            }
            else if (spriteX == -HeroImage.ActualWidth / 2)
            {
                Canvas.SetLeft(HeroImage, spriteX);

                SpriteTimer.Stop();
            }
        }

        public void ZIndexChange(Canvas Change)
        {
            Canvas.SetZIndex(Change, PosY);
        }
    }
}
