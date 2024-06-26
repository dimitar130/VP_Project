using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VP_Ptoject
{
    public class HangmanWord
    {
        public string Word { get; set; }
        public HashSet<char> wordLetters { get; set; }
        public HashSet<char> AttemptedLetters { get; set; }
        public int Misses { get; set; } = 0;
        public int[] MaxNumOfMiss { get; set; } = { 3, 5, 7 };

        public int MaxFailedAttempts { get; set; }

        public HangmanWord(string word)
        {
            Word = word;
            wordLetters = new HashSet<char>();
            AttemptedLetters = new HashSet<char>();

            foreach (char letter in Word.ToCharArray())
            {
                wordLetters.Add(Char.ToLower(letter));
            }
        }

        public void GuessLetter(Char letter)
        {
            letter = Char.ToLower(letter);
            if (wordLetters.Contains(letter))
            {
                wordLetters.Remove(letter);
                AttemptedLetters.Add(letter);
            }
            else {
                AttemptedLetters.Add(letter);
                Misses++;
            }
           

        }

        public string UpdateAlphabet()
        {
            StringBuilder sb = new StringBuilder();

            for (char i = 'a'; i <= 'z'; i++)
            {
                if (AttemptedLetters.Contains(i)) sb.Append(i);
                else sb.Append("_");

                sb.Append(" ");
            }

            return sb.ToString();
        }

        public string UpdateWordState()
        {
            StringBuilder sb = new StringBuilder();


            foreach(char letter in Word)
            {
                char tmp_letter = Char.ToLower(letter);
                if (wordLetters.Contains(tmp_letter)) sb.Append("_");
                else sb.Append(tmp_letter);

                sb.Append(" ");
            }

            return sb.ToString();
        }
    }
}
