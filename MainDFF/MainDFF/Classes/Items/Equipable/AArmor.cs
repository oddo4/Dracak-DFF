using MainDFF.Interface.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainDFF.Classes.Items.Equipable
{
    public class AArmor : AItemType, IBuyable, ISellable
    {
        public bool IsBuyable { get; set; }
        public bool IsSellable { get; set; }
    }
}
