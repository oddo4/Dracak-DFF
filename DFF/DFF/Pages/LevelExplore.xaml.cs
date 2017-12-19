using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using WpfAnimatedGif;

namespace DFF.Pages
{
    /// <summary>
    /// Interakční logika pro LevelExplore.xaml
    /// </summary>
    public partial class LevelExplore : Page
    {
        Window window;
        List<Classes.TileMap> LevelList = new List<Classes.TileMap>();
        Classes.TileMap LevelMap = new Classes.TileMap();
        Classes.PlayerMovement Player;
        List<Classes.EnemyPosition> EnemyList = new List<Classes.EnemyPosition>();
        DispatcherTimer MainTimer = new DispatcherTimer();
        Random rand = new Random();
        ParserContext context = new ParserContext();
        int c;

        public LevelExplore()
        {
            InitializeComponent();
            MainTimer.Interval = new TimeSpan(0, 0, 1);
            MainTimer.Tick += new EventHandler(EnemyMove);
            MainTimer.Start();
            Player = new Classes.PlayerMovement(1,1);
            ParserContext(context);
            CreateTileMap();
            SpawnEnemy(1);

            LevelMap.SetPlayerOnMap(MapCanvas, HeroCanvas, Player.PosX, Player.PosY);
        }

        private void DHeroAniCmp(object sender, EventArgs e)
        {
            Player.HeroAniCmp = true;
            Player.ZIndexChange(HeroCanvas);
            test2.Content = LevelMap.TestMap();
        }
        private void DEnemyAniCmp(object sender, EventArgs e)
        {
            foreach (Classes.EnemyPosition Enemy in EnemyList)
            {
                Enemy.EnemyAniCmp = true;
            }
            test2.Content = LevelMap.TestMap();
        }

        private void HeroKeyDown(object sender, RoutedEventArgs e)
        {
            window = Window.GetWindow(this);
            window.KeyDown += HeroMove;
        }

        private void HeroMove(object sender, KeyEventArgs e)
        {
            var Storyboard = new Storyboard();
            Storyboard.Completed += new EventHandler(DHeroAniCmp);

            if (Player.HeroAniCmp && Player.LastKey != e.Key && !e.IsRepeat)
            {
                Player.MoveDirection(e.Key, HeroImage);
            }
            else if ((Player.HeroAniCmp && e.IsDown) || (Player.HeroAniCmp && Player.LastKey == e.Key))
            {
                if (Player.CheckMap(e.Key, MapCanvas, HeroCanvas, LevelMap, Storyboard, EnemyList))
                {
                    Player.Move(e.Key, HeroImage, HeroCanvas);
                    Storyboard.Begin();
                    LevelMap.PlayerPosition(e.Key, Player.PosX, Player.PosY);

                    if (Player.BattleStart)
                    {
                        LevelExplore levelpage = this;
                        this.NavigationService.Navigate(new NormalBattle(0, Player.BattleStart, MainTimer, levelpage));
                        MainTimer.Stop();
                        Player.BattleStart = false;
                        window.KeyDown -= HeroMove;
                        for (int i = 0; i < EnemyList.Count; i++)
                        {
                            if (EnemyList[i].PosX == Player.PosX && EnemyList[i].PosY == Player.PosY)
                            {
                                EnemyList.RemoveAt(i);
                                Characters.Children.RemoveAt(i + 1);
                            }
                        }
                        LevelMap.PlayerClearAround(Player.PosX, Player.PosY);
                    }
                }
            }

            test.Content = Player.PosX + "," + Player.PosY + ";" + Canvas.GetLeft(MapCanvas) + "," + Canvas.GetTop(MapCanvas) + ";" + Canvas.GetZIndex(HeroCanvas);
        }

