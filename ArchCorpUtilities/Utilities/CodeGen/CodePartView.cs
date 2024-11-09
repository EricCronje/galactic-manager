using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchCorpUtilities.Utilities.CodeGen
{
    internal class CodePartView : CodePart
    {
        public CodePartView(string baseFolder, string targetFile, string entity, string searchString, string workingFolder, string heading, string searchStringPostPart, string sessionID) : base(baseFolder, targetFile, entity, searchString, workingFolder, heading, searchStringPostPart, sessionID)
        {
        }

        internal override string ModifyCode(string CodeToAlter)
        {
            StringBuilder stringBuilder = new();
            var Tabs = "\t\t\t\t\t\t\t\t";
            stringBuilder.Append(CodeToAlter.AsSpan(2));
            stringBuilder.AppendLine($"case U.MenuDomain.{Entity}: L.Log(\"{Entity}-View\", SessionID, 1);");
            stringBuilder.AppendLine($"{Tabs}if (A.{Entity}Helper != null && A.{Entity}Helper.Items != null) {{ A.{Entity}Helper?.Refresh(A.{Entity}Helper.Items); }}");
            stringBuilder.AppendLine($"{Tabs}break;");
            var CodeAltered = stringBuilder.ToString();
            stringBuilder.Clear();
            return CodeAltered;
        }

    }
}
