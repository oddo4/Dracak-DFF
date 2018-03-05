using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MainDFF.Classes
{
    public abstract class ASpriteAnimation
    {
        [JsonIgnore]
        public Point SpritePos = new Point(0, 0);
    }
}
