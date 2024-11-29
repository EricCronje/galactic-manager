using ArchCorpUtilities.Utilities.CodeGen;
using System.Text;

namespace ArchCorpUtilities.Utilities
{
    internal class CodePartArchShowMenu : CodePart
    {
        public CodePartArchShowMenu(string? baseFolder, string targetFile, string entity, string searchString, string workingFolder, string heading, string searchStringPostPart, string sessionID, string? lHLink = null, string? rHLink = null) : base(baseFolder, targetFile, entity, searchString, workingFolder, heading, searchStringPostPart, sessionID, lHLink, rHLink)
        {
        }

        
        internal override string ModifyCode(string CodeToAlter)
        {
            StringBuilder stringBuilder = new();
            stringBuilder.AppendLine($"{Tabs}//Generated - {DateTime.Now.ToString()} - {SessionID}");
            stringBuilder.AppendLine($"{Tabs}public static int ShowMenu(int? page, string? menuHeading, int? simChoice = null)");
            stringBuilder.AppendLine($"{Tabs}{{");
            stringBuilder.AppendLine($"{Tabs}\tCH.Feedback(MH.ShowMenu(menuHeading, page));");
            stringBuilder.AppendLine($"{Tabs}\tif (simChoice != null) {{ return simChoice.Value; }}");
            stringBuilder.AppendLine($"{Tabs}\t_ = int.TryParse(CH.GetInput(), out int Choice); return Choice;");
            stringBuilder.AppendLine($"{Tabs}}}");
            var AlteredCode = stringBuilder.ToString();
            stringBuilder.Clear();
            return AlteredCode;
        }
    }
}