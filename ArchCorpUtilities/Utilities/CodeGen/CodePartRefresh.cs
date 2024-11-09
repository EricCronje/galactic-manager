using ArchCorpUtilities.Utilities.CodeGen;
using System.Text;

namespace ArchCorpUtilities;

public class CodePartRefresh : CodePart
{
    public CodePartRefresh(string baseFolder, string targetFile, string entity, string searchString, string workingFolder, string heading, string searchStringPostPart, string sessionID) : base(baseFolder, targetFile, entity, searchString, workingFolder, heading, searchStringPostPart, sessionID)
    {
    }

    internal override string ModifyCode(string CodeToAlter)
    {
        StringBuilder stringBuilder = new();
        var Tabs = "\t\t\t\t\t\t\t";
        stringBuilder.Append(CodeToAlter.AsSpan(2));
        stringBuilder.AppendLine($"case U.MenuDomain.{Entity}: L.Log(\"{Entity}-View\", SessionID, 1);");
        stringBuilder.AppendLine($"{Tabs}\tif (A.{Entity}Helper != null && A.{Entity}Helper.Items != null && A.{Entity}Helper.Items.Count > 0)");
        stringBuilder.AppendLine($"{Tabs}\t{{A.{Entity}Helper.Refresh(A.{Entity}Helper.Items);}}");
        stringBuilder.AppendLine($"{Tabs}break;");
        var CodeAltered = stringBuilder.ToString();
        stringBuilder.Clear();
        return CodeAltered;
    }

}
