using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainDFF
{
    public abstract class AMenuSelectAction
    {
        public int MoveCursor(int direction, int current, int max)
        {
            switch (direction)
            {
                case 0:
                    if (current > 0)
                    {
                        return current - 1;
                    }
                    else
                    {
                        return max;
                    }
                case 1:
                    if (current < max)
                    {
                        return current + 1;
                    }
                    else
                    {
                        return 0;
                    }
                default:
                    return current;
            }
        }
    }
}
