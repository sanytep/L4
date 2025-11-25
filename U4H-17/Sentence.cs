namespace U4H_17
{
    /// <summary>
    /// Class to store entence information
    /// </summary>
    class Sentence
    {
        public string Text {  get; set; }
        public int WordCount { get; set; }
        public int SymbolCount { get; set; }
        public int LineNumber {  get; set; }

        /// <summary>
        /// Constructor that creates a new sentence object
        /// </summary>
        /// <param name="text">Sentence text</param>
        /// <param name="wordCount">Nummber of words in a sentence</param>
        /// <param name="symbolCount">Number of characters in a sentence</param>
        /// <param name="lineNumber">Number of line, in which the sentence is in</param>
        public Sentence(string text, int wordCount, int symbolCount, int lineNumber)
        {
            Text = text;
            WordCount = wordCount;
            SymbolCount = symbolCount;
            LineNumber = lineNumber;
        }
    }
}
