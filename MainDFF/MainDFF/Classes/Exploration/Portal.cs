using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MainDFF.Classes.Exploration
{
    public class Portal
    {
        public Point PortalPos { get; set; }
        public int RoomID { get; set; }
        public ExploreAnimation SpriteAnimation = new ExploreAnimation();
    }
}
