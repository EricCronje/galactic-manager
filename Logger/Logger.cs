using Microsoft.VisualBasic;
using System.Text;

namespace Logger
{
    public static class Logger
    {
        public static int CurrentLevel { get; set; }

        public static void Log(string? description, string key = "", int level = 0, string path = "log")
        {
            StringBuilder sb = new(7);
            sb.Append(DateAndTime.Now.ToUniversalTime());
            sb.Append('|');
            sb.Append(key);
            sb.Append('|');
            sb.Append(description ?? "");
            sb.Append('|');
            sb.Append(level);
            var content = sb.ToString();
            sb.Clear();
            if (level >= CurrentLevel && !string.IsNullOrWhiteSpace(content))
            {
                try
                {
                    using StreamWriter sw = File.AppendText(path);
                    sw.WriteLine(content);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
