using System.Text;

namespace Feedback
{
    public class Feedback : IDisposable
    {

        static readonly StringBuilder FeedbackSB = new();

        public static void AddArgsReceivedToFeedback(string[] args)
        {
            foreach (var arg in args)
            {
                AddToFeedback($"Arg received: {arg}");
            }
        }

        public static void AddToFeedback(string message)
        {
            FeedbackSB.Append(message);
        }

        public void Dispose()
        {
            FeedbackSB.Clear();
            GC.SuppressFinalize(this);
        }
    }
}
