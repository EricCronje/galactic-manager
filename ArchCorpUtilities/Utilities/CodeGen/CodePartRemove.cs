using System.Text;

namespace ArchCorpUtilities.Utilities.CodeGen
{
    internal class CodePartRemove : CodePart
    {
        public CodePartRemove(string baseFolder, string targetFile, string entity, string searchString, string workingFolder, string heading, string searchStringPostPart, string sessionID) : base(baseFolder, targetFile, entity, searchString, workingFolder, heading, searchStringPostPart, sessionID)
        {
        }

        internal override string ModifyCode(string CodeToAlter)
        {
            StringBuilder stringBuilder = new();
            var Tabs = "\t\t\t\t\t\t\t";
            stringBuilder.Append(CodeToAlter.AsSpan(2));
            stringBuilder.AppendLine($"case U.MenuDomain.{Entity}:");
            stringBuilder.AppendLine($"{Tabs}\tL.Log(\"{Entity}-Remove\", SessionID, 1);");
            stringBuilder.AppendLine($"{Tabs}\tA.{Entity}Helper?.Remove(simChoice, simInputValues);");
            stringBuilder.AppendLine($"{Tabs}\tbreak;");
            var CodeAltered = stringBuilder.ToString();
            stringBuilder.Clear();
            return CodeAltered;
        }
    }
}
