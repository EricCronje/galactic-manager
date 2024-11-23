using ArchCorpUtilities.Utilities.CodeGen;
using E = EnumLib.EnumLib;
using M = MenuEnumLib.MenuEnumLib;

namespace ArchCorpUtilities.Utilities
{
    internal class CodePartCreateMockRepository(string baseFolder, string targetFile, string entity, string searchString, string workingFolder, string heading, string searchStringPostPart, string sessionID, string? lHLink = null, string? rHLink = null) : CodePart(baseFolder, targetFile, entity, searchString, workingFolder, heading, searchStringPostPart, sessionID, lHLink, rHLink)
    {
        public override bool AlterCode()
        {
            return CreateCode(M.CodeTemplateEnum.MockRepository, Entity, BaseFolder);
        }
    }
}