using System.Text;

namespace ArchCorpUtilities.Utilities.CodeGen
{
    public class CodePartHelperInstance : CodePart
    {
        public CodePartHelperInstance(string baseFolder, string targetFile, string entity, string searchString, string workingFolder, string heading, string searchStringPostPart, string sessionID) : base(baseFolder, targetFile, entity, searchString, workingFolder, heading, searchStringPostPart, sessionID)
        {
        }

        internal override string ModifyCode(string CodeToAlter)
        {
            StringBuilder stringBuilder = new();
            var CodeToAlterLineFeedsAtTheEnd = CodeToAlter.Replace("\r\n", "").Replace(";", ";\r\n");
            stringBuilder.Append(CodeToAlterLineFeedsAtTheEnd);
            stringBuilder.AppendLine($"internal static {Entity}Helper? {Entity}Helper = null;");
            var AlteredCode = stringBuilder.ToString();
            stringBuilder.Clear();
            return AlteredCode;
        }
    }
}
