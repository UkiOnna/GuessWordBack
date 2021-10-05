using GuessWord.Models;
using System;
using System.Linq;

namespace GuessWord.Services
{
    public class GuessAnalyzer
    {
        private readonly UserProgress _progress;

        public GuessAnalyzer(UserProgress progress)
        {
            _progress = progress;
        }
        public GuessType Analyze(string guess, string rightAnswer)
        {
            var analysis = new AnalysisOfAnswer()
            {
                GuessLength = guess.Length,
                RightAnswerLength = rightAnswer.Length,
                LevenshteinDistance = CalculateLevenshteinDistance(guess, rightAnswer),
                WordsAreSame = guess.Equals(rightAnswer, StringComparison.InvariantCultureIgnoreCase)
            };

            return IdentifyGuessType(analysis);
        }

        private GuessType IdentifyGuessType(AnalysisOfAnswer analysis)
        {
            if (analysis.WordsAreSame)
            {
                return GuessType.Right;
            }

            if (analysis.LevenshteinDistance < 5)
            {
                return GuessType.VerySimilar;
            }

            if (analysis.GuessLength < analysis.RightAnswerLength && _progress.ShouldHelpUser)
            {
                return GuessType.Shorter;
            }

            if (analysis.GuessLength > analysis.RightAnswerLength && _progress.ShouldHelpUser)
            {
                return GuessType.Longer;
            }

            return GuessType.Wrong;
        }

        private static int CalculateLevenshteinDistance(string guess, string rightWord)
        {
            // Взял реализацию из: https://programm.top/c-sharp/algorithm/levenshtein-distance/
            var matrixD = new int[guess.Length + 1, rightWord.Length + 1];

            const int deletionCost = 1;
            const int insertionCost = 1;

            for (var i = 0; i < guess.Length + 1; i++)
            {
                matrixD[i, 0] = i;
            }

            for (var j = 0; j < rightWord.Length + 1; j++)
            {
                matrixD[0, j] = j;
            }

            for (var i = 1; i < guess.Length + 1; i++)
            {
                for (var j = 1; j < rightWord.Length + 1; j++)
                {
                    var substitutionCost = guess[i - 1] == rightWord[j - 1] ? 0 : 1;

                    matrixD[i, j] = new int[] {matrixD[i - 1, j] + deletionCost,          // удаление
                                            matrixD[i, j - 1] + insertionCost,         // вставка
                                            matrixD[i - 1, j - 1] + substitutionCost }.Min(); // замена
                }
            }

            return matrixD[guess.Length, rightWord.Length];
        }
    }
}
