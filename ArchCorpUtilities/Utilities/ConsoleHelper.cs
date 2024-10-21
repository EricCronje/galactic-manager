using System.Text;

namespace ArchCorpUtilities.Utilities
{
    public static class ConsoleHelper
    {

        private readonly static StringBuilder FeedbackSB = new();
        public static bool IsSimulate { get; set; } = false;

        public static string GetFeedback()
        {
            return FeedbackSB.ToString();
        }

        public static void ClearFeedback()
        {
            FeedbackSB.Clear();
        }



        public static void Feedback(string? feedback, bool visible = true)
        {
            feedback ??= "";
            FeedbackSB.AppendLine(feedback);
            if (!IsSimulate && visible)
            {
                Console.WriteLine(feedback);
            }
        }

        public static string GetInput(string? simInput = null)
        {
            if (!IsSimulate)
            {
                string? input;
                input = Console.ReadLine();
                if (input != null)
                {
                    FeedbackSB.AppendLine($"Input received: {input}");
                    return input;
                }
            }
            if (IsSimulate && simInput != null)
            {
                FeedbackSB.AppendLine($"Simulate: {simInput}");
                return simInput;
            }

            return string.Empty;
        }

    }
}
