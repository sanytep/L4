using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.Exercises.Register
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string CFd = "Data.txt";
            const string CFr = "Rezultatai.txt";
            LettersFrequency letters = new LettersFrequency();
            InOut.Repetitions(CFd, letters);
            InOut.PrintRepetitions(CFr, letters);
        }
    }
}
