using System.Collections.Generic;

namespace GuessWord.Models
{
    public class UserProgress
    {
        private List<char> suggestedLetters = new List<char>();
        private int wrongGuesses;

        public UserProgress()
        {
            wrongGuesses = 0;
        }

        public bool ShouldHelpUser => !ShouldMockUser && wrongGuesses > 5;
        public bool ShouldSuggestLetter => !ShouldMockUser && wrongGuesses > 12;
        public bool ShouldMockUser => wrongGuesses > 15;

        public bool IsSuggestedEarilier(char letter)
        {
            return suggestedLetters.Contains(letter);
        }

        public void AddSuggestedLetter(char letter)
        {
            suggestedLetters.Add(letter);
        }

        public void AddWrongGuess()
        {
            wrongGuesses++;
        }

        public void Reset()
        {
            suggestedLetters.Clear();
            wrongGuesses = 0;
        }
    }
}
