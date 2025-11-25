using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.Exercises.Register
{
    class LettersFrequency
    {
        private const int CMax = 256; // Size of the frequency array, which covers all ASCII characters
        private int[] Frequency; // Array to store the count of each character
        public string line { get; set; } // The line of text we want to analyze

        public LettersFrequency() // Constructor
        {
            line = ""; // empty string
            Frequency = new int[CMax]; // new array with 256 elements
            for (int i = 0; i < CMax; i++)
            {
                Frequency[i] = 0; // initializes all frequency counts to 0
            }
        }

        // Method to retrieve the frequency of a specific character
        // @character - the character we want to retrieve
        public int Get(char character)
        {
            // Uses the character's ASCII value as the array index
            return Frequency[character];
        }

        // Method to count the frequency of all letters in the stored line
        public void Count()
        {
            // Loop through each character in the line string
            for (int i = 0; i < line.Length; i++)
            {
                // Check if the current character is a letter
                // First condition: <= line[i] <= 'z' checks for lowercase letters
                // Second condition: 'A' <= line[i] <= 'Z' checks for uppercase letters
                if (('a' <= line[i] && line[i] <= 'z') || 
                    ('A' <= line[i] && line[i] <= 'Z'))
                {
                    Frequency[line[i]]++;
                }
            }
        }
    }
}
