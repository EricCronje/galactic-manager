﻿using ArchCorpUtilities.Utilities.CodeGen;
using System.Text;

namespace ArchCorpUtilities.Utilities
{
    internal class CodePartGenUsingDefault : CodePart
    {
        public CodePartGenUsingDefault(string? baseFolder, string targetFile, string entity, string searchString, string workingFolder, string heading, string searchStringPostPart, string sessionID, string? lHLink = null, string? rHLink = null, string? sourcePath = null) : base(baseFolder, targetFile, entity, searchString, workingFolder, heading, searchStringPostPart, sessionID, lHLink, rHLink, sourcePath)
        {
        }

        internal override string ModifyCode(string CodeToAlter)
        {
            if (!CodeToAlter.Contains(Entity))
            {
                StringBuilder stringBuilder = new();
                var CodeToAlterLineFeedsAtTheEnd = CodeToAlter.Replace("\r\n", "").Replace(";", ";\r\n");
                stringBuilder.Append(CodeToAlterLineFeedsAtTheEnd);
                stringBuilder.AppendLine($"using {Entity}Helper = ArchCorpUtilities.Models.DefaultHelper;");
                var CodeAltered = stringBuilder.ToString();
                stringBuilder.Clear();
                return CodeAltered;
            }
            else
                return "<NoAction>";
        }

    }
}