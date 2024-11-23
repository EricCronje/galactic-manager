using ArchCorpUtilities.Utilities.CodeGen;
using E = EnumLib.EnumLib;

namespace ArchCorpUtilities.Utilities
{
    internal class CodePartCreateHelperLink(string baseFolder, string targetFile, string entity, string searchString, string workingFolder, string heading, string searchStringPostPart, string sessionID, string? lHLinkGuid = null, string? rHLinkGuid = null) : CodePart(baseFolder, targetFile, entity, searchString, workingFolder, heading, searchStringPostPart, sessionID, lHLinkGuid, rHLinkGuid)
    {
        public override bool AlterCode()
        {
            return CreateCode(E.CodeTemplateEnum.HelperLink, Entity, BaseFolder, LHLink, RHLink);
        }
    }
}