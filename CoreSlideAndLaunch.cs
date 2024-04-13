//David Schriver - April 2024

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterPunch
{
    public class CoreSlideAndLaunch
    {
        private Point player;
        private int playerSpeed = 4;
        private double playerStrength = 15;//How much force the player fist goes out with.
        private double playerGravity = 1.2;
        private double playerForce = 0;
        private List<CoreSlideAndLaunchNME> enemies = new List<CoreSlideAndLaunchNME>();
        private int enemiesSpeedBase = 1;
        private int enemiesSpawnPadding = 10; //How far away from the edges to spawn enemies.
        private Point enemiesSpawnRate = new Point(100, 30); //X = Max Y = Min
        private int enemiesSpawnTickDown = 30;
        private int punchDistanceY = 5;
        private int punchDistanceX = 10;
        public int score { get; private set; } = 0;
        public bool sndFist { get; private set; } = false;
        public bool sndHit { get; private set; } = false;
        public bool getHarder = true;//If true will make the game harder as the score progresses.
        public int getHarderAt = 5;//How many points to score to get harder.
        private int maxplayerSpeed = 8;
        private int maxenemySpeed = 5;
        

        public Boolean gameOn { get; private set; }//only runs if true.

        

        public CoreSlideAndLaunch()
        {
            player = new Point(50, 100);

            gameOn = true;
        }

      
       


        public void crank()
        {
            if (gameOn)
            {
                //Slide player left and right.
                if (FistLaunched() == false)
                {
                    this.player.X += playerSpeed;
                }

                //Keep it in bounds.
                if (player.X > 100)
                {
                    player.X = 100;
                }
                if (player.X < 0)
                {
                    player.X = 0;
                }

                //Change direction if at bound.
                if (player.X == 0 || player.X == 100)
                {
                    playerSpeed *= -1;
                }

                //Move the fist forward.
                this.player.Y -= Convert.ToInt32(this.playerForce);

                //Fall if not 0;
                if (FistLaunched())
                {
                    this.playerForce -= this.playerGravity;
                }

                //They've hit the ground.
                if (this.player.Y > 100)
                {
                    this.player.Y = 100;
                    this.playerForce = 0;
                }

                //Spawn enemies
                enemiesSpawnTickDown--;

                if (enemiesSpawnTickDown == 0)
                {
                    enemiesSpawnTickDown = new Random().Next(this.enemiesSpawnRate.Y,this.enemiesSpawnRate.X);

                    CoreSlideAndLaunchNME newEnemy = new CoreSlideAndLaunchNME(new Random().Next(100), 0, enemiesSpeedBase);

                    enemies.Add(newEnemy);


                }

                //Handle Enemies
                for (int a = enemies.Count - 1; a > -1; a--)
                {
                    sndHit = false;
                    CoreSlideAndLaunchNME nme = enemies[a];
                    //Checking for a punch
                    if (FistLaunched() && playerForce > 0 && Math.Abs(player.Y - nme.Y) <= punchDistanceY && Math.Abs(player.X - nme.X) <= punchDistanceX)
                    {
                        //Transfer of speed.

                        nme.Speed = Convert.ToInt32(playerForce * -1);
                        playerForce = 0;
                        sndHit = true;
                    }

                    //Move enemy.
                    nme.Y += nme.Speed;


                    //Remove if out of bounds.
                    if (nme.Y < 0)
                    {
                        enemies.Remove(nme);
                        score++;
                        
                        //Make the game harder?
                        if (getHarder == true && score % getHarderAt == 0)
                        {
                            
                            bool changeMade = false;
                            //Nothing left to change
                            if (playerSpeed >= maxplayerSpeed && enemiesSpeedBase >= maxenemySpeed && enemiesSpawnRate.Y == enemiesSpawnRate.X)
                            {
                                changeMade = true;
                            }

                            //Choose a random thing to get worse.
                            while (changeMade == false)
                            {
                                int diceRoll = new Random().Next(1, 3);

                                if (diceRoll == 1 && playerSpeed < maxplayerSpeed)
                                {
                                    playerSpeed++;
                                    changeMade = true;
                                }
                                else if (diceRoll == 2 && enemiesSpeedBase < maxenemySpeed)
                                {
                                    enemiesSpeedBase++;
                                    changeMade = true;
                                }
                                else
                                {
                                    this.enemiesSpawnRate.X = this.enemiesSpawnRate.X - 5;
                                }


                            }
                        }
                    }

                    //Enemy got to the bottom, game over.
                    if (nme.Y > 100)
                    {
                        gameOn = false;
                    }
                    

                }
                

            }
            else
            {
                sndHit = false;
            }
            sndFist = false;
        }

        public int GetPlayerX ()
        {
            return player.X;
        }

        public int GetPlayerY ()
        {
            return player.Y;
        }

        public Boolean FistLaunched()
        {
            return this.player.Y != 100;
        }

        public void LaunchFist()
        {
            if (FistLaunched() == false)
            {
                playerForce = playerStrength;
                this.sndFist = true;
            }
        }

        //Returns the location of every enemy.
        public List<Point> GetEnemyLoci()
        {
            List<Point> toSend = new List<Point>();

            foreach (CoreSlideAndLaunchNME nme in enemies)
            {
                toSend.Add(new Point(nme.X, nme.Y));
            }

            return toSend;
        }

        public static double LengthBetween2Points(Point pt1, Point pt2)
        {
            //The square of the hypotenuse is equal to the sum of the square of the other 2 sides.
            double x = pt1.X - pt2.X;
            double y = pt1.Y - pt2.Y;
            return Math.Sqrt((x * x) + (y * y));
        }

    }
}
