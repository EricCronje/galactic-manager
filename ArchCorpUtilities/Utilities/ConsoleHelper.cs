using System.Text;

namespace ArchCorpUtilities.Utilities
{
    using L = Logger.Logger;
    //{BC02EA7E-D281-4C03-90A8-7720501E5F80}
    using U = UniversalUtilities;
    //{BC02EA7E-D281-4C03-90A8-7720501E5F80}

    public static class ConsoleHelper
    {

        private readonly static StringBuilder FeedbackSB = new();
        public static bool IsSimulate { get; set; } = false;
        public static string? SessionID { get; set; }
        public static bool LogFeedback { get; set; }

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
            if (feedback != null && feedback.Length > 0)
            {
                FeedbackSB.AppendLine(feedback);
                if (!IsSimulate && visible)
                    Console.WriteLine(feedback);
                if (SessionID != null && LogFeedback)
                    L.Log($"FB: {feedback}", SessionID, 5);
            }
        }

        //{0EC6AB48-9FF0-4465-B084-FAA2589B1207}
        public static string GetInput(string? simInput = null)
        {
            var input = InternalGetInput(simInput);

            if (U.ValidateInput(input) != null)
            {
                FeedbackSB.AppendLine($"Invalid Input received: {input}");
                return string.Empty;
            }

            return input;
        }
        //{0EC6AB48-9FF0-4465-B084-FAA2589B1207}

        //{87EF7E35-47F0-4483-A92B-914466E545D7}
        private static string InternalGetInput(string? simInput)
        {
            if (!IsSimulate)
            {
                string? input;
                input = Console.ReadLine();

                if (input != null)
                {
                    FeedbackSB.AppendLine($"Input received: {input}");
                    FeedbackSB.AppendLine($"Trimmed the Input received: {input.Trim()}");
                    return input.Trim();
                }
            }
            if (IsSimulate && simInput != null)
            {
                FeedbackSB.AppendLine($"Simulate: {simInput}");
                if (SessionID != null) { L.Log($"Simulate Trimmed: {simInput.Trim()}", SessionID, 5); }                
                return simInput.Trim();
            }

            return string.Empty;
        }
        //{87EF7E35-47F0-4483-A92B-914466E545D7}
    }
}
