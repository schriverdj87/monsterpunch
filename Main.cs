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
    public partial class Main : Form
    {

        MonsterPunchMenu leMenu ;
        MonsterPunchFront leGame;
        int HighScore = 0;
        String thisTitle = "Monster Punch";
        private Timer engine = new Timer()
        {
            Interval = 33
        };

        public Main()
        {
            InitializeComponent();
           
        }

        private void Main_Load(object sender, EventArgs e)
        {
            Reset();
            engine.Tick += EngineFn;
            engine.Start();
            this.KeyDown += BTNPlay_FN;
            
        }

        private void Reset()
        {
            
            Controls.Clear();
            leMenu = new MonsterPunchMenu();
            Controls.Add(leMenu);
            leMenu.btnStart.Click += BTNPlay_FN;
            leMenu.Show();
            
        }

        private void BTNPlay_FN (object sender, EventArgs e)
        {

            leMenu.Close();
            this.Controls.Clear();
            leGame = new MonsterPunchFront();
            this.Controls.Add(leGame);
            leGame.Show();
            leGame.Focus();

        }
        /// <summary>
        /// Main method that runs about 30 times a second.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EngineFn (object sender, EventArgs e)
        {
            if (leGame != null)
            {
                leGame.Crank();
                //Display the score.
                this.Text = thisTitle + " - " + Convert.ToString(leGame.GetScore());

                if (leGame.KillMe() == true)
                {
                    if (leGame.GetScore() != 0)
                    {
                        //Set high Score.
                        if (leGame.GetScore() > this.HighScore)
                        {
                            this.HighScore = leGame.GetScore();
                        }

                        //Put high score.
                        this.Text = thisTitle + " - High Score: " + Convert.ToString(this.HighScore);
                    }

                    leGame = null;
                    Reset();
                    
                }
            }

            
        }
    }
}
