using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MainDFF.Classes
{
    class TileMap
    {
        public Point BegPos = new Point(147, 115);

        public void MoveMap(Grid MapGrid, Point Pos)
        {
            Canvas.SetLeft(MapGrid, BegPos.X + (Pos.X * -26));
            Canvas.SetTop(MapGrid, BegPos.Y + (Pos.Y * -26));
        }
    }
}
