using MainDFF.Classes;
using MainDFF.Classes.ControlActions.MoveActions;
using MainDFF.Classes.Exploration;
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
        PlayerMoveAction moveAction = new PlayerMoveAction(new Point(0,1));
        SetCharacterOnMap setCharacter = new SetCharacterOnMap();
        List<EnemyMoveAction> enemyList = new List<EnemyMoveAction>();
        DispatcherTimer MainTimer = new DispatcherTimer();
        MapData mapData = new MapData();
        ConflictChecker conflictChecker = new ConflictChecker();
        int time = 0;
        public LevelPlayPage()
        {
            InitializeComponent();
            mapData.SetPlayerOnMapData(moveAction);
            CreateTimer();
            CreateEnemy(1);
            setCharacter.SetEnemyOnMap(enemyList, MapCanvas);
            setCharacter.SetPlayerOnMap(moveAction, MapCanvas);
        }
        private void MenuKey_Loaded(object sender, RoutedEventArgs e)
        {
            App.window.KeyDown += MenuKeyDown;
            MainTimer.Start();
        }
        private void MenuKeyDown(object sender, KeyEventArgs e)
        {
            if (moveAction.StoryboardAnimation.AnimationComplete)
            {
                moveAction.StoryboardAnimation.MainStoryboard.Completed += new EventHandler(AnimationCompleted);
                var max = MapGrid.RowDefinitions.Count - 1;
                var selected = moveAction.GetDirection(e.Key, max);
                if (selected > 0)
                {
                    moveAction.StoryboardAnimation.CreateStoryboard(e.Key, MapCanvas);
                    moveAction.SpriteAnimation.CreateSprite(e.Key, PlayerImage);
                    moveAction.StoryboardAnimation.MainStoryboard.Begin();                    
                }
                else if (selected == -1)
                {
                    Debug.WriteLine("E3001");
                }
                else if (selected < -1)
                {
                    NavigationService.Navigate(moveAction.NavigateToPage);
                    ResetEvent();
                }
            }
        }
        private void AnimationCompleted(object sender, EventArgs e)
        {
            moveAction.StoryboardAnimation.AnimationCompleted();
            mapData.SetPlayerOnMapData(moveAction);
            UpdateMap();
        }
        private void ResetEvent()
        {
            App.window.KeyDown -= MenuKeyDown;
            MainTimer.Stop();
            //moveAction = new PlayerMoveAction();
        }
        private void CreateEnemy(int Count)
        {
            Random rand = new Random();
            for (int i = 0; i < Count; i++)
            {
                Image image = new Image() { Height = 232, Width = 232, Margin = new Thickness(0, 0, 0, 29) };
                image.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/CharacterSprites/Exploration/EnemyWalk.png"));
                Canvas.SetLeft(image, 0);
                Canvas.SetTop(image, 0);
                Canvas canvas = new Canvas() { Height = 58, Width = 58, ClipToBounds = true };
                Canvas.SetLeft(canvas, 131);
                Canvas.SetTop(canvas, 77);

                canvas.Children.Add(image);
                Canvas.SetZIndex(canvas, 1);
                MapCanvas.Children.Add(canvas);

                var X = rand.Next(3, 11);
                var Y = rand.Next(3, 11);

                EnemyMoveAction newEnemyMove = new EnemyMoveAction(new Point(X, Y), rand);
                mapData.SetEnemyOnMapData(newEnemyMove);
                enemyList.Add(newEnemyMove);
            }
        }
        private void CreateTimer()
        {
            MainTimer.Interval = new TimeSpan(0,0,1);
            MainTimer.Tick += new EventHandler(TimerUpdate);
        }
        private void TimerUpdate(object sender, EventArgs e)
        {
            EnemyWalk();
            test.Text = time.ToString();
            UpdateMap();
            time++;
        }
        private void EnemyWalk()
        {
            for (int i = 0; i < enemyList.Count; i++)
            {
                if (!enemyList[i].MoveSettings.CheckSteps(time))
                {
                    var enemyMove = enemyList[i];
                    enemyMove.GetDirection(enemyMove.MoveSettings.Direction, MapGrid.RowDefinitions.Count - 1);
                    enemyMove.StoryboardAnimation.AnimationCompleted();

                    var direction = enemyMove.MoveSettings.Direction;
                    var enemyCanvas = (Canvas)MapCanvas.Children[i + 1];
                    var enemyImage = (Image)enemyCanvas.Children[0];

                    enemyMove.StoryboardAnimation.CreateStoryboard(direction, enemyCanvas);
                    enemyMove.SpriteAnimation.CreateSprite(direction, enemyImage);
                    enemyMove.StoryboardAnimation.MainStoryboard.Begin();
                    mapData.SetEnemyOnMapData(enemyMove);
                    enemyMove.MoveSettings.StepsCount++;
                }
            }
        }
        private void UpdateMap()
        {
            testMap.Content = mapData.TestMap();
        }
    }
}
