using System.Collections.Generic;

namespace U4H_17
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Paths to input and output files
            const string CFd1 = "Knyga1_1.txt";
            const string CFd2 = "Knyga2_1.txt";
            const string CFr1 = "Rodikliai.txt";
            const string CFr2 = "ManoKnyga.txt";

            // Array of punctuation characters
            char[] punctuation = { ' ', '.', ',', '!', '?', ':', ';', '(', ')', '–', '-', '"', '/' ,'\t', '\n', '\r'};
            
            // Read unique words from first book
            HashSet<string> wordsFirstBook = InOut.ReadFirstData(CFd1, punctuation);
            // Count word occurences in second book (ignoring words from first)
            Dictionary<string, int> wordCountSecondBook = InOut.ReadSecondData(CFd2, punctuation, wordsFirstBook);
            // Sort words by their frequency
            List<KeyValuePair<string, int>> sortedSecondBookWord = TaskUtils.SortWordsByFrequency(wordCountSecondBook);

            // Write TOP 10 most frequent words 
            int maxListCount = 10;
            string countHeader = "Unikalių žodžių skaičius: ";
            string header = $"TOP {maxListCount} daugiausiai pasikartojančių, unikalių žodžių sąrašas:";
            InOut.WriteMostFrequentWords(CFr1, sortedSecondBookWord, header, countHeader, maxListCount);

            // Find longest sentences in both books
            Sentence firstBookLongestSentence = InOut.FindLongestSentence(CFd1, punctuation);
            Sentence secondBookLongestSentence = InOut.FindLongestSentence(CFd2, punctuation);

            // Write longest sentences and their information
            header = "Ilgiausias sakinys pirmoje knygoje: ";
            InOut.WriteLongestSentence(CFr1, header, firstBookLongestSentence);
            header = "Ilgiausias sakinys antroje knygoje: ";
            InOut.WriteLongestSentence(CFr1, header, secondBookLongestSentence);
            
            // Read unique words from second book
            HashSet<string> wordsSecondBook = InOut.ReadFirstData(CFd2, punctuation);
            // Merge and write the new book
            InOut.MergeBooks(CFd1, CFd2, CFr2, wordsFirstBook, wordsSecondBook, punctuation);
        }
    }
}
