using ArchCorpUtilities.Utilities.CodeGen;
using System.Text;

namespace ArchCorpUtilities;

public class CodePartHiddenRules : CodePart
{
    public CodePartHiddenRules(string baseFolder, string targetFile, string entity, string searchString, string workingFolder, string heading, string searchStringPostPart, string sessionID) : base(baseFolder, targetFile, entity, searchString, workingFolder, heading, searchStringPostPart, sessionID)
    {
    }

    internal override string ModifyCode(string CodeToAlter)
    {
        StringBuilder stringBuilder = new();
        var Tabs = "\t\t\t\t";
        stringBuilder.Append(CodeToAlter.AsSpan(2));
        stringBuilder.AppendLine($"case U.MenuDomain.{Entity}:");
        stringBuilder.AppendLine($"{Tabs}\tif (A.{Entity}Helper != null && ApplyHiddenRules(A.{Entity}Helper.Page) && doReIndex)");
        stringBuilder.AppendLine($"{Tabs}\t{{A.{Entity}Helper.ReIndexDisplayId();}}");
        stringBuilder.AppendLine($"{Tabs}\tbreak;");
        var CodeAltered = stringBuilder.ToString();
        stringBuilder.Clear();
        return CodeAltered;
    }

}
