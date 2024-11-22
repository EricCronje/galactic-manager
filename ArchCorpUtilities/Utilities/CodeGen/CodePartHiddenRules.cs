using System.Text;
using U = ArchCorpUtilities.Utilities.UniversalUtilities;

namespace ArchCorpUtilities.Utilities.CodeGen;

public class CodePartHiddenRules(string baseFolder, string targetFile, string entity, string searchString, string workingFolder, string heading, string searchStringPostPart, string sessionID) : CodePart(baseFolder, targetFile, entity, searchString, workingFolder, heading, searchStringPostPart, sessionID)
{
    internal override string ModifyCode(string CodeToAlter)
    {
        StringBuilder stringBuilder = new();
        var Tabs = "\t\t\t\t";
        U.RemoveFirstLineFeed(CodeToAlter, stringBuilder);
        stringBuilder.AppendLine($"case U.MenuDomain.{Entity}:");
        stringBuilder.AppendLine($"{Tabs}\tif (A.{Entity}Helper != null && ApplyHiddenRules(A.{Entity}Helper.Page) && doReIndex)");
        stringBuilder.AppendLine($"{Tabs}\t{{A.{Entity}Helper.ReIndexDisplayId();}}");
        stringBuilder.AppendLine($"{Tabs}\tbreak;");
        var CodeAltered = stringBuilder.ToString();
        stringBuilder.Clear();
        return CodeAltered;
    }

}
