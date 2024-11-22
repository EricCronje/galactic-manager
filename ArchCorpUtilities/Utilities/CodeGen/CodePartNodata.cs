using System.Text;
using U = ArchCorpUtilities.Utilities.UniversalUtilities;

namespace ArchCorpUtilities.Utilities.CodeGen
{
    internal class CodePartNodal(string baseFolder, string targetFile, string entity, string searchString, string workingFolder, string heading, string tabs, string sessionID) : CodePart(baseFolder, targetFile, entity, searchString, workingFolder, heading, tabs, sessionID)
    {
        internal override string ModifyCode(string CodeToAlter)
        {

            StringBuilder AlteredCode = new();
            U.RemoveFirstLineFeed(CodeToAlter, AlteredCode);
            AlteredCode.AppendLine($"case U.MenuDomain.{Entity}:");
            AlteredCode.AppendLine("                                    if (SessionID != null)");
            AlteredCode.AppendLine("                                        L.Log(\"Menu - NoData\", SessionID, 1);");
            AlteredCode.AppendLine($"                                    if (A.{Entity}Helper != null && !A.{Entity}Helper.IsItemsOnThePage())");
            AlteredCode.AppendLine("                                    {");
            AlteredCode.AppendLine("                                        item.IsHidden = true;");
            AlteredCode.AppendLine("                                        ResetPage = true;");
            AlteredCode.AppendLine("                                    }");
            AlteredCode.AppendLine("                                    break;");
            
            var CodeAltered = AlteredCode.ToString();
            AlteredCode.Clear();
            
            return CodeAltered;
        }
    }
}
