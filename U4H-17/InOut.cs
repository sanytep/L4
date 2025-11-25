using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace U4H_17
{
    /// <summary>
    /// Class for input and output operations
    /// </summary>
    static class InOut
    {
        private const int WordsWidth = 37;
        private const int SentenceWidth = 45;

        /// <summary>
        /// Reads unique words from a file
        /// </summary>
        /// <param name="fin">Input file name</param>
        /// <param name="punctuation">Array of punctuation characters</param>
        /// <returns>HashSet with unique words</returns>
        public static HashSet<string> ReadFirstData(string fin, char[] punctuation)
        {
            HashSet<string> uniqueWords = new HashSet<string>();

            using (StreamReader reader = new StreamReader(fin, Encoding.UTF8))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    TaskUtils.FindUniqueWords(uniqueWords, line, punctuation);
                }
            }

            return uniqueWords;
        }

        /// <summary>
        /// Counts word occurences, except words, which are in a given HashSet
        /// </summary>
        /// <param name="fin">Input file name</param>
        /// <param name="punctuation">Array of punctuation characters</param>
        /// <param name="uniqueWords">HashSet of words to not count</param>
        /// <returns>Dictionary with words and their occurrence counts</returns>
        public static Dictionary<string, int> ReadSecondData(string fin, char[] punctuation, HashSet<string> uniqueWords)
        {
            Dictionary<string, int> wordCount = new Dictionary<string, int>();

            using (StreamReader reader = new StreamReader(fin, Encoding.UTF8))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    TaskUtils.CountUniqueWords(wordCount, uniqueWords, line, punctuation);
                }
            }

            return wordCount;
        }

        /// <summary>
        /// Writes TOP most frequently occurring words list to a given file
        /// </summary>
        /// <param name="fout">Output file name</param>
        /// <param name="words">Sorted words and their occurrence count list</param>
        /// <param name="header">Header text</param>
        /// <param name="countHeader">Count header text</param>
        /// <param name="maxListCount">Number of TOP words to write</param>
        public static void WriteMostFrequentWords(string fout, List<KeyValuePair<string, int>> words, string header, string countHeader, int maxListCount)
        {
            using (StreamWriter writer = new StreamWriter(fout, false, Encoding.UTF8))
            {
                writer.WriteLine(countHeader + words.Count);
                writer.WriteLine(header);
                writer.WriteLine(new string('-', WordsWidth));
                writer.WriteLine("| {0, -15} | {1, 15} |", "Žodis", "Pasikartojimas");
                writer.WriteLine(new string('-', WordsWidth));

                for (int i = 0; i < words.Count && i < maxListCount; i++)
                {
                    writer.WriteLine("| {0, -15} | {1, 15} |", words[i].Key, words[i].Value);
                }

                writer.WriteLine(new string('-', WordsWidth) + '\n');
            }
        }

        /// <summary>
        /// Finds longest sentence in a file by word count
        /// </summary>
        /// <param name="fin">Input file name</param>
        /// <param name="punctuation">Array of punctuation characters</param>
        /// <returns>Sentence object with the longest sentence</returns>
        public static Sentence FindLongestSentence(string fin, char[] punctuation)
        {
            Sentence longestSentence = null;
            int lineNumber = 0;

            using (StreamReader reader = new StreamReader(fin, Encoding.UTF8))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    lineNumber++;
                    List<string> lineSentences = TaskUtils.ExtractSentences(line);
                    
                    foreach (string lineSentence in lineSentences)
                    {
                        if (string.IsNullOrWhiteSpace(lineSentence))
                        {
                            continue;
                        }

                        string[] words = lineSentence.Split(punctuation, StringSplitOptions.RemoveEmptyEntries);
                        int wordCount = words.Length;

                        if (wordCount == 0)
                        {
                            continue;
                        }

                        int symbolCount = lineSentence.Length;

                        Sentence currentSentence = new Sentence(lineSentence, wordCount, symbolCount, lineNumber);

                        if (longestSentence == null || currentSentence.WordCount > longestSentence.WordCount)
                        {
                            longestSentence = currentSentence;
                        }
                    }
                    
                }
            }
            return longestSentence;
        }

        /// <summary>
        /// Writes the longest sentence to a given file
        /// </summary>
        /// <param name="fout">Output file name</param>
        /// <param name="header">Header text</param>
        /// <param name="sentence">Longest sentence</param>
        public static void WriteLongestSentence(string fout, string header, Sentence sentence)
        {
            using (StreamWriter writer = new StreamWriter(fout, true, Encoding.UTF8))
            {
                writer.WriteLine(header + sentence.Text);
                writer.WriteLine(new string('-', SentenceWidth));
                writer.WriteLine("| {0, 14} | {1, 16} | {2, 5} |",
                    "Ilgis žodžiais", "Ilgis simboliais", "Vieta");
                writer.WriteLine(new string('-', SentenceWidth));
                writer.WriteLine("| {0, 14} | {1, 16} | {2, 5} |",
                    sentence.WordCount, sentence.SymbolCount, sentence.LineNumber);
                writer.WriteLine(new string('-', SentenceWidth) + '\n');
            }
        }

        /// <summary>
        /// Merges two books according to these rules:
        /// Starts from first book, switches to second when it finds a word that exists in the second book and hasn't been written yet.
        /// Process repeats alternating between books.
        /// </summary>
        /// <param name="fin1">First book file name</param>
        /// <param name="fin2">Second book file name</param>
        /// <param name="fout">Output file name</param>
        /// <param name="book1UniqueWords">HashSet of first book's unique words</param>
        /// <param name="book2UniqueWords">HashSet of second book's unique words</param>
        /// <param name="punctuation">Array of punctuation characters</param>
        public static void MergeBooks(string fin1, string fin2, string fout, HashSet<string> book1UniqueWords, HashSet<string> book2UniqueWords, char[] punctuation)
        {
            HashSet<string> writtenWords = new HashSet<string>();

            using (StreamReader reader1 = new StreamReader(fin1, Encoding.UTF8))
            using (StreamReader reader2 = new StreamReader(fin2, Encoding.UTF8))
            using (StreamWriter writer = new StreamWriter(fout, false, Encoding.UTF8))
            {
                Queue<string> queue1 = new Queue<string>();
                Queue<string> queue2 = new Queue<string>();

                bool readFromBook1 = true;
                bool book1Finished = false;
                bool book2Finished = false;

                while (!book1Finished || !book2Finished || queue1.Count > 0 || queue2.Count > 0)
                {
                    if (readFromBook1)
                    {
                        if (queue1.Count == 0 && !book1Finished)
                        {
                            string line = reader1.ReadLine();
                            if (line != null)
                            {
                                List<string> parts = TaskUtils.SplitIntoParts(line + '\n', punctuation);
                                foreach (string part in parts)
                                {
                                    queue1.Enqueue(part);
                                }
                            }
                            else
                            {
                                book1Finished = true;
                            }
                        }

                        if (queue1.Count > 0)
                        {
                            while (queue1.Count > 0)
                            {
                                string currentPart = queue1.Dequeue();
                                writer.Write(currentPart);

                                if (TaskUtils.IsWord(currentPart, punctuation))
                                {
                                    string cleanWord = currentPart.ToLower();
                                    
                                    bool existsInBook2 = book2UniqueWords.Contains(cleanWord);
                                    bool notYetWritten = !writtenWords.Contains(cleanWord);

                                    writtenWords.Add(cleanWord);

                                    if (existsInBook2 && notYetWritten)
                                    {
                                        while (queue1.Count > 0 && !TaskUtils.IsWord(queue1.Peek(), punctuation))
                                        {
                                            writer.Write(queue1.Dequeue());
                                        }

                                        readFromBook1 = false;
                                        break;
                                    }
                                }
                            }
                        }
                        else if (book1Finished)
                        {
                            readFromBook1 = false;
                        }
                    }
                    else
                    {
                        if (queue2.Count == 0 && !book2Finished)
                        {
                            string line = reader2.ReadLine();
                            if (line != null)
                            {
                                List<string> parts = TaskUtils.SplitIntoParts(line + '\n', punctuation);
                                foreach (string part in parts)
                                {
                                    queue2.Enqueue(part);
                                }
                            }
                            else
                            {
                                book2Finished = true;
                            }
                        }

                        if (queue2.Count > 0)
                        {
                            while (queue2.Count > 0)
                            {
                                string currentPart = queue2.Dequeue();
                                writer.Write(currentPart);

                                if (TaskUtils.IsWord(currentPart, punctuation))
                                {
                                    string cleanWord = currentPart.ToLower();

                                    bool existsInBook1 = book1UniqueWords.Contains(cleanWord);
                                    bool notYetWritten = !writtenWords.Contains(cleanWord);

                                    writtenWords.Add(cleanWord);

                                    if (existsInBook1 && notYetWritten)
                                    {
                                        while (queue2.Count > 0 && !TaskUtils.IsWord(queue2.Peek(), punctuation))
                                        {
                                            writer.Write(queue2.Dequeue()); 
                                        }

                                        readFromBook1 = true;
                                        break;
                                    }
                                }
                            }
                        }
                        else if (book2Finished)
                        {
                            readFromBook1 = true;
                        }
                    }
                }
            }
        }
    }
}
