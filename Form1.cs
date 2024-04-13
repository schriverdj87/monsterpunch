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
using System.Media;


namespace MonsterPunch
{
    public partial class MonsterPunchFront : Form
    {
        private Player stanley;
        private CoreSlideAndLaunch coreGame;
        private List<PictureBox> enemies;
        private int enemiesSize = 50;
        private SoundPlayer sndSwing;
        private SoundPlayer sndHit;
        private SoundPlayer sndGameOver;
        private int GameOverCountdown = 35;

        public MonsterPunchFront()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.TopLevel = false;
            coreGame = new CoreSlideAndLaunch();
            this.KeyDown += Key_Down;
            enemies = new List<PictureBox>();
            sndSwing = new SoundPlayer("snd\\swing.wav");
            sndSwing.Load();

            sndHit = new SoundPlayer("snd\\hit.wav");
            sndHit.Load();

            sndGameOver = new SoundPlayer("snd\\gameover.wav");
            sndGameOver.Load();


        }


        private void Form1_Load(object sender, EventArgs e)
        {
            stanley = new Player();
            this.Controls.Clear();
            this.Controls.Add(stanley);
            this.Click += ClickEvent;
           
        }
        /// <summary>
        /// Should be fired off 30 times per second.
        /// </summary>
        public void Crank()
        {

            if (coreGame.sndFist == true)
            {
                sndSwing.Play();
            }

            bool gameOnBefore = coreGame.gameOn;
            coreGame.crank();
            bool gameOnAfter = coreGame.gameOn;

            if (coreGame.sndHit == true)
            {
                sndHit.Play();
            }

            if (gameOnBefore == true && gameOnAfter == false)
            {
                sndGameOver.Play();
            }


            //Synchronize the player avatar to the game location.
            double padding = 50;

            stanley.Left = Convert.ToInt32((coreGame.GetPlayerX() * 0.01 * (this.Size.Width - padding)) - stanley.Width / 2);
            stanley.Top = Convert.ToInt32((coreGame.GetPlayerY() * 0.01 * (this.Size.Height - padding)) - stanley.Height / 2);
            stanley.Left += Convert.ToInt32(padding / 2);
            stanley.Top += Convert.ToInt32(padding / 2);

            //Display enemies

            List<Point> enemiesLoci = coreGame.GetEnemyLoci();

            //Balance the number of enemies showing
            while (enemies.Count < enemiesLoci.Count)
            {
                PictureBox pictureBoxToAdd = new PictureBox();
                pictureBoxToAdd.Width = pictureBoxToAdd.Height = enemiesSize;
                pictureBoxToAdd.Image = Image.FromFile("img\\nme.png");
                pictureBoxToAdd.SizeMode = PictureBoxSizeMode.StretchImage;
                this.Controls.Add(pictureBoxToAdd);
                this.enemies.Add(pictureBoxToAdd);
            }


            while (enemies.Count > enemiesLoci.Count)
            {
                this.Controls.Remove(enemies[0]);
                enemies.RemoveAt(0);
            }

            //Sync the picture boxes to the enemies.
            for (int a = 0; a < enemies.Count; a++)
            {
                enemies[a].Left = Convert.ToInt32((enemiesLoci[a].X * 0.01 * (this.Size.Width - padding)) - enemiesSize / 2);
                enemies[a].Top = Convert.ToInt32((enemiesLoci[a].Y * 0.01 * (this.Size.Height - padding)) );
                enemies[a].Left += Convert.ToInt32(padding / 2);
                enemies[a].Top += Convert.ToInt32(padding / 2);

            }

            //Countdown on game over.

            if (coreGame.gameOn == false)
            {
                GameOverCountdown--;
            }

        }
        
        //Queue to remove from stage.
        public bool KillMe ()
        {
            return GameOverCountdown <= 0 && coreGame.gameOn == false;
        }

        public int GetScore()
        {
            return coreGame.score;
        }

        private void Key_Down (object sender, EventArgs e)
        {
            KeyEventArgs leKey = (KeyEventArgs)e;

            if (leKey.KeyValue == 32)//Space, launch fist.
            {
                coreGame.LaunchFist();
            }
        }

        private void ClickEvent (object sender, EventArgs e)
        {
           coreGame.LaunchFist();
            
        }
    }
}
