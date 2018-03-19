using MainDFF.Classes.ControlActions.MoveActions;
using MainDFF.Classes.Items;
using MainDFF.Classes.Items.Key;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace MainDFF.Classes.Exploration
{
    public class MapData
    {
        public int RoomID { get; set; }
        public List<List<int>> ListMap = new List<List<int>>();
        public List<PathToLevel> PathsLevel = new List<PathToLevel>();
        public List<EnemyMoveAction> EnemyList = new List<EnemyMoveAction>();
        public Portal Portal = null;

        public MapData(int roomID, int enemyCount, int pathDirection, int lastDirection, bool portal = false, bool key = false)
        {
            RoomID = roomID;
            CreateListMap();
            SetWall();
            CreateEnemyList(enemyCount);
            CreatePathsToLevels(pathDirection, lastDirection);
            if (portal)
            {
                Random rand = new Random();
                CreatePortal(rand, roomID);
            }
            if (key)
            {
                Random rand = new Random();
                SetKeyToEnemy(0); //rand.Next(0, EnemyList.Count)
            }
        }

        private void SetKeyToEnemy(int value)
        {
            for (int i = 0; i < EnemyList.Count; i++)
            {
                if (value == i)
                {
                    Item key = new Item("Crystal fragment");
                    key.Type = new PortalKey();
                    EnemyList[i].PortalKey = key;
                }
            }
        }

        private void CreatePortal(Random rand, int roomID)
        {
            var pos = new Point(rand.Next(1, 17), rand.Next(1, 17));
            foreach (EnemyMoveAction enemy in EnemyList)
            {
                if (pos == enemy.Pos)
                {
                    pos = new Point(rand.Next(1, 17), rand.Next(1, 17));
                }
            }
            Portal = new Portal();
            Portal.PortalPos = pos;
            Portal.RoomID = roomID;

            ListMap[(int)Portal.PortalPos.X][(int)Portal.PortalPos.Y] = 4;
        }
        public void ShowPortalElement(Canvas PortalCanvas, bool visible = false)
        {
            if (visible)
            {
                Panel.SetZIndex(PortalCanvas, (int)Portal.PortalPos.Y);
                PortalCanvas.Visibility = Visibility.Visible;
            }
            else
            {
                PortalCanvas.Visibility = Visibility.Hidden;
            }
        }
        private void CreateListMap()
        {
            for (int i = 0; i < 18; i++)
            {
                ListMap.Add(new List<int>());
                for (int j = 0; j < 18; j++)
                {
                    ListMap[i].Add(0);
                }
            }
        }

        public void SetWall()
        {
            for (int i = 0; i < ListMap.Count; i++)
            {
                var list = ListMap[i];
                for (int j = 0; j < list.Count; j++)
                {
                    if (j == 0 || j == 17 || i == 0 || i == 17)
                    {
                        ListMap[i][j] = 5;
                    }
                }
            }
            
        }

        public void SetPlayerOnMapData(PlayerMoveAction player)
        {
            if(ListMap[(int)player.Pos.X][(int)player.Pos.Y] != 3)
            {
                SetZeroOnMapData(player.LastPos);
                ListMap[(int)player.Pos.X][(int)player.Pos.Y] = 2;
            }
        }
        public void SetEnemyOnMapData(EnemyMoveAction enemy)
        {
            SetZeroOnMapData(enemy.LastPos);
            ListMap[(int)enemy.Pos.X][(int)enemy.Pos.Y] = 1;
        }
        public void SetZeroOnMapData(Point pos)
        {
            if (pos != default(Point))
                ListMap[(int)pos.X][(int)pos.Y] = 0;
        }
        public void SetPathsToLevels()
        {
            for (int i = 0; i < PathsLevel.Count; i++)
            {
                var X = (int)PathsLevel[i].PathPos.X;
                var Y = (int)PathsLevel[i].PathPos.Y;

                if (X == 8)
                {
                    X++;
                }

                if (Y == 8)
                {
                    Y++;
                }

                ListMap[(int)PathsLevel[i].PathPos.X][(int)PathsLevel[i].PathPos.Y] = 3;
                ListMap[X][Y] = 3;
            }
        }
        public void CreatePathsToLevels(int pathDirection, int lastDirection)
        {
            List<int> paths = new List<int>();
            List<int> XValues = new List<int>() { 8, 8, 0, 17 };
            List<int> YValues = new List<int>() { 0, 17, 8, 8 };

            var offset = 1;

            if (pathDirection != -1)
            {
                paths.Add(pathDirection);
            }
            else
            {
                offset *= -1;
            }
            if (lastDirection != -1)
            {
                paths.Add(lastDirection);
            }
            else
            {
                offset *= 1;
            }

            for (int i = 0; i < paths.Count; i++)
            {
                var X = XValues[paths[i]];
                var Y = YValues[paths[i]];
                PathToLevel path = new PathToLevel();
                path.PathPos = new Point(X, Y);
                path.RoomID = RoomID + offset;

                PathsLevel.Add(path);
                offset *= -1;
            }

            SetPathsToLevels();
        }
        public void CreateEnemyList(int Count)
        {
            Random rand = new Random();
            var key = rand.Next(0, Count);
            var offsetX = 0;
            var offsetY = 0;
            for (int i = 0; i < Count; i++)
            {
                var X = rand.Next(3 + offsetX, 7 + offsetX);
                var Y = rand.Next(3 + offsetY, 7 + offsetY);

                EnemyMoveAction newEnemyMove = new EnemyMoveAction(new Point(X, Y), rand);
                if (key == i)
                {
                    newEnemyMove.PortalKey = new Item("Crystal fragment");
                    newEnemyMove.PortalKey.Type = new PortalKey();
                }
                SetEnemyOnMapData(newEnemyMove);
                EnemyList.Add(newEnemyMove);

                offsetX += 6;
                if (offsetX > 6)
                {
                    offsetX = 0;
                    offsetY += 6;
                }
            }
        }

        public void CreateEnemyElement(Canvas CharacterField)
        {
            for (int i = 0; i < EnemyList.Count; i++)
            {
                Image image = new Image() { Height = 232, Width = 232 };
                image.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/CharacterSprites/Exploration/EnemyWalk.png"));
                Canvas.SetLeft(image, 0);
                Canvas.SetTop(image, 0);
                Canvas canvas = new Canvas() { Height = 58, Width = 58, ClipToBounds = true, Margin = new Thickness(0, -23, 0, 0) };
                Canvas.SetLeft(canvas, 291);
                Canvas.SetTop(canvas, 211);

                canvas.Children.Add(image);
                Canvas.SetZIndex(canvas, (int)EnemyList[i].Pos.Y);
                CharacterField.Children.Add(canvas);
            }
        }
        public string TestMap()
        {
            string stringMap = "";
            for (int i = 0; i < ListMap.Count; i++)
            {
                for (int j = 0; j < ListMap[i].Count; j++)
                {
                    stringMap += ListMap[j][i];
                }
                stringMap += Environment.NewLine;
            }

            return stringMap;
        }
    }
}
