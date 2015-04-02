using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IngenieProject
{
    public class Coordinate
    {
        public Int32 X { get; set; }
        public Int32 Y { get; set; }

        public Coordinate()
        {
            this.X = 0;
            this.Y = 0;
        }

        public Coordinate(Int32 x, Int32 y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
