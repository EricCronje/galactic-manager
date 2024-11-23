using System.Text;
using U = ArchCorpUtilities.Utilities.UniversalUtilities;

namespace ArchCorpUtilities.Utilities.CodeGen;

public class CodePartRefresh(string baseFolder, string targetFile, string entity, string searchString, string workingFolder, string heading, string searchStringPostPart, string sessionID) : CodePart(baseFolder, targetFile, entity, searchString, workingFolder, heading, searchStringPostPart, sessionID)
{
    internal override string ModifyCode(string CodeToAlter)
    {
        StringBuilder stringBuilder = new();
        U.RemoveFirstLineFeed(CodeToAlter, stringBuilder);
        stringBuilder.AppendLine($"case E.MenuDomain.{Entity}: L.Log(\"{Entity}-View\", SessionID, 1); A.{Entity}Helper?.Refresh(); break;");
        var CodeAltered = stringBuilder.ToString();
        stringBuilder.Clear();
        return CodeAltered;
    }

}
