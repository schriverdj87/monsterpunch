//David Schriver
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MonsterPunch
{
    class CoreSlideAndLaunchNME 
    {
        public int X = 0;
        public int Y = 0;
        public int Speed = 0;
        public CoreSlideAndLaunchNME (int X, int Y, int Speed)
        {
            this.X = X;
            this.Y = Y;
            this.Speed = Speed;
        }

        public Point toPoint ()
        {
            return new Point(this.X, this.Y);
        }
    }
}
