using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.Exercises.Individual1
{
    class LettersFrequency
    {
        private const int CMax = 383;
        private int[] Frequency;
        public string line {  get; set; }

        public LettersFrequency()
        {
            line = "";
            Frequency = new int[CMax];
            for (int i = 0; i < CMax; i++)
            {
                Frequency[i] = 0;
            }
        }

        public void Count()
        {
            for (int i = 0; i < line.Length; i++)
            {
                if (Char.IsLetter(line[i]))
                {
                    Frequency[line[i]]++;
                }
            }
        }

        public int Get(char character)
        {
            return Frequency[character];
        }

        public List<LetterCount> GetSorted()
        {
            List<LetterCount> letterCounts = new List<LetterCount>();

            for (int i = 0; i < CMax; i++)
            {
                if (Frequency[i] > 0)
                {
                    char ch = (char)i;
                    if (Char.IsLetter(ch))
                    {
                        LetterCount letterCount = new LetterCount(ch, Frequency[i]);
                        letterCounts.Add(letterCount);
                    }
                }
            }

            return letterCounts;
        }

        public void Sort(List<LetterCount> list)
        {
            for (int i = 0; i < list.Count - 1; i++)
            {
                int max = i;
                for (int j = i + 1; j < list.Count; j++)
                {
                    if (list[j].CompareTo(list[max]) == -1)
                    {
                        max = j;
                    }
                }
                LetterCount temp = list[i];
                list[i] = list[max];
                list[max] = temp;
            }
        }
    }
}
