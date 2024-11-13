﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchCorpUtilities.Utilities.CodeGen
{
    internal class CodePartAdd : CodePart
    {
        public CodePartAdd(string baseFolder, string targetFile, string entity, string searchString, string workingFolder, string heading, string searchStringPostPart, string sessionID) : base(baseFolder, targetFile, entity, searchString, workingFolder, heading, searchStringPostPart, sessionID)
        {
        }

        internal override string ModifyCode(string CodeToAlter)
        {
            StringBuilder stringBuilder = new();
            stringBuilder.Append(CodeToAlter.AsSpan(2));
            stringBuilder.AppendLine($"case U.MenuDomain.{Entity}: L.Log(\"{Entity}-Add\", SessionID, 1); A.{Entity}Helper?.Add(simChoice, simInputValues); break;");
            var CodeAltered = stringBuilder.ToString();
            stringBuilder.Clear();
            return CodeAltered;
        }
    }
}