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
            if (tmp == 0) GameOver();
        }

        private void GameOver()
        {
            timer1.Stop();
            if (MessageBox.Show("Play again?", "Game over", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                timer1.Start();
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
                Invalidate();
                formatTime();
                lbl_Time.ForeColor = Color.Black;
                lbl_Attempts.ForeColor = Color.Black;
                lbl_Attempts.Text = "Attempts: " + (hangmanWord.MaxFailedAttempts - hangmanWord.Misses);

            }
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
                Invalidate();
                if(hangmanWord.Misses == hangmanWord.MaxFailedAttempts)
                {
                    GameOver();
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

        private void DrawBodyPart(BodyParts bp, Graphics g)
        {
            Pen p = new Pen(Color.Blue, 2);
            if (bp == BodyParts.Head)//draw head
            {
                g.DrawEllipse(p, 484, 100, 40, 40);
            }
            else if (bp == BodyParts.Face)//draw face
            {
                SolidBrush s = new SolidBrush(Color.Black);
                g.FillEllipse(s, 494, 110, 5, 5);
                g.FillEllipse(s, 508, 110, 5, 5);
                g.DrawArc(p, 494, 110, 20, 20, 45, 90);

            }           
            else if (bp == BodyParts.Body)// draw body
            {
                g.DrawLine(p, new Point(505, 140), new Point(505, 220));
            }
            else if (bp == BodyParts.Left_Arm)//draw left arm
            {
                g.DrawLine(p, new Point(505, 160), new Point(475, 145));
            }
            else if (bp == BodyParts.Right_Arm)// draw right arm
            {
                g.DrawLine(p, new Point(505, 160), new Point(535, 145));/// the first point is always where you start from on the body
            }
            else if (bp == BodyParts.Left_Leg)// darw left leg
            {
                g.DrawLine(p, new Point(505, 220), new Point(475, 240));
            }
            else if (bp == BodyParts.Right_Leg)// draw right leg
            {
                g.DrawLine(p, new Point(505, 220), new Point(535, 240));
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen p = new Pen(Color.Black, 6);
            g.DrawLine(p, new Point(570, 290), new Point(570, 70));//draws vertical post
            g.DrawLine(p, new Point(573, 70), new Point(502, 70));// draws the horizontal post from the vertical post to where the hook is for the hangman
            g.DrawLine(p, new Point(505, 70), new Point(505, 100));//vertical hook

            if (easyDifficulty.Checked)
            {
                if (hangmanWord.Misses >= 1) DrawBodyPart(BodyParts.Head, g);
                if (hangmanWord.Misses >= 2) DrawBodyPart(BodyParts.Face, g);
                if (hangmanWord.Misses >= 3) DrawBodyPart(BodyParts.Body, g);
                if (hangmanWord.Misses >= 4) DrawBodyPart(BodyParts.Left_Arm, g);
                if (hangmanWord.Misses >= 5) DrawBodyPart(BodyParts.Right_Arm, g);
                if (hangmanWord.Misses >= 6) DrawBodyPart(BodyParts.Left_Leg, g);
                if (hangmanWord.Misses >= 7) DrawBodyPart(BodyParts.Right_Leg, g);
            }
            if (normalDifficulty.Checked)
            {
                if (hangmanWord.Misses >= 1) DrawBodyPart(BodyParts.Head, g);
                if (hangmanWord.Misses >= 2) DrawBodyPart(BodyParts.Face, g);
                if (hangmanWord.Misses >= 3) DrawBodyPart(BodyParts.Body, g);
                if (hangmanWord.Misses >= 4)
                {
                    DrawBodyPart(BodyParts.Left_Arm, g);
                    DrawBodyPart(BodyParts.Right_Arm, g);
                }
                if (hangmanWord.Misses >= 5)
                {
                    DrawBodyPart(BodyParts.Left_Leg, g);
                    DrawBodyPart(BodyParts.Right_Leg, g);
                }            
            }
            if (hardDifficulty.Checked)
            {
                if (hangmanWord.Misses >= 1)
                {
                    DrawBodyPart(BodyParts.Head, g);
                    DrawBodyPart(BodyParts.Face, g);
                    DrawBodyPart(BodyParts.Body, g);
                }
                if (hangmanWord.Misses >= 2)
                {
                    DrawBodyPart(BodyParts.Left_Arm, g);
                    DrawBodyPart(BodyParts.Right_Arm, g);
                }
                if (hangmanWord.Misses >= 3)
                {
                    DrawBodyPart(BodyParts.Left_Leg, g);
                    DrawBodyPart(BodyParts.Right_Leg, g);
                }
            }
            


            

        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            Point point = e.Location ;
        }
    }
}
