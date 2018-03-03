using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace MainDFF.Classes.Battle
{
    class SetCharacterOnField
    {
        public void SetPlayerOnField(List<PlayerCharacter> playerList, Grid playerField)
        {
            for (int i = 0; i < playerList.Count; i++)
            {
                var player = playerList[i];
                CreatePlayerElement(i, player.FileID, playerField);
            }
        }
        private void CreatePlayerElement(int row, int id, Grid playerField)
        {
            Image image = new Image();
            BitmapImage source = new BitmapImage();

            source.BeginInit();
            source.UriSource = new Uri("pack://application:,,,/Resources/CharacterSprites/Battle/" + id + "/Idle.png");
            source.EndInit();
            image.Source = source;

            Canvas.SetLeft(image, 0);
            Canvas.SetTop(image, 0);
            Canvas canvas = new Canvas();
            canvas.Children.Add(image);

            Grid.SetRow(canvas, row);
            Grid.SetColumn(canvas, 1);

            playerField.Children.Add(canvas);
        }
    }
}
