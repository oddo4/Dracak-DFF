using MainDFF.Interface.Inventory;
using MainDFF.Interface.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainDFF.Classes.Items.Usable
{
    public class Ether : AItemType, IStatMP, IBuyable, ISellable
    {
        public double MPValue { get; set; }
        public bool IsBuyable { get; set; }
        public bool IsSellable { get; set; }
    }
}
