using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.Exercises.Individual1
{
    class LetterCount
    {
        public char Letter { get; set; }
        public int Count { get; set; }

        public LetterCount(char letter, int count)
        {
            Letter = letter;
            Count = count;
        }

        public int CompareTo(LetterCount other)
        {
            if (this.Count.CompareTo(other.Count) != 0)
            {
                return other.Count.CompareTo(this.Count);
            }
            return this.Letter.CompareTo(other.Letter);
        }
    }
}
