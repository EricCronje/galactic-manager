using ArchCorpUtilities.Utilities.CodeGen;
using System.Text;

namespace ArchCorpUtilities.Utilities
{
    internal class CodePartLoadDefaults : CodePart
    {
        public CodePartLoadDefaults(string baseFolder, string targetFile, string entity, string searchString, string workingFolder, string heading, string searchStringPostPart, string sessionID) : base(baseFolder, targetFile, entity, searchString, workingFolder, heading, searchStringPostPart, sessionID)
        {
        }

        internal override string ModifyCode(string CodeToAlter)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(CodeToAlter.AsSpan(2));
            stringBuilder.AppendLine($"{Entity}Helper?.LoadDefaults();");
            var AlteredCode = stringBuilder.ToString();
            stringBuilder.Clear();
            return AlteredCode;
        }
    }
}