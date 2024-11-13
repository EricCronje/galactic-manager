﻿using ArchCorpUtilities.Utilities.CodeGen;
using System.Text;

namespace ArchCorpUtilities;

public class CodePartLast : CodePart
{
    public CodePartLast(string baseFolder, string targetFile, string entity, string searchString, string workingFolder, string heading, string searchStringPostPart, string sessionID) : base(baseFolder, targetFile, entity, searchString, workingFolder, heading, searchStringPostPart, sessionID)
    {
    }
    internal override string ModifyCode(string CodeToAlter)
    {
        StringBuilder stringBuilder = new();
        var Tabs = "\t\t\t\t\t\t\t\t";
        stringBuilder.Append(CodeToAlter.AsSpan(2));
        stringBuilder.AppendLine($"case U.MenuDomain.{Entity}:");
        stringBuilder.AppendLine($"{Tabs}L.Log(\"{Entity}- LastPage\", SessionID, 1);");
        stringBuilder.AppendLine($"{Tabs}A.{Entity}Helper?.View(U.Navigation.LastPage);");
        stringBuilder.AppendLine($"{Tabs}break;");
        var CodeAltered = stringBuilder.ToString();
        stringBuilder.Clear();
        return CodeAltered;
    }

}