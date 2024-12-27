using ArchCorpUtilities.Utilities.CodeGen;
using System.Text;
using U = ArchCorpUtilities.Utilities.UniversalUtilities;

namespace ArchCorpUtilities.Utilities
{
    internal class CodePartLoadDefaults(string baseFolder, string targetFile, string entity, string searchString, string workingFolder, string heading, string searchStringPostPart, string sessionID) : CodePart(baseFolder, targetFile, entity, searchString, workingFolder, heading, searchStringPostPart, sessionID)
    {
        internal override string ModifyCode(string CodeToAlter)
        {
            StringBuilder stringBuilder = new();
            U.RemoveFirstLineFeed(CodeToAlter, stringBuilder);
            stringBuilder.AppendLine($"if ({Entity}Helper?.Load() == false) {Entity}Helper?.LoadDefaults();");
            var AlteredCode = stringBuilder.ToString();
            stringBuilder.Clear();
            return AlteredCode;
        }
    }
}