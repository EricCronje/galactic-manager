using ArchCorpUtilities.Utilities.CodeGen;
using System.Text;

namespace ArchCorpUtilities.Utilities
{
    internal class CodePartMenuEnum : CodePart
    {
        public CodePartMenuEnum(string baseFolder, string targetFile, string entity, string searchString, string workingFolder, string heading, string searchStringPostPart, string sessionID) : base(baseFolder, targetFile, entity, searchString, workingFolder, heading, searchStringPostPart, sessionID)
        {
        }

        internal override string ModifyCode(string CodeToAlter)
        {
            StringBuilder stringBuilder = new StringBuilder();
            CodeToAlter = CodeToAlter.Replace("\n\t\t\t", "");
            stringBuilder.Append(CodeToAlter.AsSpan(2));
            stringBuilder.AppendLine(",");
            stringBuilder.Append("\t\t\t");
            stringBuilder.Append(Entity);
            stringBuilder.Append("\n\t\t\t");
            var AlteredCode = stringBuilder.ToString();
            stringBuilder.Clear();
            return AlteredCode;
        }
    }
}