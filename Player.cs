//David Schriver - April 2024
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace MonsterPunch
{
    public class Player : PictureBox
    {
        private string myImg = "img\\fist.png";
        private Point mySize = new Point(50, 50);

        public Player()
        {
            this.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Image = Image.FromFile(myImg);
            Width = mySize.X;
            Height = mySize.Y;
            
            
        }
    }
}
