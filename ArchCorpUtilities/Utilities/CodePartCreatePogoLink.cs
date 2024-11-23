using ArchCorpUtilities.Utilities.CodeGen;
using E = EnumLib.EnumLib;
using M = MenuEnumLib.MenuEnumLib;

namespace ArchCorpUtilities.Utilities
{
    internal class CodePartCreatePogoLink : CodePart
    {
        public CodePartCreatePogoLink(string baseFolder, string targetFile, string entity, string searchString, string workingFolder, string heading, string searchStringPostPart, string sessionID, string? lHLinkGuid = null, string? rHLinkGuid = null) : base(baseFolder, targetFile, entity, searchString, workingFolder, heading, searchStringPostPart, sessionID, lHLinkGuid, rHLinkGuid)
        {
        }

        public override bool AlterCode()
        {
            return CreateCode(M.CodeTemplateEnum.POCOLink, Entity, BaseFolder, LHLink, RHLink);
        }
    }
}