using ArchCorpUtilities.Utilities.CodeGen;

namespace ArchCorpUtilities.Utilities
{
    internal class CodePartCreatePogoLink : CodePart
    {
        public CodePartCreatePogoLink(string baseFolder, string targetFile, string entity, string searchString, string workingFolder, string heading, string searchStringPostPart, string sessionID, string? lHLinkGuid = null, string? rHLinkGuid = null) : base(baseFolder, targetFile, entity, searchString, workingFolder, heading, searchStringPostPart, sessionID, lHLinkGuid, rHLinkGuid)
        {
        }

        public override bool AlterCode()
        {
            return CreateCode(CodeTemplateEnum.POCOLink, Entity, BaseFolder, LHLink, RHLink);
        }
    }
}