using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.Exercises.Individual1
{
    static class InOut
    {
        public static void ReadRepetitions(string fin, LettersFrequency letters)
        {
            using (StreamReader reader = new StreamReader(fin))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    letters.line = line;
                    letters.Count();
                }
            }
        }

        public static void PrintRepetitions(string fout, LettersFrequency letters)
        {
            using (var writer = File.CreateText(fout))
            {
                for (char ch = 'a'; ch <= 'z'; ch++)
                {
                    writer.WriteLine("{0, 3:c} {1, 4:d} | {2, 3:c} {3, 4:d}",
                        ch,
                        letters.Get(ch),
                        Char.ToUpper(ch),
                        letters.Get(Char.ToUpper(ch)));
                }
            }
        }

        public static void PrintRepetitions(string fout, List<LetterCount> letters)
        {
            using (var writer = File.CreateText(fout))
            {
                foreach (LetterCount letterCount in letters)
                {
                    writer.WriteLine("{0, 3:c} {1, 4:d}", letterCount.Letter, letterCount.Count);
                }
            }
        }

        public static void PrintLithuanianRepetitons(string fout, LettersFrequency letters)
        {
            char[] lithuanianCharacters = { 'ą', 'č', 'ę', 'ė', 'į', 'š', 'ų', 'ū', 'ž' };

            using (var writer = File.AppendText(fout))
            {
                foreach (char ch in lithuanianCharacters)
                {
                    writer.WriteLine("{0, 3:c} {1, 4:d} | {2, 3:c} {3, 4:d},",
                        ch,
                        letters.Get(ch),
                        Char.ToUpper(ch),
                        letters.Get(Char.ToUpper(ch)));
                    
                }
            }
            
        }
    }
}
