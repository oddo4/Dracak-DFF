using MainDFF.Classes;
using MainDFF.Classes.Battle;
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
        CharactersLists charactersLists = new CharactersLists();
        int time = 0;
        string enemyElement;
        public LevelPlayPage(int levelID)
        {
            InitializeComponent();
            enemyElement = App.fileHelper.LoadEnemyElement(levelID);
            mapData.SetPlayerOnMapData(moveAction);
            CreateTimer();
            CreateEnemy(4);
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
                    CheckConflict(moveAction);
                }
                else if (selected == -1)
                {
                    Debug.WriteLine("E3001");
                }
                else if (selected < -1)
                {
                    NavigateToPage(new PartyMenuPage(1));
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
            var offsetX = 0;
            var offsetY = 0;
            for (int i = 0; i < Count; i++)
            {
                Image image = new Image() { Height = 232, Width = 232};
                image.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/CharacterSprites/Exploration/EnemyWalk.png"));
                Canvas.SetLeft(image, 0);
                Canvas.SetTop(image, 0);
                Canvas canvas = new Canvas() { Height = 58, Width = 58, ClipToBounds = true, Margin = new Thickness(0, -23, 0, 0) };
                Canvas.SetLeft(canvas, 291);
                Canvas.SetTop(canvas, 211);

                canvas.Children.Add(image);
                Canvas.SetZIndex(canvas, 1);
                MapCanvas.Children.Add(canvas);

                var X = rand.Next(3 + offsetX, 6 + offsetX);
                var Y = rand.Next(3 + offsetY, 6 + offsetY);

                EnemyMoveAction newEnemyMove = new EnemyMoveAction(new Point(X, Y), rand);
                mapData.SetEnemyOnMapData(newEnemyMove);
                enemyList.Add(newEnemyMove);

                offsetX += 5;
                if (offsetX > 5)
                {
                    offsetX = 0;
                    offsetY += 5;
                }
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
                    CheckConflict(enemyMove);
                    mapData.SetEnemyOnMapData(enemyMove);
                    enemyMove.MoveSettings.StepsCount++;
                }
            }
        }
        private void UpdateMap()
        {
            testMap.Content = mapData.TestMap();
        }
        private void CheckConflict(AMoveAction character)
        {
            if (conflictChecker.Conflict(character, mapData.ListMap))
            {
                NavigateToPage(new BattlePage(enemyElement));
                var index = enemyList.FindIndex(x => x.Pos == character.Pos);
                MapCanvas.Children.RemoveAt(index + 1);
                enemyList.RemoveAt(index);
            }
        }
        private void NavigateToPage(Page page)
        {
            if (page != null)
            {
                NavigationService.Navigate(page);
                ResetEvent();
            }
        }
    }
}
