using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Windows.Forms;

namespace Space_Invaders_WINFORM
{
    public partial class Form2 : Form
    {
        const string fileName = "NewHighScoreFile.txt";

        public static int HighScore;

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            if (File.Exists(fileName))
            {
                try
                {
                    HighScore = Convert.ToInt32(File.ReadAllText(fileName));
                    label1.Text += HighScore;
                }
                catch (Exception)
                {
                    label1.Visible = false; //hide failure
                }
                
            }
            else
            {
                label1.Visible = false; //hide failure
            }
        }
        
        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                pictureBox1_Click(sender, e); //if they press enter, go click the box
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Form1 gameForm = new Form1();
            Form2 form2 = new Form2();
            this.Hide();
            gameForm.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
        }
    }
}
