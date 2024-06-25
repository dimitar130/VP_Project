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
        private int time = 120;
        public Form1()
        {
            InitializeComponent();
            hangmanWord = new HangmanWord("Dimitar");
            hangmanWord.MaxFailedAttempts = 3;
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
            toolStrip1.Visible = false;
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
            }
            else MessageBox.Show("Put a letter in the text box!");
        }

        private void easy_Difficulty_Click(object sender, EventArgs e)
        {
            time = 120;
            hangmanWord.MaxFailedAttempts = hangmanWord.MaxNumOfMiss[2];
        }

        private void normal_Difficulty_Click(object sender, EventArgs e)
        {

        }
    }
}
