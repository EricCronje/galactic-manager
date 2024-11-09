﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchCorpUtilities.Utilities.CodeGen
{
    public class CodePartGenUsing : CodePart
    {
        public CodePartGenUsing(string baseFolder, string targetFile, string entity, string searchString, string workingFolder, string heading, string searchStringPostPart, string sessionID) : base(baseFolder, targetFile, entity, searchString, workingFolder, heading, searchStringPostPart, sessionID)
        {
        }

        internal override string ModifyCode(string CodeToAlter)
        {
            StringBuilder stringBuilder = new();
            var CodeToAlterLineFeedsAtTheEnd = CodeToAlter.Replace("\r\n", "").Replace(";", ";\r\n");
            stringBuilder.Append(CodeToAlterLineFeedsAtTheEnd);
            stringBuilder.AppendLine($"using {Entity}Helper = ArchCorpUtilities.GeneratedModels.{Entity}Model.{Entity}Helper;");
            stringBuilder.AppendLine($"using ArchCorpUtilities.Models.{Entity}Model.{Entity};");
            var CodeAltered = stringBuilder.ToString();
            stringBuilder.Clear();
            return CodeAltered;
        }
    }
}
