using System.Text;
using U = ArchCorpUtilities.Utilities.UniversalUtilities;

namespace ArchCorpUtilities.Utilities.CodeGen
{
    internal class CodePartEdit(string baseFolder, string targetFile, string entity, string searchString, string workingFolder, string heading, string searchStringPostPart, string sessionID) : CodePart(baseFolder, targetFile, entity, searchString, workingFolder, heading, searchStringPostPart, sessionID)
    {
        internal override string ModifyCode(string CodeToAlter)
        {
            StringBuilder stringBuilder = new();
            var Tabs = "\t\t\t\t\t\t\t";
            U.RemoveFirstLineFeed(CodeToAlter, stringBuilder);
            stringBuilder.AppendLine($"case E.MenuDomain.{Entity}:");
            stringBuilder.AppendLine($"{Tabs}\tL.Log(\"{Entity}-Edit\", SessionID, 1);");
            stringBuilder.AppendLine($"{Tabs}\tA.{Entity}Helper?.Edit(simChoice, simInputValues);");
            stringBuilder.AppendLine($"{Tabs}\tbreak;");
            var CodeAltered = stringBuilder.ToString();
            stringBuilder.Clear();
            return CodeAltered;
        }
    }
}
