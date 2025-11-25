using System;
using System.Collections.Generic;
using System.Text;

namespace U4H_17
{
    /// <summary>
    /// Utility class with methods for processing data
    /// </summary>
    static class TaskUtils
    {
        /// <summary>
        /// Finds unique words in a line and adds them to a given HashSet
        /// </summary>
        /// <param name="uniqueWords">HashSet where unique words are stored</param>
        /// <param name="line">Text line to process</param>
        /// <param name="punctuation">Array of punctuation characters</param>
        public static void FindUniqueWords (HashSet<string> uniqueWords, string line, char[] punctuation)
        {
            string[] words = line.Split(punctuation, StringSplitOptions.RemoveEmptyEntries);
            foreach (string word in words)
            {
                uniqueWords.Add(word.ToLower());
            }
        }

        /// <summary>
        /// Counts how many times words occur that are not in the given HashSet
        /// </summary>
        /// <param name="wordsCount">Dictionary storing words and their occurrence</param>
        /// <param name="uniqueWords">HashSet with unique words</param>
        /// <param name="line">Text line to process</param>
        /// <param name="punctuation">Array of punctuation characters</param>
        public static void CountUniqueWords (Dictionary<string, int> wordsCount, HashSet<string> uniqueWords, string line, char[] punctuation)
        {
            string[] words = line.Split(punctuation, StringSplitOptions.RemoveEmptyEntries);
            foreach (string word in words)
            {
                string lowerWord = word.ToLower();

                if (!uniqueWords.Contains(lowerWord))
                {
                    if (!wordsCount.ContainsKey(lowerWord))
                    {
                        wordsCount[lowerWord] = 0;
                    }
                    wordsCount[lowerWord]++;
                }
            }
        }

        /// <summary>
        /// Sorts words by frequency (descending order)
        /// If frequency if equal, sorts alphabetically
        /// </summary>
        /// <param name="wordCount">Dictionary with words and their occurrences</param>
        /// <returns>Sorted list of words with their occurrences</returns>
        public static List<KeyValuePair<string, int>> SortWordsByFrequency(Dictionary<string, int> wordCount)
        {
            List<KeyValuePair<string, int>> sortedList = new List<KeyValuePair<string, int>>(wordCount);

            for (int i = 0; i < sortedList.Count - 1; i++)
            {
                for (int j = i + 1; j < sortedList.Count; j++)
                {
                    bool shouldSwap = false;

                    if (sortedList[j].Value > sortedList[i].Value)
                    {
                        shouldSwap = true;
                    }
                    else if (sortedList[j].Value == sortedList[i].Value)
                    {
                        if (string.Compare(sortedList[j].Key, sortedList[i].Key) < 0)
                        {
                            shouldSwap = true;
                        }
                    }

                    if (shouldSwap)
                    {
                        KeyValuePair<string, int> temp = sortedList[i];
                        sortedList[i] = sortedList[j];
                        sortedList[j] = temp;
                    }
                }
            }
            return sortedList;
        }

        /// <summary>
        /// Extracts sentences from a line
        /// </summary>
        /// <param name="line">Text line to process</param>
        /// <returns>List of sentences</returns>
        public static List<string> ExtractSentences(string line)
        {
            List<string> sentences = new List<string>();
            StringBuilder currentSentence = new StringBuilder();

            foreach(char ch in line)
            {
                currentSentence.Append(ch);
                if (ch == '.' || ch == '!' || ch == '?')
                {
                    string sentence = currentSentence.ToString();
                    if (!string.IsNullOrWhiteSpace(sentence))
                    {
                        sentences.Add(sentence);
                    }
                    currentSentence.Clear();
                }
            }

            if (currentSentence.Length > 0)
            {
                string sentence = currentSentence.ToString().Trim();
                if (!string.IsNullOrWhiteSpace(sentence))
                {
                    sentences.Add(sentence);
                }
            }

            return sentences;
        }

        /// <summary>
        /// Splits text into parts: words and punctuation marks
        /// </summary>
        /// <param name="text">Text to split</param>
        /// <param name="punctuation">Array of punctuation characters</param>
        /// <returns>List of text parts</returns>
        public static List<string> SplitIntoParts(string text, char[] punctuation)
        {
            List<string> parts = new List<string>();
            StringBuilder currentPart = new StringBuilder();
            bool buildingWord = false;

            foreach (char ch in text)
            {
                bool isPunctuation = IsPunctuation(ch, punctuation);

                if (!isPunctuation)
                {
                    if (buildingWord)
                    {
                        currentPart.Append(ch);
                    }
                    else
                    {
                        if (currentPart.Length > 0)
                        {
                            parts.Add(currentPart.ToString());
                            currentPart.Clear();
                        }
                        currentPart.Append(ch);
                        buildingWord = true;
                    }
                }
                else
                {
                    if (!buildingWord)
                    {
                        currentPart.Append(ch);
                    }
                    else
                    {
                        if (currentPart.Length > 0)
                        {
                            parts.Add(currentPart.ToString());
                            currentPart.Clear();
                        }
                        currentPart.Append(ch);
                        buildingWord = false;
                    }
                }
            }

            if (currentPart.Length > 0)
            {
                parts.Add(currentPart.ToString());
            }

            return parts;
        }

        /// <summary>
        /// Checks if a character is a punctuation mark
        /// </summary>
        /// <param name="ch">Character to check</param>
        /// <param name="punctuation">Array of punctuation characters</param>
        /// <returns>True if character is punctuation</returns>
        private static bool IsPunctuation(char ch, char[] punctuation)
        {
            foreach (char p in punctuation)
            {
                if (ch == p)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Checks if a text part is a word
        /// </summary>
        /// <param name="part">Text part to check</param>
        /// <param name="punctuation">Array of punctuation characters</param>
        /// <returns>true if text part is a word</returns>
        public static bool IsWord(string part, char[] punctuation)
        {
            if (string.IsNullOrWhiteSpace(part))
            {
                return false;
            }

            foreach (char ch in part)
            {
                if (char.IsLetter(ch))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
