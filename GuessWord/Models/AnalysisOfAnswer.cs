namespace GuessWord.Models
{
    public class AnalysisOfAnswer
    {
        public int GuessLength { get; set; }
        public int RightAnswerLength { get; set; }
        public int LevenshteinDistance { get; set; }
        public bool WordsAreSame { get; set; }
    }
}
