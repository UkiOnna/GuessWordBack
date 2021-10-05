
using System.Collections.Generic;
using System.Linq;

namespace GuessWord.Models
{
    public class AnswerHistory
    {
        private readonly List<Message> _messages = new();

        public void AddMessage(string message, Sender sender)
        {
            _messages.Add(new Message() { Text = message, Sender = sender });
        }

        public List<Message> GetAnswers()
        {
            return (_messages as IEnumerable<Message>).Reverse().ToList();
        }

        public void Clear()
        {
            _messages.Clear();
        }
    }
}
