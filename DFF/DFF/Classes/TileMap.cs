using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace DFF.Classes
{
    class TileMap
    {
        // array values
        // 0 - empty
        // 1 - player
        // 2 - enemy
        // 3 - enemy field of view
        // 4 - enemy weakness
        // 5 - 
        public int[,] Map = new int[16 ,16];

        public TileMap ()
        {
            SetMap();
        }

        public void SetPlayerOnMap(Canvas MapCanvas, Canvas HeroCanvas, int HeroX, int HeroY)
        {
            Canvas.SetLeft(MapCanvas, (HeroX * -24) - 24);
            Canvas.SetTop(MapCanvas, (HeroY * -24) - 48);
            Canvas.SetLeft(HeroCanvas, Canvas.GetLeft(HeroCanvas) + (HeroX * 24) + 24);
            Canvas.SetTop(HeroCanvas, Canvas.GetTop(HeroCanvas) + (HeroY * 24) + 24);
            Map[HeroX, HeroY] = 9;
        }

        public void UpdatePlayerOnMap(Canvas MapCanvas, Canvas HeroCanvas, int HeroX, int HeroY)
        {
            Canvas.SetLeft(MapCanvas, (HeroX * -24) - 24);
            Canvas.SetTop(MapCanvas, (HeroY * -24) - 48);
            Canvas.SetLeft(HeroCanvas, Canvas.GetLeft(HeroCanvas) + (HeroX * 24));
            Canvas.SetTop(HeroCanvas, Canvas.GetTop(HeroCanvas) + (HeroY * 24));
        }

        public void SetEnemyOnMap(Canvas MapCanvas, Canvas EnemyCanvas, int EnemyX, int EnemyY)
        {
            Canvas.SetLeft(EnemyCanvas, Canvas.GetLeft(EnemyCanvas) + (EnemyX * 24) + 24);
            Canvas.SetTop(EnemyCanvas, Canvas.GetTop(EnemyCanvas) + (EnemyY * 24) + 24);
            Map[EnemyX, EnemyY] = 2;
        }

        public bool PlayerPosition(Key Direction, int HeroX, int HeroY)
        {
            switch (Direction)
            {
                case Key.Up:
                    if (Map[HeroX, HeroY] == 0)
                    {
                        Map[HeroX, HeroY + 1] = 0;
                        Map[HeroX, HeroY] = 9;
                        return true;
                    }
                    break;
                case Key.Down:
                    if (Map[HeroX, HeroY] == 0)
                    {
                        Map[HeroX, HeroY - 1] = 0;
                        Map[HeroX, HeroY] = 9;
                        return true;
                    }
                    break;
                case Key.Left:
                    if (Map[HeroX, HeroY] == 0)
                    {
                        Map[HeroX + 1, HeroY] = 0;
                        Map[HeroX, HeroY] = 9;
                        return true;
                    }
                    break;
                case Key.Right:
                    if (Map[HeroX, HeroY] == 0)
                    {
                        Map[HeroX - 1, HeroY] = 0;
                        Map[HeroX, HeroY] = 9;
                        return true;
                    }
                    break;
                default:
                    break;
            }

            return false;
        }

        public bool EnemyPosition(int EnemyX, int EnemyY,int Type, int Steps)
        {
            if (Map[EnemyX, EnemyY] == 0)
            {
                for (int y = 0; y < 3; y++)
                {
                    for (int x = 0; x < 3; x++)
                    {
                        if (Map[(EnemyX - 1) + x, (EnemyY - 1) + y] != 0)
                        {
                            return false;
                        }
                    }
                }

                Map[EnemyX, EnemyY] = 2;
                return true;
            }

            return false;
        }

        public void PlayerClearAround(int HeroX, int HeroY)
        {
            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    if (HeroX - 1 >= 0 && HeroY - 1 >= 0)
                    {
                        Map[(HeroX - 1) + x, (HeroY - 1) + y] = 0;
                    }
                }
            }
        }

        public void EnemyClearAround(int EnemyX, int EnemyY)
        {
            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    if (EnemyX - 1 >= 0 && EnemyY - 1 >= 0)
                    {
                        Map[(EnemyX - 1) + x, (EnemyY - 1) + y] = 0;
                    }
                }
            }

            Map[EnemyX, EnemyY] = 2;
        }

        public void EnemyRange(int Direction, EnemyPosition e)
        {
            switch (Direction)
            {
                case 0:
                    for (int i = e.PosX - 1; i < e.PosX + 2; i++)
                    {
                        if (i >= 0 && i <= Map.GetLength(0))
                        {
                            if (Map[i, e.PosY - 1] == 9)
                            {
                                e.EnemyAlert = true;
                            }
                            else
                            {
                                Map[i, e.PosY - 1] = 3;
                                Map[i, e.PosY + 1] = 0;
                            }
                        }
                    }
                    Map[e.PosX, e.PosY + 1] = 4;
                    Map[e.PosX + 1, e.PosY] = 4;
                    Map[e.PosX - 1, e.PosY] = 4;
                    Map[e.PosX, e.PosY + 2] = 0;
                    Map[e.PosX + 1, e.PosY + 1] = 0;
                    Map[e.PosX - 1, e.PosY + 1] = 0;
                    break;
                case 1:
                    for (int i = e.PosX - 1; i < e.PosX + 2; i++)
                    {
                        if (i >= 0 && i <= Map.GetLength(0))
                        {
                            if (Map[i, e.PosY + 1] == 9)
                            {
                                e.EnemyAlert = true;
                            }
                            else
                            {
                                Map[i, e.PosY - 1] = 0;
                                Map[i, e.PosY + 1] = 3;
                            }
                        }
                    }
                    Map[e.PosX, e.PosY - 1] = 4;
                    Map[e.PosX + 1, e.PosY] = 4;
                    Map[e.PosX - 1, e.PosY] = 4;
                    Map[e.PosX, e.PosY - 2] = 0;
                    Map[e.PosX + 1, e.PosY - 1] = 0;
                    Map[e.PosX - 1, e.PosY - 1] = 0;
                    break;
                case 2:
                    for (int i = e.PosY - 1; i < e.PosY + 2; i++)
                    {
                        if (i >= 0 && i <= Map.GetLength(0))
                        {
                            if (Map[i, e.PosX - 1] == 9)
                            {
                                e.EnemyAlert = true;
                            }
                            else
                            {
                                Map[e.PosX - 1, i] = 3;
                                Map[e.PosX + 1, i] = 0;
                            }
                        }
                    }
                    Map[e.PosX, e.PosY + 1] = 4;
                    Map[e.PosX, e.PosY - 1] = 4;
                    Map[e.PosX + 1, e.PosY] = 4;
                    Map[e.PosX + 1, e.PosY + 1] = 0;
                    Map[e.PosX + 1, e.PosY - 1] = 0;
                    Map[e.PosX + 2, e.PosY] = 0;
                    break;
                case 3:
                    for (int i = e.PosY - 1; i < e.PosY + 2; i++)
                    {
                        if (i >= 0 && i <= Map.GetLength(0))
                        {

                            if (Map[i, e.PosX - 1] == 9)
                            {
                                e.EnemyAlert = true;
                            }
                            else
                            {
                                Map[e.PosX - 1, i] = 0;
                                Map[e.PosX + 1, i] = 3;
                            }
                        }
                    }
                    Map[e.PosX, e.PosY + 1] = 4;
                    Map[e.PosX, e.PosY - 1] = 4;
                    Map[e.PosX - 1, e.PosY] = 4;
                    Map[e.PosX - 1, e.PosY + 1] = 0;
                    Map[e.PosX - 1, e.PosY - 1] = 0;
                    Map[e.PosX - 2, e.PosY] = 0;
                    break;
                default:
                    break;
            }

            Map[e.PosX, e.PosY] = 2;
        }

        public bool CheckConflict(int Direction, int HeroX, int HeroY)
        {
            if (Map[HeroX, HeroY] == 3)
            {
                //NormalBattle
            }
            else if (Map[HeroX, HeroY] == 4)
            {
                //Pre-emptiveStrike
                switch (Direction)
                {
                    case 0:
                        if (Map[HeroX, HeroY - 1] == 2)
                        {
                            return true;
                        }
                        break;
                    case 1:
                        if (Map[HeroX, HeroY + 1] == 2)
                        {
                            return true;
                        }
                        break;
                    case 2:
                        if (Map[HeroX - 1, HeroY] == 2)
                        {
                            return true;
                        }
                        break;
                    case 3:
                        if (Map[HeroX + 1, HeroY] == 2)
                        {
                            return true;
                        }
                        break;
                    default:
                        break;
                }
            }

            return false;
        }

        public void SetMap()
        {
            for (int i = 0; i < this.Map.GetLength(0); i++)
            {
                for (int j = 0; j < this.Map.GetLength(1); j++)
                {
                    this.Map[i, j] = 0;
                }
            }
        }
        public string TestMap()
        {
            string MapString = "";
            for (int i = 0; i < this.Map.GetLength(0); i++)
            {
                for (int j = 0; j < this.Map.GetLength(1); j++)
                {
                    MapString += Map[j, i];
                }
                MapString += Environment.NewLine;
            }

            return MapString;
        }
    }
}
