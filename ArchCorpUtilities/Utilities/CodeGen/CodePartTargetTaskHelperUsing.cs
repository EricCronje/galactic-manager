using System.Text;
using U = ArchCorpUtilities.Utilities.UniversalUtilities;

namespace ArchCorpUtilities.Utilities.CodeGen
{
    internal class CodePartTargetTaskHelperUsing(string baseFolder, string targetFile, string entity, string searchString, string workingFolder, string heading, string searchStringPostPart, string sessionID) : CodePart(baseFolder, targetFile, entity, searchString, workingFolder, heading, searchStringPostPart, sessionID)
    {
        private readonly string _searchStringPostPart = searchStringPostPart;

        internal override string ModifyCode(string CodeToAlter)
        {
            var NewCode = $"using ArchCorpUtilities.GeneratedModels.{Entity}Model;";

            if (string.IsNullOrWhiteSpace(NewCode) && NewCode.Length == 0) { return "<NoAction>"; }

            var AlteredCode = U.ProcessingCodeToAlter(NewCode, _searchStringPostPart, CodeToAlter, false);

            return AlteredCode;
        }

    }
}