        private void EnemyMove(object sender, EventArgs e)
        {
            for (int i = 0; i < EnemyList.Count; i++)
            {
                var Storyboard = new Storyboard();
                Storyboard.Completed += new EventHandler(DEnemyAniCmp);

                Canvas childEnemy = (Canvas)Characters.Children[i + 1];
                Image childImage = (Image)childEnemy.Children[0];

                if (EnemyList[i].EnemyAniCmp && EnemyList[i].StepsCtr > 0 && EnemyList[i].EnemyAlert == false)
                {
                    if (EnemyList[i].Move(childEnemy, childImage, LevelMap))
                    {
                        EnemyList[i].MoveAnimation(Storyboard, childEnemy);
                        Storyboard.Begin();
                        LevelMap.EnemyRange(EnemyList[i].Direction, EnemyList[i]);
                        LevelMap.CheckConflict(EnemyList[i].Direction, Player.PosX, Player.PosY);
                        EnemyList[i].StepsCtr--;
                    }
                }
                else if (EnemyList[i].StepsCtr == 0)
                {
                    EnemyList[i].MoveDirection(childImage);
                    EnemyList[i].StepsCtr = EnemyList[i].Steps;
                }
                else if (EnemyList[i].EnemyAlert)
                {
                    EnemyList[i].BattleStart = true;
                    LevelExplore levelpage = this;
                    this.NavigationService.Navigate(new NormalBattle(0, EnemyList[i].BattleStart, MainTimer, levelpage));
                    MainTimer.Stop();
                    EnemyList[i].BattleStart = false;
                    window.KeyDown -= HeroMove;
                    EnemyList.RemoveAt(i);
                    Characters.Children.RemoveAt(i + 1);
                    LevelMap.PlayerClearAround(Player.PosX, Player.PosY);
                }
            }

            c++;
            test3.Content = c;
            test2.Content = LevelMap.TestMap();
        }

        public void SpawnEnemy(int Count)
        {
            for (int i = 0; i < Count; i++)
            {
                Classes.EnemyPosition Enemy = new Classes.EnemyPosition(2, 3, 0, 1, 1, 4);

                if (LevelMap.EnemyPosition(Enemy.PosX, Enemy.PosY, Enemy.Type, Enemy.Steps))
                {
                    StringBuilder EnemySB = new StringBuilder();
                    EnemySB.Append(@"<Canvas x:Name='EnemyCanvas" + i + "' Canvas.ZIndex='" + Enemy.PosY + "'  Height='58' Width='58' Canvas.Top='91' Canvas.Left='132' ClipToBounds='True'>");
                    EnemySB.Append(@"<Image x:Name='EnemyImage" + i + "' Canvas.ZIndex='0' Source='/Anim/EnemyIdle.png' Width='232' Canvas.Top='0' Canvas.Left='0'/>");
                    EnemySB.Append(@"</Canvas>");

                    UIElement newEnemy = (UIElement)XamlReader.Parse(EnemySB.ToString(), context);
                    Characters.Children.Add(newEnemy);

                    Canvas childEnemy = (Canvas)Characters.Children[i + 1];

                    LevelMap.SetEnemyOnMap(MapCanvas, (Canvas)childEnemy, Enemy.PosX, Enemy.PosY);
                    EnemyList.Add(Enemy);
                }
                else
                {
                    i--;
                }
            }
        }
        
        public void CreateTileMap()
        {
            for (int i = 0; i < LevelMap.Map.GetLength(0) + 2; i++)
            {
                for (int j = 0; j < LevelMap.Map.GetLength(1) + 2; j++)
                {
                    StringBuilder TileSB = new StringBuilder();
                    if (i == 0 || i == LevelMap.Map.GetLength(0) + 1 || j == 0 || j == LevelMap.Map.GetLength(1) + 1)
                    {
                        TileSB.Append("<Image x:Name='Tile" + i + "" + j + "' Source='/Anim/TileDirt.png' Stretch='None' Grid.Row='" + j + "' Grid.Column='" + i + "'/>");
                    }
                    else
                    {
                        TileSB.Append("<Image x:Name='Tile" + i + "" + j + "' Source='/Anim/TileGrass.png' Stretch='None' Grid.Row='" + j + "' Grid.Column='" + i + "'/>");
                    }
                    

                    UIElement newTile = (UIElement)XamlReader.Parse(TileSB.ToString(), context);
                    Map.Children.Add(newTile);
                }
            }
        }

        private void ParserContext(ParserContext context)
        {
            context.XmlnsDictionary.Add("", "http://schemas.microsoft.com/winfx/2006/xaml/presentation");
            context.XmlnsDictionary.Add("x", "http://schemas.microsoft.com/winfx/2006/xaml");
            context.XmlnsDictionary.Add("mc", "http://schemas.openxmlformats.org/markup-compatibility/2006");
            context.XmlnsDictionary.Add("d", "http://schemas.microsoft.com/expression/blend/2008");
            context.XmlnsDictionary.Add("local", "clr-namespace:DFF");
        }
    }
}
