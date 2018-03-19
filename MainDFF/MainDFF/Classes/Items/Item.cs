using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainDFF.Classes.Items
{
    public class Item
    {
        public string Name { get; set; }
        public AItemType Type { get; set; }

        public Item(string name)
        {
            Name = name;
        }
    }
}
