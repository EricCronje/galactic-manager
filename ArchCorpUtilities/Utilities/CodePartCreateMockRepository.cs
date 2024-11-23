using ArchCorpUtilities.Utilities.CodeGen;
using E = EnumLib.EnumLib;

namespace ArchCorpUtilities.Utilities
{
    internal class CodePartCreateMockRepository(string baseFolder, string targetFile, string entity, string searchString, string workingFolder, string heading, string searchStringPostPart, string sessionID, string? lHLink = null, string? rHLink = null) : CodePart(baseFolder, targetFile, entity, searchString, workingFolder, heading, searchStringPostPart, sessionID, lHLink, rHLink)
    {
        public override bool AlterCode()
        {
            return CreateCode(E.CodeTemplateEnum.MockRepository, Entity, BaseFolder);
        }
    }
}