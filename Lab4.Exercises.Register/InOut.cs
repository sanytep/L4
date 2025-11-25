using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Lab4.Exercises.Register
{
    static class InOut
    {
        // Method to write letter frequency results to a file
        // @fout - file path where output will be written
        public static void PrintRepetitions(string fout, LettersFrequency letters)
        {
            // Create a new text file (or overwrite if it exists) and get a writer
            // 'using' ensures the file is properly closed even if an error occurs
            using (var writer = File.CreateText(fout))
            {
                // Loop through all lowercase letters from 'a' to 'z'
                for (char ch = 'a'; ch <= 'z'; ch++)
                {
                    // c - character, d - decimal
                    writer.WriteLine("{0,3:c} {1, 4:d} | {2, 3:c} {3, 4:d}", 
                        ch,                             // lowercase letter
                        letters.Get(ch),                // count of lowercase letter
                        Char.ToUpper(ch),               // uppercase SAME letter
                        letters.Get(Char.ToUpper(ch))); // count of uppercase letter
                }
            }
        }

        // Method to read text from a file and count letter frequencies
        // @fin - input file path to read from
        public static void Repetitions(string fin, LettersFrequency letters)
        {
            // Open the file for reading
            // 'using' ensures the file is properly closed even if an error occurs
            using (StreamReader reader = new StreamReader(fin))
            {
                string line; // Variable to store each line read from the file

                // Read the file line by line until we reach the end
                // ReadLine() returns null when there are no more lines
                while ((line = reader.ReadLine()) != null)
                {
                    letters.line = line; // Store the current line in the letters object
                    letters.Count(); // Count the letter frequencies in this line
                }
            }
        }
    }
}
