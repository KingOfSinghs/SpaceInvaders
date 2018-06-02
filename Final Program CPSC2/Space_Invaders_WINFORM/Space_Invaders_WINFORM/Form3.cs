using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Space_Invaders_WINFORM
{
    public partial class Form3 : Form
    {
        string message = "Good Job Privahte Knucklehead. You made us proud.";

        string surrenderMessage = "Boi, we nevah surrender, git outta mah sight.";

        int CurrentPos = 0;

        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        #region Garbage

        private void LynchBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
        }
        #endregion

        private void Form3_KeyDown(object sender, KeyEventArgs e) //que?
        {
            if (e.KeyCode == Keys.Left)
            {
                MessageBox.Show("sup");
            }
        }
        private void label2_Click_1(object sender, EventArgs e)
        {
            label2.Text = "Thanks for Playing!";
            label1.Visible = false;

            timer1.Start();
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            if (Form1.surrendered == true) //passing variables from form to form
            {
                message = surrenderMessage;
            }

            //Check if we still need to write out the sentence
            if (CurrentPos < message.Length)
            {
                //Add the next letter to the TextBox's text
                textBox1.Text += message[CurrentPos];

                //Move to the next letter
                CurrentPos += 1;
            }
            else //We have finished writing out the sentence
            {
                //Disable the timer
                timer1.Enabled = false;
                
                this.Close();
            }
            
        }

        private void keyPressed(object sender, KeyPressEventArgs e)
        {
            
        }
    }
}
