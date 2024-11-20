using System.Text;

namespace ArchCorpUtilities.Utilities.CodeGen
{
    internal class CodePartTargetTaskHelperUsing : CodePart
    {
        public CodePartTargetTaskHelperUsing(string baseFolder, string targetFile, string entity, string searchString, string workingFolder, string heading, string searchStringPostPart, string sessionID) : base(baseFolder, targetFile, entity, searchString, workingFolder, heading, searchStringPostPart, sessionID)
        {
        }

        internal override string ModifyCode(string CodeToAlter)
        {
            StringBuilder stringBuilder = new();
            //var CodeToAlterLineFeedsAtTheEnd = CodeToAlter.Replace("\r\n", "").Replace(";", ";\r\n");
            stringBuilder.Append(CodeToAlter.AsSpan(2));
            stringBuilder.AppendLine($"using ArchCorpUtilities.GeneratedModels.{Entity}Model;");
            var AlteredCode = stringBuilder.ToString();
            stringBuilder.Clear();
            return AlteredCode;
        }

    }
}
