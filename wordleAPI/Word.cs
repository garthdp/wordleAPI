namespace wordleAPI
{
    public class Word
    {
        public string word {  get; set; }
        public List<char> correctLetters { get; set; }
        public List<int> correctPositions { get; set; }
    }
}
