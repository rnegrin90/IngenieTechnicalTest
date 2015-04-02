using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IngenieProject
{
    public class Wall
    {
        public Int32 top { get; set; }
        public Int32 right { get; set; }

        public Wall(Int32 x, Int32 y)
        {
            this.top = x;
            this.right = y;
        }
    }
}
