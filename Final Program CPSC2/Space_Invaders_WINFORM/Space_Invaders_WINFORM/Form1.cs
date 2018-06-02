using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Media;
using System.Timers;
using System.Threading;

namespace Space_Invaders_WINFORM
{
    public partial class Form1 : Form //todo ; Music , game over form
    {
        bool goleft;

        bool goright;

        bool spaceIsPressed;

        public static bool surrendered = false; //to use in other form

        int score = 0;

        int timerCount = 0;

        double weakEnemySpeed = 7;

        double bigBoiSpeed = 15;

        const string fileName = "NewHighScoreFile.txt";

        Random rand = new Random();


        static SoundPlayer sound = new SoundPlayer(@"H:\"); //fix

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void keyisdown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    goleft = true;
                    break;
                case Keys.Right:
                    goright = true;
                    break;
                case Keys.Space:
                    if (!spaceIsPressed)
                    {
                        spaceIsPressed = true; //removes bullet spamming (googled)
                        makeBullet();
                    }
                    break;
            }
        }

        private void keyisup(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    goleft = false;
                    break;
                case Keys.Right:
                    goright = false;
                    break;
                case Keys.Space:       //removes bullet spamming
                    if (spaceIsPressed)
                    {
                        spaceIsPressed = false;
                    }
                    break;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            if (goleft)
            {
                if (player.Left < 40) //barrier
                {
                    player.Left += 0;
                }
                else
                {
                    player.Left -= 6; //makes movement more fluid (i googled this)
                }
            }
            if (goright)
            {
                if (player.Left > 415) //barrier
                {
                    player.Left += 0;
                }
                else
                {
                    player.Left += 6;
                }
            }
            //invader control
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && x.Tag != null && (x.Tag.ToString() == "invaders" || x.Tag.ToString() == "bigInvader")) //if any invader
                {
                    if (((PictureBox)x).Bounds.IntersectsWith(player.Bounds)) //if a picturebox hit the user
                    {
                        gameOver();
                    }

                    if (x.Tag.ToString() == "invaders")
                    {
                        ((PictureBox)x).Left += (int)weakEnemySpeed; // - move left 
                    }
                    else
                    {
                        ((PictureBox)x).Left += (int)bigBoiSpeed;
                    }

                    if (((PictureBox)x).Left > 650) //if reaches the end of screen
                    {
                        ((PictureBox)x).Top += ((PictureBox)x).Height + 10; //reset to next row

                        ((PictureBox)x).Left = -50; //move back to next row
                    }
                }
            }
            //bullet animation
            foreach (Control y in this.Controls)
            {
                if (y is PictureBox && y.Tag != null && y.Tag.ToString() == "bullet")
                {
                    y.Top -= 20; //get 20 pixels closer to top
                    if (((PictureBox)y).Top < this.Height - 490) //button reaches top of form
                    {
                        this.Controls.Remove(y); //removes bullet
                    }
                }
            }
            //bullet & enemy collisions
            foreach (Control i in this.Controls)
            {
                foreach (Control j in this.Controls)
                {
                    if (i is PictureBox && i.Tag != null && (i.Tag.ToString() == "invaders"|| i.Tag.ToString() =="bigInvader"))
                    { //if enemy
                        if (j is PictureBox && j.Tag != null && j.Tag.ToString() == "bullet")
                        { //if bullet
                            if (i.Bounds.IntersectsWith(j.Bounds))
                            {
                                if (i.Tag.ToString() == "invaders")
                                {
                                    score += 100;
                                }
                                else
                                {
                                    score += 500;
                                }
                                this.Controls.Remove(i); //remove invader
                                this.Controls.Remove(j); //remove bullet
                            }
                        }
                    }
                }
            }
            label1.Text = "SCORE " + score;

            //respawn
            if (timerCount > 7)
            {
                if (timerCount % 7  == 0)
                {
                    makeEnemy();
                }
                if (rand.Next (0,20) == rand.Next(0,20)) //randomly generate big boys
                {
                    makeAdvancedEnemy();
                }
            }
            if (timerCount % 100 == 0)
            {
                weakEnemySpeed += 0.5;
                bigBoiSpeed += 0.2;
            }
            timerCount++;
        }

        private void makeBullet()
        {
            PictureBox bullet = new PictureBox(); //bc you dont want to have a bullet on the screen until spacebar

            bullet.Image = Properties.Resources.bullet;

            bullet.Size = new Size(5, 20);

            bullet.Left = player.Left + player.Width / 2; //centering the bullet

            bullet.Top = player.Top - 20;

            bullet.Tag = "bullet"; //for timer

            this.Controls.Add(bullet); //adds control manually because bullet images are not set on the screen

            bullet.BringToFront();
        }

        private void makeEnemy() //same as make bullet pretty much
        {
            PictureBox invader = new PictureBox(); //respawn

            invader.Image = Properties.Resources.inavders; //spelled wrong oops

            invader.Size = new Size(35, 30);

            invader.SizeMode = PictureBoxSizeMode.StretchImage;

            invader.Tag = "invaders"; //for timer

            invader.Top += 10;  //to align

            this.Controls.Add(invader); //adds control manually because enemy images are not set on the screen

            invader.BringToFront();
        }

        private void makeAdvancedEnemy() //same as make bullet pretty much
        {
            PictureBox bigInvader = new PictureBox(); //respawn

            bigInvader.Image = Properties.Resources.bigInvader; //spelled wrong oops

            bigInvader.Size = new Size(40, 35);

            bigInvader.SizeMode = PictureBoxSizeMode.StretchImage;

            bigInvader.Tag = "bigInvader"; //for timer

            bigInvader.Top += 10;  //to align

            this.Controls.Add(bigInvader); //adds control manually because enemy images are not set on the screen

            bigInvader.BringToFront();
        }

        private void gameOver()
        {
            timer1.Stop();

                try
                {
                    if (score > Form2.HighScore) //passing data between forms
                    {
                        MessageBox.Show("NEW HIGH SCORE : " + score);
                        using (StreamWriter writer = new StreamWriter(File.Open(fileName, FileMode.Open)))
                        {
                            writer.Flush();
                            writer.Write(score);
                        }
                    }
                    else
                    {
                        MessageBox.Show("GAME OVER | Score : " + score);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }

            Form3 form3 = new Form3();
            form3.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void label2_Click(object sender, EventArgs e)
        {
            surrendered = true;
            gameOver();
        }
    }
}
