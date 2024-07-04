using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;

namespace VP_Ptoject
{
    enum BodyParts // enumeration is used to store all the body parts
    {
        Head,
        Face,
        Right_Arm,
        Left_Arm,
        Body,
        Right_Leg,
        Left_Leg,
    }

    public partial class Form1 : Form
    {
        private HangmanWord hangmanWord;
        Random Random = new Random();
        private string[] art = { "paint", "animation", "brush" };
        private string[] football = { "offside", "goalkeeper", "playmaker" };
        private string[] country = { "America", "Germany", "Bologna" };
        private int time = 120;
        private int time_left = 0;
        private int maxFailedAttempts = 7;
        public Form1()
        {
            InitializeComponent();
            hangmanWord = new HangmanWord(football.ElementAt(Random.Next(0, football.Length)));
            hangmanWord.MaxFailedAttempts = maxFailedAttempts;
            formatTime();
            Invalidate();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            formatTime();
            time_left++;
        }

        private void formatTime()
        {
            int tmp = time - time_left;
            int min = tmp / 60;
            int sec = tmp % 60;
            if (min == 0 && sec <= 20) lbl_Time.ForeColor = Color.Red;
            lbl_Time.Text = String.Format("{0:00}:{1:00}", min, sec);
        }

        private void btn_Start_Click(object sender, EventArgs e)
        {
            btn_Start.Visible = false;
            label1.Visible = false;
            btn_Ok.Enabled = true;
            timer1.Start();
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            if (tb_Attempt.Text.Length == 1)
            {
                hangmanWord.GuessLetter(tb_Attempt.Text.ToCharArray().First());
                if(hangmanWord.Misses == hangmanWord.MaxFailedAttempts)
                {
                    if (MessageBox.Show("Play again?", "Game over", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        //scene = new Scene();
                        //Invalidate();
                        if (footballCategory.Checked)
                        {
                            hangmanWord = new HangmanWord(football.ElementAt(Random.Next(0, football.Length)));
                        }
                        else if (artCategory.Checked)
                        {
                            hangmanWord = new HangmanWord(art.ElementAt(Random.Next(0, art.Length)));
                        }
                        else
                        {
                            hangmanWord = new HangmanWord(country.ElementAt(Random.Next(0, country.Length)));
                        }
                        hangmanWord.MaxFailedAttempts = maxFailedAttempts;
                        time_left = 0;
                        formatTime();
                        lbl_Attempts.Text = "Attempts: " + (hangmanWord.MaxFailedAttempts - hangmanWord.Misses);

                    }
                }
                lbl_Word.Text = hangmanWord.UpdateWordState();
                lbl_AllLeters.Text = hangmanWord.UpdateAlphabet();
                lbl_Attempts.Text = "Attempts: " + (hangmanWord.MaxFailedAttempts-hangmanWord.Misses);
                if ((hangmanWord.MaxFailedAttempts - hangmanWord.Misses) < 3) lbl_Attempts.ForeColor = Color.Red;

            }
            else MessageBox.Show("Put a letter in the text box!");
            tb_Attempt.Text = "";
        }

        private void footballCategory_Click(object sender, EventArgs e)
        {
            footballCategory.Checked = true;          
            hangmanWord = new HangmanWord(football.ElementAt(Random.Next(0, football.Length)));
            artCategory.Checked = countryCategory.Checked = false;
        }

        private void artCategory_Click(object sender, EventArgs e)
        {
            artCategory.Checked = true;
            hangmanWord = new HangmanWord(art.ElementAt(Random.Next(0, art.Length)));
            footballCategory.Checked = countryCategory.Checked = false;
        }

        private void countryCategory_Click(object sender, EventArgs e)
        {
            countryCategory.Checked = true;
            hangmanWord = new HangmanWord(country.ElementAt(Random.Next(0, country.Length)));
            artCategory.Checked = footballCategory.Checked = false;
        }

        private void easyDifficulty_Click(object sender, EventArgs e)
        {
            easyDifficulty.Checked = true;
            hangmanWord.MaxFailedAttempts = hangmanWord.MaxNumOfMiss[2];
            maxFailedAttempts = hangmanWord.MaxNumOfMiss[2];
            lbl_Attempts.Text = "Attempts: " + hangmanWord.MaxFailedAttempts;
            normalDifficulty.Checked = hardDifficulty.Checked = false;
            time = 120;
            formatTime();
        }

        private void normalDifficulty_Click(object sender, EventArgs e)
        {
            easyDifficulty.Checked = hardDifficulty.Checked = false;
            normalDifficulty.Checked = true;
            hangmanWord.MaxFailedAttempts = hangmanWord.MaxNumOfMiss[1];
            maxFailedAttempts = hangmanWord.MaxNumOfMiss[1];
            lbl_Attempts.Text = "Attempts: " + hangmanWord.MaxFailedAttempts;
            time = 60;
            formatTime();


        }

        private void hardDifficulty_Click(object sender, EventArgs e)
        {
            easyDifficulty.Checked = normalDifficulty.Checked = false;
            hardDifficulty.Checked = true;
            hangmanWord.MaxFailedAttempts = hangmanWord.MaxNumOfMiss[0];
            maxFailedAttempts = hangmanWord.MaxNumOfMiss[0];
            lbl_Attempts.Text = "Attempts: " + hangmanWord.MaxFailedAttempts;
            time = 30;
            formatTime();


        }

        private void DrawHangPost()
        {
                     
            Pen p = new Pen(Color.Brown, 10);// use for creating the pn 
            /**
             * The next eight lines are for diffrent body parts that call the function "DrawBodypart()" to draw those bodyparts every time the user
             * enters a wrong entry. 
             */
            DrawBodyPart(BodyParts.Head);
            DrawBodyPart(BodyParts.Face);
            DrawBodyPart(BodyParts.Body);
            DrawBodyPart(BodyParts.Left_Arm);
            DrawBodyPart(BodyParts.Right_Arm);
            DrawBodyPart(BodyParts.Left_Leg);
            DrawBodyPart(BodyParts.Right_Leg);
        }

        private void DrawBodyPart(BodyParts bp)
        {
            throw new NotImplementedException();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen p = new Pen(Color.Black, 6);
            g.DrawLine(p, new Point(570, 290), new Point(570, 70));//draws vertical post
            g.DrawLine(p, new Point(573, 70), new Point(502, 70));// draws the horizontal post from the vertical post to where the hook is for the hangman
            g.DrawLine(p, new Point(505, 70), new Point(505, 100));//vertical hook
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            Point point = e.Location ;
        }
    }
}
