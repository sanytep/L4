using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.Exercises.Individual3
{
    static class TaskUtils
    {
        public static int CountPalindromes(string fin)
        {
            int totalCount = 0;

            using (StreamReader reader = new StreamReader(fin, Encoding.UTF8))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    int lineCount = CountPalindromesInLine(line);
                    totalCount += lineCount;
                }
            }

            return totalCount;
        }

        private static int CountPalindromesInLine(string line)
        {
            int count = 0;
            int i = 0;

            while (i < line.Length)
            {
                while (i < line.Length && !char.IsLetter(line[i]))
                {
                    i++;
                }

                if (i < line.Length)
                {
                    int wordStart = i;

                    while (i < line.Length && char.IsLetter(line[i]))
                    {
                        i++;
                    }

                    int wordLength = i - wordStart;

                    if (wordLength > 1)
                    {
                        string word = ExtractWord(line, wordStart, wordLength);

                        if(IsPalindrome(word))
                        {
                            count++;
                        }
                    }
                }
            }

            return count;
        }

        private static string ExtractWord(string text, int start, int length)
        {
            StringBuilder result = new StringBuilder();

            for (int i = start; i < start + length; i++)
            {
                result.Append(char.ToLower(text[i]));
            }

            return result.ToString();
        }

        private static bool IsPalindrome(string word)
        {
            for (int i = 0; i < word.Length / 2; i++)
            {
                if (word[i] != word[word.Length - 1 - i])
                {
                    return false;
                }
            }

            return true;
        }

    }
}
