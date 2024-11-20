using System.Text;

namespace ArchCorpUtilities.Utilities.CodeGen
{
    internal class CodePartView(string baseFolder, string targetFile, string entity, string searchString, string workingFolder, string heading, string searchStringPostPart, string sessionID) : CodePart(baseFolder, targetFile, entity, searchString, workingFolder, heading, searchStringPostPart, sessionID)
    {
        internal override string ModifyCode(string CodeToAlter)
        {
            StringBuilder stringBuilder = new();
            stringBuilder.Append(CodeToAlter.AsSpan(2));
            stringBuilder.AppendLine($"case U.MenuDomain.{Entity}: L.Log(\"{Entity}-View\", SessionID, 1); A.{Entity}Helper?.Refresh(); break;");
            var CodeAltered = stringBuilder.ToString();
            stringBuilder.Clear();
            return CodeAltered;
        }

    }
}
