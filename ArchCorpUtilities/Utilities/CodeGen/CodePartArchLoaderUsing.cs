using System.Text;

namespace ArchCorpUtilities.Utilities.CodeGen
{
    internal class CodePartArchLoaderUsing : CodePart
    {
        public CodePartArchLoaderUsing(string baseFolder, string targetFile, string entity, string searchString, string workingFolder, string heading, string searchStringPostPart, string sessionID) : base(baseFolder, targetFile, entity, searchString, workingFolder, heading, searchStringPostPart, sessionID)
        {
        }

        internal override string ModifyCode(string CodeToAlter)
        {
            if (CodeToAlter.Contains($"using A = ArchCorpUtilities.Models.ArchLoader;"))
                return "<NoAction>";

            StringBuilder stringBuilder = new();
            var CodeToAlterLineFeedsAtTheEnd = CodeToAlter.Replace("\r\n", "").Replace(";", ";\r\n");
            stringBuilder.Append(CodeToAlterLineFeedsAtTheEnd);
            stringBuilder.AppendLine($"using A = ArchCorpUtilities.Models.ArchLoader;");
            string AlteredCode = stringBuilder.ToString();
            return AlteredCode;
        }

    }
}
