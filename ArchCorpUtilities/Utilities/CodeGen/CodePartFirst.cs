﻿using System.Text;
using U = ArchCorpUtilities.Utilities.UniversalUtilities;

namespace ArchCorpUtilities.Utilities.CodeGen
{
    internal class CodePartFirst(string baseFolder, string targetFile, string entity, string searchString, string workingFolder, string heading, string searchStringPostPart, string sessionID) : CodePart(baseFolder, targetFile, entity, searchString, workingFolder, heading, searchStringPostPart, sessionID)
    {
        internal override string ModifyCode(string CodeToAlter)
        {
            StringBuilder stringBuilder = new();
            var Tabs = "\t\t\t\t\t\t\t\t";
            U.RemoveFirstLineFeed(CodeToAlter, stringBuilder);
            stringBuilder.AppendLine($"case U.MenuDomain.{Entity}:");
            stringBuilder.AppendLine($"{Tabs}L.Log(\"{Entity}- FirstPage\", SessionID, 1);");
            stringBuilder.AppendLine($"{Tabs}A.{Entity}Helper?.View(U.Navigation.FirstPage);");
            stringBuilder.AppendLine($"{Tabs}break;");
            var CodeAltered = stringBuilder.ToString();
            stringBuilder.Clear();
            return CodeAltered;
        }

    }
}
