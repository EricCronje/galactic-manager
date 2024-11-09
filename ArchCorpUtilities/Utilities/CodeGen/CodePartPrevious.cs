using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchCorpUtilities.Utilities.CodeGen
{
    internal class CodePartPrevious : CodePart
    {
        public CodePartPrevious(string baseFolder, string targetFile, string entity, string searchString, string workingFolder, string heading, string searchStringPostPart, string sessionID) : base(baseFolder, targetFile, entity, searchString, workingFolder, heading, searchStringPostPart, sessionID)
        {
        }

        internal override string ModifyCode(string CodeToAlter)
        {
            StringBuilder stringBuilder = new();
            var Tabs = "\t\t\t\t\t\t\t";
            stringBuilder.Append(CodeToAlter.AsSpan(2));
            stringBuilder.AppendLine($"case U.MenuDomain.{Entity}:");
            stringBuilder.AppendLine($"{Tabs}\tL.Log(\"{ Entity}-PreviousPage\", SessionID, 1);");
            stringBuilder.AppendLine($"{Tabs}\tA.{Entity}Helper?.View(U.Navigation.PreviousPage);");
            stringBuilder.AppendLine($"{Tabs}\tbreak;");
            var CodeAltered = stringBuilder.ToString();
            stringBuilder.Clear();
            return CodeAltered;
        }

    }
}
