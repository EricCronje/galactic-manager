using ArchCorpUtilities.Utilities.CodeGen;
using System.Text;
using U = ArchCorpUtilities.Utilities.UniversalUtilities;

namespace ArchCorpUtilities.Utilities
{
    internal class CodePartClear : CodePart
    {
        public CodePartClear(string? baseFolder, string targetFile, string entity, string searchString, string workingFolder, string heading, string searchStringPostPart, string sessionID, string? lHLink = null, string? rHLink = null, string? sourcePath = null) : base(baseFolder, targetFile, entity, searchString, workingFolder, heading, searchStringPostPart, sessionID, lHLink, rHLink, sourcePath)
        {
        }

        internal override string ModifyCode(string CodeToAlter)
        {
            StringBuilder stringBuilder = new();
            U.RemoveFirstLineFeed(CodeToAlter, stringBuilder);
            stringBuilder.AppendLine($"case E.MenuDomain.{Entity}: L.Log(\"{Entity}-Clear\", SessionID, 1); A.{Entity}Helper?.Clear(); break;");
            var CodeAltered = stringBuilder.ToString();
            stringBuilder.Clear();
            return CodeAltered;
        }
    }
}