using System.Collections.Generic;

namespace Lab4.Exercises.Individual4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string CFd = "Duomenys.txt";
            const string CFr = "Rezultatai.txt";

            List<string> wordsToRemove = InOut.ReadWordsToRemove();
            InOut.Proccess(CFd, CFr, wordsToRemove);
        }
    }
}
