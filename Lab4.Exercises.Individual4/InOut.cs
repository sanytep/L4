using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Lab4.Exercises.Individual4
{
    static class InOut
    {
        public static void Proccess(string fin, string fout, List<string> wordsToRemove)
        {
            using (StreamReader reader = new StreamReader(fin, Encoding.UTF8))
            {
                using (StreamWriter writer = new StreamWriter(fout, false, Encoding.UTF8))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string processedLine = TaskUtils.RemoveWords(line, wordsToRemove);

                        if (!string.IsNullOrEmpty(processedLine))
                        {
                            writer.WriteLine(processedLine);
                        }
                    }
                }
            }
        }

        public static List<string> ReadWordsToRemove()
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;

            List<string> words = new List<string>();
            Console.WriteLine("Surašykite žodžius, kuriuos reikia pašalinti iš teksto");

            while (true)
            {
                string word = Console.ReadLine();
                if (string.IsNullOrEmpty(word))
                {
                    break;
                }
                words.Add(word);
            }

            return words;
        }
    }
}
