using GuessWord.Extensions;
using GuessWord.Models;
using System.Collections.Generic;
using System.Linq;

namespace GuessWord.Services
{
    public class MessageGenerator
    {
        private static readonly Dictionary<GuessType, List<string>> _wordsBank = new()
        {
            { GuessType.Right, new() { "Поздравляем вы угадали!" } },
            { GuessType.Shorter, new() { "Слово не такое короткое!" } },
            { GuessType.Longer, new() { "Слишком длинное слово!" } },
            { GuessType.VerySimilar, new() { "Это было очень близко!" } },
            { GuessType.Wrong, new() { "Нет,это неправильно." } }
        };

        private static readonly Dictionary<GuessType, List<string>> _mockWordsBank = new()
        {
            { GuessType.Right, new() { "Что то до тебя долго доходило!" } },
            { GuessType.Shorter, new() { "Что то у тебя короткий,нужно по длинее!" } },
            { GuessType.Longer, new() { "Как ты не поймешь что слово короче! Совсем дуболом?" } },
            { GuessType.VerySimilar, new() { "Да ладно неужели почти угадал? Я думал я тут засну." } },
            { GuessType.Wrong, new() { "Все совсем там плохо,да?" } }
        };

        private readonly UserProgress _progress;
        private readonly string _rightAnswer;

        public MessageGenerator(UserProgress progress, string rightAnswer)
        {
            _progress = progress;
            _rightAnswer = rightAnswer;
        }
        public string GenerateMessage(GuessType guessType)
        {
            if (guessType is not GuessType.Right && CanSuggestLetter() && _progress.ShouldSuggestLetter)
            {
                return SuggestLetter();
            }
            else if (_progress.ShouldMockUser)
            {
                return _mockWordsBank[guessType].PickRandom();
            }

            return _wordsBank[guessType].PickRandom();
        }

        private string SuggestLetter()
        {
            var letterToSuggest = GetNotSuggestedLetters().PickRandom();

            _progress.AddSuggestedLetter(letterToSuggest);

            return $"Подсказка - в этом слове есть буква '{letterToSuggest}'";
        }

        public bool CanSuggestLetter()
        {
            return GetNotSuggestedLetters().Any();
        }

        private List<char> GetNotSuggestedLetters()
        {
            var lettersOfRightWord = _rightAnswer.Distinct().ToList();

            return lettersOfRightWord
                .Where(l => !_progress.IsSuggestedEarilier(l))
                .ToList();
        }
    }
}
