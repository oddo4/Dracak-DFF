using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MainDFF.Classes.Battle
{
    class SetCharacterOnField
    {
        public void SetPlayerOnField(List<PlayerCharacter> playerList, Grid playerField)
        {
            for (int i = 0; i < playerList.Count; i++)
            {
                var player = playerList[i];
                CreatePlayerElement(i, player, playerField);
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
            image.Width = source.Width;
            image.Height = source.Height;

            Canvas.SetLeft(image, 0);
            Canvas.SetTop(image, 0);
            Canvas canvas = new Canvas() {
                Width = source.Width / player.CharacterAnimationList[0].SpriteRowCol.X,
                Height = source.Height / player.CharacterAnimationList[0].SpriteRowCol.Y};
            canvas.Style = (Style)(Application.Current.FindResource("PlayerCanvas"));
            canvas.Children.Add(image);

            Grid.SetRow(canvas, row);
            Grid.SetColumn(canvas, 1);

            playerField.Children.Add(canvas);
            var img = (Image)((Canvas)playerField.Children[row]).Children[0];
            player.CharacterAnimationList[0].CreateSprite(img);
        }
        public void SetCharacterOrder(CharactersLists charactersLists, ListBox characterOrder)
        {            
            foreach (ACharacter c in charactersLists.CharacterOrder)
            {
                Rectangle rect = new Rectangle() { Width = 26, Height = 14 };
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
