using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.Exercises.Individual3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string CFd = "Duomenys.txt";
            int palindromeCount = TaskUtils.CountPalindromes(CFd);
            Console.WriteLine("Tekste rasta {0} palindromai(-ų, -as)", palindromeCount);
        }
    }
}
