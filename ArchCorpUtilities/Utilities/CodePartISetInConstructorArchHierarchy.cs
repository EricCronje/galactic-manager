﻿using ArchCorpUtilities.Utilities.CodeGen;
using System.Text;

namespace ArchCorpUtilities.Utilities
{
    internal class CodePartISetInConstructorArchHierarchy(string? baseFolder, string targetFile, string entity, string searchString, string workingFolder, string heading, string searchStringPostPart, string sessionID, string? lHLink = null, string? rHLink = null, string? sourcePath = null) : CodePart(baseFolder, targetFile, entity, searchString, workingFolder, heading, searchStringPostPart, sessionID, lHLink, rHLink, sourcePath)
    {
        internal override string ModifyCode(string CodeToAlter)
        {
            StringBuilder stringBuilder = new();
            var CodeToAlterLineFeedsAtTheEnd = CodeToAlter.Replace("\r\n", "").Replace(";", ";\r\n");
            stringBuilder.Append(CodeToAlterLineFeedsAtTheEnd);
            stringBuilder.AppendLine($"{Entity}Helper = new(SessionID, \"{Entity}\", \"{Entity}\", {LhLink}Helper, \"{LhLink}\");");
            var CodeAltered = stringBuilder.ToString();
            stringBuilder.Clear();
            return CodeAltered;
        }

    }
}