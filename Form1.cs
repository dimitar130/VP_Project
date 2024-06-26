using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VP_Ptoject
{
    public partial class Form1 : Form
    {
        private HangmanWord hangmanWord;
        Random Random = new Random();
        private string[] art = { "paint", "animation", "brush" };
        private string[] football = { "offside", "goalkeeper", "Martinez" };
        private string[] country = { "USA", "Germany", "Bologna" };
        private int time = 120;
        public Form1()
        {
            InitializeComponent();
            hangmanWord = new HangmanWord(football.ElementAt(Random.Next(0, football.Length)));
            hangmanWord.MaxFailedAttempts = hangmanWord.MaxNumOfMiss[2];
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            formatTime();
            time--;
        }

        private void formatTime()
        {
            int min = time / 60;
            int sec = time % 60;
            if (min == 0 && sec <= 20) lbl_Time.ForeColor = Color.Red;
            lbl_Time.Text = String.Format("{0:00}:{1:00}", min, sec);
        }

        private void btn_Start_Click(object sender, EventArgs e)
        {
            btn_Start.Visible = false;
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
                        time = 120;
                        formatTime();
                    }
                }
                lbl_Word.Text = hangmanWord.UpdateWordState();
                lbl_AllLeters.Text = hangmanWord.UpdateAlphabet();
                lbl_Attempts.Text = "Attempts: " + (hangmanWord.MaxFailedAttempts-hangmanWord.Misses);
                if ((hangmanWord.MaxFailedAttempts - hangmanWord.Misses) < 3) lbl_Attempts.ForeColor = Color.Red;
            }
            else MessageBox.Show("Put a letter in the text box!");
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
            lbl_Attempts.Text = "Attempts: " + hangmanWord.MaxFailedAttempts;
            normalDifficulty.Checked = hardDifficulty.Checked = false;
        }

        private void normalDifficulty_Click(object sender, EventArgs e)
        {
            easyDifficulty.Checked = hardDifficulty.Checked = false;
            normalDifficulty.Checked = true;
            hangmanWord.MaxFailedAttempts = hangmanWord.MaxNumOfMiss[1];
            lbl_Attempts.Text = "Attempts: " + hangmanWord.MaxFailedAttempts;


        }

        private void hardDifficulty_Click(object sender, EventArgs e)
        {
            easyDifficulty.Checked = normalDifficulty.Checked = false;
            hardDifficulty.Checked = true;
            hangmanWord.MaxFailedAttempts = hangmanWord.MaxNumOfMiss[0];
            lbl_Attempts.Text = "Attempts: " + hangmanWord.MaxFailedAttempts;


        }
    }
}
