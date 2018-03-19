using MainDFF.Classes;
using MainDFF.Classes.Battle;
using MainDFF.Classes.ControlActions.MoveActions;
using MainDFF.Classes.Exploration;
using MainDFF.Classes.Items;
using MainDFF.Classes.Items.Key;
using MainDFF.Pages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MainDFF.Pages
{
    /// <summary>
    /// Interakční logika pro LevelPlayPage.xaml
    /// </summary>
    public partial class LevelPlayPage : Page
    {
        PlayerMoveAction moveAction = new PlayerMoveAction(new Point(1, 1));
        SetCharacterOnMap setCharacter = new SetCharacterOnMap();
        DispatcherTimer MainTimer = new DispatcherTimer();
        MapData MapData;
        ConflictChecker conflictChecker = new ConflictChecker();
        int LevelID;
        int time = 0;
        string enemyElement;
        bool End = false;
        public LevelPlayPage(int levelID, MapData mapData)
        {
            InitializeComponent();
            LevelID = levelID;
            enemyElement = App.fileHelper.LoadEnemyElement(levelID);
            CreateTimer();
            LoadMapData(mapData);
            //SetMapImage(enemyElement);
        }

        private void MenuKey_Loaded(object sender, RoutedEventArgs e)
        {
            if (End)
            {
                if (LevelID >= App.dataFileLists.CompletedChapters)
                {
                    App.dataFileLists.CompletedChapters++;
                }
                NavigationService.RemoveBackEntry();
                NavigationService.GoBack();
            }
            App.window.KeyDown += MenuKeyDown;
            MainTimer.Start();
        }
        private void MenuKeyDown(object sender, KeyEventArgs e)
        {
            if (gridInteract.Visibility == Visibility.Visible)
            {
                gridInteract.Visibility = Visibility.Hidden;
            }
            if (moveAction.StoryboardAnimation.AnimationComplete)
            {
                moveAction.StoryboardAnimation.MainStoryboard.Completed += new EventHandler(AnimationCompleted);
                var max = MapGrid.RowDefinitions.Count - 1;
                var selected = moveAction.GetDirection(e.Key, max);
                if (selected > 0)
                {
                    if (!conflictChecker.WallConflict(moveAction, MapData.ListMap))
                    {
                        if (conflictChecker.PortalConflict(moveAction, MapData.ListMap))
                        {
                            moveAction.SpriteAnimation.CreateSprite(e.Key, PlayerImage);
                            moveAction.Pos = moveAction.LastPos;
                        }
                        else
                        {
                            moveAction.StoryboardAnimation.CreateStoryboard(e.Key, MapCanvas);
                            moveAction.StoryboardAnimation.CreateStoryboard(e.Key, PlayerCanvas, true);
                            moveAction.SpriteAnimation.CreateSprite(e.Key, PlayerImage);
                            moveAction.StoryboardAnimation.MainStoryboard.Begin();
                            moveAction.StoryboardAnimation.SetCanvasZIndex((int)moveAction.Pos.Y, PlayerCanvas);
                            CheckConflict(moveAction);
                        }
                    }
                    else
                    {
                        moveAction.Pos = moveAction.LastPos;
                    }
                }
                else if (selected == -1)
                {
                    Debug.WriteLine("E3001");
                }
                else if (selected < -1)
                {
                    if (selected == -2)
                    {
                        if(conflictChecker.PortalInteract(moveAction, MapData.ListMap))
                        {
                            if (moveAction.PortalKey != null)
                            {
                                NavigateToPage(new BattlePage("Boss"));
                                End = true;
                            }
                            else
                            {
                                txtBlkInteract.Text = "You don't have key!";
                                gridInteract.Visibility = Visibility.Visible;
                            }
                        }
                    }
                    else if (selected == -3)
                    {
                        NavigateToPage(new PartyMenuPage(1));
                    }
                }
            }

            //test.Text = "Room " + MapData.RoomID + "; Count: " + App.levelList.LevelsList.Count;
        }

        private void SwitchLevel(AMoveAction player)
        {
            Dictionary<int, int> switchPos = new Dictionary<int, int>() { { 0, 16 }, { 17, 1 } };
            var path = MapData.PathsLevel.Where(p => p.PathPos.X == player.Pos.X || p.PathPos.Y == player.Pos.Y).FirstOrDefault();
            var newMap = App.levelList.LevelsList.Where(m => m.RoomID == path.RoomID).FirstOrDefault();
            var X = (int)player.Pos.X;
            var Y = (int)player.Pos.Y;

            if (path.PathPos.X == 0 || path.PathPos.X == 17)
            {
                X = switchPos[(int)path.PathPos.X];
            }

            if (path.PathPos.Y == 0 || path.PathPos.Y == 17)
            {
                Y = switchPos[(int)path.PathPos.Y];
            }

            MapData.SetZeroOnMapData(player.LastPos);

            player.Pos = new Point(X, Y);

            LoadMapData(newMap);
        }

        private void AnimationCompleted(object sender, EventArgs e)
        {
            if (conflictChecker.PlayerNextLevel(moveAction, MapData.ListMap))
            {
                SwitchLevel(moveAction);
            }
            moveAction.StoryboardAnimation.AnimationCompleted();
            MapData.SetPlayerOnMapData(moveAction);
            UpdateMap();
        }
        private void ResetEvent()
        {
            App.window.KeyDown -= MenuKeyDown;
            MainTimer.Stop();
            //moveAction = new PlayerMoveAction();
        }
        private void LoadMapData(MapData mapData)
        {
            ResetCanvas();
            CharacterField.Children.RemoveRange(2, CharacterField.Children.Count - 2);
            MapData = mapData;
            MapData.CreateEnemyElement(CharacterField);
            MapData.SetPathsToLevels();
            if (MapData.Portal != null)
            {
                setCharacter.SetPortalOnMap(MapData.Portal, CharacterField);
                MapData.ShowPortalElement(PortalCanvas, true);
                var image = (Image)PortalCanvas.Children[0];
                mapData.Portal.SpriteAnimation.CreateSprite(Key.Down, image, true);
            }
            else
            {
                MapData.ShowPortalElement(PortalCanvas, false);
            }

            setCharacter.SetEnemyOnMap(MapData.EnemyList, CharacterField);
            setCharacter.SetPlayerOnMap(moveAction, MapCanvas, CharacterField);

            SetPathsNumber();
            //testMap.Content = MapData.TestMap();
        }

        private void ResetCanvas()
        {
            ResetAnimation(MapCanvas);
            Canvas.SetLeft(MapCanvas, 0);
            Canvas.SetTop(MapCanvas, 0);
            ResetAnimation(PlayerCanvas);
            Canvas.SetLeft(PlayerCanvas, moveAction.BegPos.X);
            Canvas.SetTop(PlayerCanvas, moveAction.BegPos.Y);
        }
        private void ResetAnimation(Canvas canvas)
        {
            canvas.BeginAnimation(Canvas.LeftProperty, null);
            canvas.BeginAnimation(Canvas.TopProperty, null);
        }

        private void CreateTimer()
        {
            MainTimer.Interval = new TimeSpan(0,0,1);
            MainTimer.Tick += new EventHandler(TimerUpdate);
        }
        private void TimerUpdate(object sender, EventArgs e)
        {
            EnemyWalk();
            //test.Text = time.ToString();
            UpdateMap();
            time++;
        }
        private void EnemyWalk()
        {
            var enemyList = MapData.EnemyList;

            for (int i = 0; i < enemyList.Count; i++)
            {
                var enemyMove = enemyList[i];
                if (!enemyMove.MoveSettings.CheckSteps(time))
                {
                    enemyMove.GetDirection(enemyMove.MoveSettings.Direction, MapGrid.RowDefinitions.Count - 1);
                    enemyMove.StoryboardAnimation.AnimationCompleted();

                    var direction = enemyMove.MoveSettings.Direction;
                    var enemyCanvas = (Canvas)CharacterField.Children[i + 2];
                    var enemyImage = (Image)enemyCanvas.Children[0];

                    enemyMove.StoryboardAnimation.CreateStoryboard(direction, enemyCanvas);
                    enemyMove.SpriteAnimation.CreateSprite(direction, enemyImage);
                    enemyMove.StoryboardAnimation.MainStoryboard.Begin();
                    enemyMove.StoryboardAnimation.SetCanvasZIndex((int)enemyMove.Pos.Y, enemyCanvas);
                    CheckConflict(enemyMove);
                    MapData.SetEnemyOnMapData(enemyMove);
                }
                enemyMove.MoveSettings.StepsCount++;
            }
        }
        private void UpdateMap()
        {
            //testMap.Content = MapData.TestMap();
        }
        private void CheckConflict(AMoveAction character)
        {
            var conflict = false;

            if (character is PlayerMoveAction)
            {
                conflict = conflictChecker.PlayerConflict(character, MapData.ListMap);
            }
            else if (character is EnemyMoveAction)
            {
                conflict = conflictChecker.EnemyConflict(character, MapData.ListMap);
            }

            if (conflict)
            {
                var enemyList = MapData.EnemyList;
                var index = enemyList.FindIndex(x => x.Pos == character.Pos);
                var hasKey = false;
                if (enemyList[index].PortalKey != null && moveAction.PortalKey == null)
                {
                    moveAction.PortalKey = enemyList[index].PortalKey;
                    enemyList[index].PortalKey = null;
                    hasKey = true;
                }
                NavigateToPage(new BattlePage(enemyElement, hasKey));
                
                CharacterField.Children.RemoveAt(index + 2);
                enemyList.RemoveAt(index);
            }
        }
        private void SetPathsNumber()
        {
            Dictionary<int, int> switchLast = new Dictionary<int, int>() { { 8, 9 }, { 0, 0 }, { 17, 17 } };
            Paths.Children.Clear();
            var paths = MapData.PathsLevel;
            for (int i = 0; i < paths.Count; i++)
            {
                Grid grid = new Grid() { Background = Brushes.Brown };
                Paths.Children.Add(grid);
                Grid.SetColumn(grid, (int)paths[i].PathPos.X);
                Grid.SetRow(grid, (int)paths[i].PathPos.Y);
                Grid grid2 = new Grid() { Background = Brushes.Brown };
                Paths.Children.Add(grid2);
                Grid.SetColumn(grid2, switchLast[(int)paths[i].PathPos.X]);
                Grid.SetRow(grid2, switchLast[(int)paths[i].PathPos.Y]);
            }
        }
        private void SetMapImage(string enemyElement)
        {
            BitmapImage bg = new BitmapImage() { UriSource = new Uri(App.resourcePaths.GetMapBG(enemyElement)) };
            BitmapImage wall = new BitmapImage() { UriSource = new Uri(App.resourcePaths.GetMapWall(enemyElement)) };
            BitmapImage floor = new BitmapImage() { UriSource = new Uri(App.resourcePaths.GetMapFloor(enemyElement)) };
            BitmapImage path = new BitmapImage() { UriSource = new Uri(App.resourcePaths.GetMapPath(enemyElement)) };
            MainGrid.Background = new ImageBrush(bg);
            MapWalls.Background = new ImageBrush(wall);
            MapField.Background = new ImageBrush(path);

            Dictionary<int, int> switchLast = new Dictionary<int, int>() { { 8, 9 }, { 0, 0 }, { 17, 17 } };

            Paths.Children.Clear();
            var paths = MapData.PathsLevel;
            for (int i = 0; i < paths.Count; i++)
            {
                Grid grid = new Grid() { Background = new ImageBrush(path) };
                Grid grid2 = new Grid() { Background = new ImageBrush(path) };
                Paths.Children.Add(grid);
                Paths.Children.Add(grid2);
                Grid.SetColumn(grid, (int)paths[i].PathPos.X);
                Grid.SetRow(grid, (int)paths[i].PathPos.Y);
                Grid.SetColumn(grid, switchLast[(int)paths[i].PathPos.X]);
                Grid.SetRow(grid, switchLast[(int)paths[i].PathPos.Y]);
            }
        }
        private void NavigateToPage(Page page)
        {
            if (page != null)
            {
                App.MainFrame.NavigationService.Navigate(page);
                ResetEvent();
            }
        }
    }
}
