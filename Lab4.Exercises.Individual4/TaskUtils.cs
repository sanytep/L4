using System.Collections.Generic;
using System.Text;

namespace Lab4.Exercises.Individual4
{
    static class TaskUtils
    {
        public static string RemoveWords(string text, List<string> wordsToRemove)
        {
            StringBuilder result = new StringBuilder();
            int i = 0;

            while (i < text.Length)
            {
                bool wordRemoved = false;

                for (int j = 0; j < wordsToRemove.Count; j++)
                {
                    string wordToRemove = wordsToRemove[j];
                    if (i + wordToRemove.Length <= text.Length)
                    {
                        bool matches = CheckWordMatch(text, i, wordToRemove);

                        if (matches)
                        {
                            bool isCompleteWord = IsCompleteWord(text, i, wordToRemove.Length);

                            if (isCompleteWord)
                            {
                                i += wordToRemove.Length;

                                while (i < text.Length && IsSeparator(text[i]))
                                {
                                    i++;
                                }

                                wordRemoved = true;
                                break;
                            }
                        }
                    }
                }

                if(!wordRemoved)
                {
                    result.Append(text[i]);
                    i++;
                }
            }
            return result.ToString().Trim();
        }

        private static bool CheckWordMatch(string text, int position, string word)
        {
            for (int i = 0; i < word.Length; i++)
            {
                if (char.ToLower(text[position + i]) != char.ToLower(word[i]))
                {
                    return false;
                }
            }
            return true;
        }

        private static bool IsSeparator(char c)
        {
            char[] punctuation = { ' ', '.', ',', '!', '?', ':', ';', '(', ')', '\t' };
            for (int i = 0; i < punctuation.Length; i++)
            {
                if (c == punctuation[i])
                {
                    return true;
                }
            }

            return false;
        }

        private static bool IsCompleteWord(string text, int position, int wordLength)
        {
            if (position > 0)
            {
                char charBefore = text[position - 1];
                if (char.IsLetter(charBefore))
                {
                    return false;
                }
            }

            int positionAfter = position + wordLength;
            if (positionAfter < text.Length)
            {
                char charAfter = text[positionAfter];
                if (char.IsLetter(charAfter))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
