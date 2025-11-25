using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.Exercises.Individual1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string CFd = "Duomenys.txt";
            const string CFr = "Rezultatai.txt";

            LettersFrequency letters = new LettersFrequency();
            InOut.ReadRepetitions(CFd, letters);
            InOut.PrintRepetitions(CFr, letters);
            InOut.PrintLithuanianRepetitons(CFr, letters);

            List<LetterCount> sortedLetters = letters.GetSorted();
            InOut.PrintRepetitions(CFr, sortedLetters);
        }
    }
}
