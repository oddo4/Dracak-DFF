using MainDFF.Classes;
using MainDFF.Classes.ControlActions.MoveActions;
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

namespace MainDFF.Pages
{
    /// <summary>
    /// Interakční logika pro LevelPlayPage.xaml
    /// </summary>
    public partial class LevelPlayPage : Page
    {
        PlayerMoveAction moveAction = new PlayerMoveAction();
        TileMap tileMap = new TileMap();
        public LevelPlayPage()
        {
            InitializeComponent();
            CreateEnemy();
        }
        private void MenuKey_Loaded(object sender, RoutedEventArgs e)
        {
            App.window.KeyDown += MenuKeyDown;
        }
        private void MenuKeyDown(object sender, KeyEventArgs e)
        {
            if (moveAction.PlayerStoryboard.AnimationComplete)
            {
                moveAction.PlayerStoryboard.MainStoryboard.Completed += new EventHandler(AnimationCompleted);
                var max = MapGrid.RowDefinitions.Count - 1;
                var selected = moveAction.GetDirection(e.Key, max);
                if (selected > -1)
                {
                    switch (selected)
                    {
                        case 1:
                            moveAction.PlayerStoryboard.CreateStoryboard(e.Key, MapCanvas, PlayerGrid);
                            moveAction.SpriteAnimation.CreateSprite(e.Key, PlayerImage);
                            moveAction.PlayerStoryboard.MainStoryboard.Begin();
                            break;
                        default:
                            break;
                    }
                }
                else if (selected == -1)
                {
                    Debug.WriteLine("E3001");
                }
                else
                {
                    switch (selected)
                    {
                        case -2:
                            break;
                        case -3:
                            NavigationService.Navigate(new PartyMenuPage());
                            ResetEvent();
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        private void AnimationCompleted(object sender, EventArgs e)
        {
            moveAction.PlayerStoryboard.AnimationCompleted();
        }
        private void ResetEvent()
        {
            App.window.KeyDown -= MenuKeyDown;
            moveAction = new PlayerMoveAction();
        }
        private void CreateEnemy()
        {
            Image image = new Image() { Height = 232, Width = 232, Margin = new Thickness(0,0,0,29) };
            image.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/CharacterSprites/Exploration/EnemyWalk.png"));
            Canvas.SetLeft(image, 0);
            Canvas.SetTop(image, 0);
            Canvas canvas = new Canvas() { Height = 58, Width = 58, ClipToBounds = true };
            Canvas.SetLeft(canvas, 131);
            Canvas.SetTop(canvas, 77);

            canvas.Children.Add(image);
            Canvas.SetZIndex(canvas, 1);
            MapCanvas.Children.Add(canvas);
        }
    }
}
