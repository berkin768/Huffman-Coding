namespace Huffman_Coding
{
    class HufmannLetter
    {
         public string LetterName { get; set; }
         public int LetterFrequency { get; set; }
         public double LetterProbability { get; set; }
        public string LetterPath { get; set; }
      
        public HufmannLetter(string letterName, int letterFreqeuncy)
        {
            LetterName = letterName;
            LetterFrequency = letterFreqeuncy;
        }
    }
}
