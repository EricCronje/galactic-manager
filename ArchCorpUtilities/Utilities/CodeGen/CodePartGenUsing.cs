using System.Text;

namespace ArchCorpUtilities.Utilities.CodeGen
{
    public class CodePartGenUsing(string baseFolder, string targetFile, string entity, string searchString, string workingFolder, string heading, string searchStringPostPart, string sessionID) : CodePart(baseFolder, targetFile, entity, searchString, workingFolder, heading, searchStringPostPart, sessionID)
    {
        internal override string ModifyCode(string CodeToAlter)
        {
            if (!CodeToAlter.Contains(Entity))
            {
                StringBuilder stringBuilder = new();
                var CodeToAlterLineFeedsAtTheEnd = CodeToAlter.Replace("\r\n", "").Replace(";", ";\r\n");
                stringBuilder.Append(CodeToAlterLineFeedsAtTheEnd);
                stringBuilder.AppendLine($"using {Entity}Helper = ArchCorpUtilities.GeneratedModels.{Entity}Model.{Entity}Helper;");
                var CodeAltered = stringBuilder.ToString();
                stringBuilder.Clear();
                return CodeAltered;
            }
            else
                return "<NoAction>";
        }
    }
}
