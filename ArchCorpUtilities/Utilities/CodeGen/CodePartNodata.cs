using System.Text;
using System.Xml.Linq;

namespace ArchCorpUtilities.Utilities.CodeGen
{
    internal class CodePartNodal : CodePart
    {
        public CodePartNodal(string baseFolder, string targetFile, string entity, string searchString, string workingFolder, string heading, string searchStringPostPart, string sessionID) : base(baseFolder, targetFile, entity, searchString, workingFolder, heading, searchStringPostPart, sessionID)
        {
        }

        internal override string ModifyCode(string CodeToAlter)
        {
            StringBuilder stringBuilder = new();
            stringBuilder.Append(CodeToAlter.AsSpan(2));
            stringBuilder.AppendLine($"case U.MenuDomain.{Entity}:");
            stringBuilder.AppendLine("                                    if (SessionID != null)");
            stringBuilder.AppendLine("                                        L.Log(\"Menu - NoData\", SessionID, 1);");
            stringBuilder.AppendLine($"                                    if (A.{Entity}Helper != null && !A.{Entity}Helper.IsItemsOnThePage())");
            stringBuilder.AppendLine("                                    {");
            stringBuilder.AppendLine("                                        item.IsHidden = true;");
            stringBuilder.AppendLine("                                        ResetPage = true;");
            stringBuilder.AppendLine("                                    }");
            stringBuilder.AppendLine("                                    break;");
            var CodeAltered = stringBuilder.ToString();
            stringBuilder.Clear();
            return CodeAltered;
        }
    }
}
