using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MainDFF.Classes.Battle
{
    class SetCharacterOnField
    {
        public void SetPlayerOnField(List<PlayerCharacter> playerList, Grid playerField, Grid playerMenu)
        {
            for (int i = 0; i < playerList.Count; i++)
            {
                var player = playerList[i];
                CreatePlayerElement(i, player, playerField);
                SetPlayerStatus(i, player, playerMenu);
            }
        }
        private void CreatePlayerElement(int row, PlayerCharacter player, Grid playerField)
        {
            Image image = new Image();
            BitmapImage source = new BitmapImage();

            source.BeginInit();
            source.UriSource = new Uri("pack://application:,,,/Resources/CharacterSprites/Battle/" + player.CharacterID + "/" + player.CharacterAnimationList[0].SpriteFileName);
            source.EndInit();
            image.Source = source;
            image.Width = source.PixelWidth;
            image.Height = source.PixelHeight;

            Grid grid = new Grid()
            {
                Width = source.PixelWidth / player.CharacterAnimationList[0].SpriteRowCol.X,
                Height = source.PixelHeight / player.CharacterAnimationList[0].SpriteRowCol.Y,
            };
            TextBlock txtBlk = new TextBlock();
            txtBlk.Style = (Style)(Application.Current.FindResource("PlayerDamageInfo"));

            grid.Children.Add(txtBlk);

            Canvas.SetLeft(image, 0);
            Canvas.SetTop(image, 0);
            Canvas canvas = new Canvas() {
                Width = source.PixelWidth / player.CharacterAnimationList[0].SpriteRowCol.X,
                Height = source.PixelHeight / player.CharacterAnimationList[0].SpriteRowCol.Y};
            canvas.Style = (Style)(Application.Current.FindResource("PlayerCanvas"));
            canvas.Children.Add(image);
            canvas.Children.Add(grid);

            Grid.SetRow(canvas, row);
            Grid.SetColumn(canvas, 1);

            playerField.Children.Add(canvas);
            var canvasImage = (Canvas)playerField.Children[row];
            player.SwitchAnimation(canvasImage, 0, App.resourcePaths.GetPlayerPath(player.CharacterID), null);
        }
        public void SetPlayerStatus(int row, PlayerCharacter player, Grid playerMenu)
        {
            var playerGrid = (Grid)playerMenu.Children[row + 1];
            var name = (TextBlock)playerGrid.Children[0];
            var hpStack = (StackPanel)playerGrid.Children[1];
            var hpStackValue = (TextBlock)((StackPanel)(hpStack.Children[0])).Children[1];
            var hpBar = (ProgressBar)hpStack.Children[1];
            var mpStack = (StackPanel)playerGrid.Children[2];
            var mpStackValue = (TextBlock)((StackPanel)(mpStack.Children[0])).Children[1];
            var mpBar = (ProgressBar)mpStack.Children[1];
            var spBar = (ProgressBar)((StackPanel)(playerGrid.Children[3])).Children[1];

            name.Text = player.Name;

            hpStackValue.Text = player.CharacterStatus.CurrentHP.ToString();
            mpStackValue.Text = player.CharacterStatus.CurrentMP.ToString();

            hpBar.Maximum = player.CharacterStats.HP;
            mpBar.Maximum = player.CharacterStats.MP;
            spBar.Maximum = player.CharacterStats.SP;

            hpBar.Value = player.CharacterStatus.CurrentHP;
            mpBar.Value = player.CharacterStatus.CurrentMP;
            spBar.Value = player.CharacterStatus.CurrentSP;
        }
        public void SetEnemyOnField(List<EnemyCharacter> enemyList, Grid enemyField, Grid enemyMenu, bool boss = false)
        {
            int col = 0;
            int rowOffset = 0;
            for (int i = 0; i < enemyList.Count; i++)
            {
                var enemy = enemyList[i];
                CreateEnemyElement(i - rowOffset, col, enemy, enemyField, boss);
                SetEnemyStatus(i, enemy, enemyMenu);
                if ((i + 1) % 3 == 0)
                {
                    col++;
                    rowOffset += 3;
                }
            }
        }
        private void CreateEnemyElement(int row, int col, EnemyCharacter enemy, Grid enemyField, bool boss = false)
        {
            Image image = new Image();
            BitmapImage source = new BitmapImage();
            image.Source = null;
            source.BeginInit();
            if (boss)
            {
                source.UriSource = new Uri("pack://application:,,,/Resources/CharacterSprites/Monsters/Boss/" + enemy.CharacterID + "/" + enemy.CharacterAnimationList[0].SpriteFileName);
            }
            else
            {
                source.UriSource = new Uri("pack://application:,,,/Resources/CharacterSprites/Monsters/" + enemy.CharacterID + "/" + enemy.CharacterAnimationList[0].SpriteFileName);
            }
            source.EndInit();
            image.Source = source;
            image.Width = source.PixelWidth;
            image.Height = source.PixelHeight;

            Grid grid = new Grid()
            {
                Width = source.PixelWidth / enemy.CharacterAnimationList[0].SpriteRowCol.X,
                Height = source.PixelHeight / enemy.CharacterAnimationList[0].SpriteRowCol.Y,
            };
            TextBlock txtBlk = new TextBlock();
            txtBlk.Style = (Style)(Application.Current.FindResource("EnemyDamageInfo"));

            grid.Children.Add(txtBlk);

            Canvas.SetLeft(image, 0);
            Canvas.SetTop(image, 0);
            Canvas canvas = new Canvas()
            {
                Width = source.PixelWidth / enemy.CharacterAnimationList[0].SpriteRowCol.X,
                Height = source.PixelHeight / enemy.CharacterAnimationList[0].SpriteRowCol.Y
            };
            canvas.Style = (Style)(Application.Current.FindResource("EnemyCanvas"));
            canvas.Children.Add(image);
            canvas.Children.Add(grid);

            if (boss)
            {
                Grid.SetRow(canvas, 1);
            }
            else
            {
                Grid.SetRow(canvas, row);
            }
            Grid.SetColumn(canvas, col);

            enemyField.Children.Add(canvas);
            var canvasImage = (Canvas)enemyField.Children[row];
            enemy.SwitchAnimation(canvasImage, 0, App.resourcePaths.GetEnemyPath(enemy.CharacterID), null);
        }
        private void SetEnemyStatus(int row, EnemyCharacter enemy, Grid enemyMenu)
        {
            var enemyStack = (StackPanel)enemyMenu.Children[row + 2];
            var name = (TextBlock)enemyStack.Children[1];

            name.Text = enemy.Name;
        }
        public void DeleteEnemyElement(EnemyCharacter enemy, List<EnemyCharacter> EnemyList, Grid enemyField, Grid enemyMenu)
        {
            for (int i = 0; i < EnemyList.Count; i++)
            {
                if (EnemyList[i] == enemy)
                {
                    var grid = (Canvas)enemyField.Children[i];
                    DoubleAnimation fadeOut = new DoubleAnimation();
                    fadeOut.From = 1;
                    fadeOut.To = 0;
                    fadeOut.Duration = new Duration(TimeSpan.FromMilliseconds(200));
                    grid.BeginAnimation(UIElement.OpacityProperty, fadeOut);

                    DeleteEnemyStatus(i, enemyMenu);
                }
            }
        }
        public void DeleteEnemyStatus(int row, Grid enemyMenu)
        {
            var enemyStack = (StackPanel)enemyMenu.Children[row + 2];
            var name = (TextBlock)enemyStack.Children[1];

            name.Text = "";
        }
        public void SetCharacterOrder(CharactersLists charactersLists, ListBox characterOrder)
        {
            characterOrder.Items.Clear();
            foreach (ACharacter c in charactersLists.CharacterOrder)
            {
                Rectangle rect = new Rectangle() { Width = 52, Height = 28 };
                rect.Style = (Style)Application.Current.FindResource("IconRectangle");

                Image img = new Image();
                BitmapImage source = new BitmapImage();

                source.BeginInit();
                if (c is PlayerCharacter)
                {
                    source.UriSource = new Uri("pack://application:,,,/Resources/CharacterSprites/Battle/" + c.CharacterID + "/Icon.png");
                }
                else if (c is EnemyCharacter)
                {
                    source.UriSource = new Uri("pack://application:,,,/Resources/CharacterSprites/Monsters/Icon.png");
                }
                source.EndInit();
                img.Source = source;

                Grid grid = new Grid();
                grid.Style = (Style)Application.Current.FindResource("IconGrid");

                grid.Children.Add(rect);
                grid.Children.Add(img);

                characterOrder.Items.Add(grid);
            }
        }
    }
}
