//David Schriver - April 2024

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MonsterPunch
{
    public partial class MonsterPunchMenu : Form
    {
        public MonsterPunchMenu()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.TopLevel = false;

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            
        }

        private void MonsterPunchMenu_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = Image.FromFile("img/logo.png");
        }
    }
}
