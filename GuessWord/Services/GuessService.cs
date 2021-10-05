using GuessWord.Models;
using System;
using System.Collections.Generic;

namespace GuessWord.Services
{
    public class GuessService
    {
        private readonly AnswerHistory _history;
        private readonly UserProgress _progress;
        private readonly GuessAnalyzer _analyzer;
        private string _rightAnswer = "Глина";

        public GuessService(UserProgress progress, GuessAnalyzer analyzer, AnswerHistory history)
        {
            _progress = progress;
            _analyzer = analyzer;
            _history = history;
        }

        public List<Message> GetHistory()
        {
            return _history.GetAnswers();
        }

        public List<Message> GuessWord(string guess)
        {
            if (guess is null) throw new ArgumentNullException(nameof(guess));

            guess = guess.Trim();

            AddUserMessage(guess);

            var guessType = _analyzer.Analyze(guess, _rightAnswer);

            if (guessType is not GuessType.Right)
            {
                _progress.AddWrongGuess();
            }
            else
            {
                _progress.Reset();
                _history.Clear();
            }

            return CreateAnswer(guessType);
        }

        private List<Message> CreateAnswer(GuessType guessType)
        {
            var messageGenerator = new MessageGenerator(_progress, _rightAnswer);

            var message = messageGenerator.GenerateMessage(guessType);

            _history.AddMessage(message, Sender.System);

            return _history.GetAnswers();
        }

        private void AddUserMessage(string guess)
        {
            _history.AddMessage(guess, Sender.User);
        }
    }
}
