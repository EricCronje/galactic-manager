using ArchCorpUtilities.Utilities.CodeGen;
using E = EnumLib.EnumLib;
using M = MenuEnumLib.MenuEnumLib;

namespace ArchCorpUtilities.Utilities
{
    internal class CodePartCreatePogo(string? baseFolder, string targetFile, string entity, string searchString, string workingFolder, string heading, string searchStringPostPart, string sessionID) : CodePart(baseFolder, targetFile, entity, searchString, workingFolder, heading, searchStringPostPart, sessionID)
    {
        public override bool AlterCode()
        {
            if (BaseFolder == null) { return false; }
            return CreateCode(M.CodeTemplateEnum.POCO, Entity, BaseFolder, LhLink, RhLink);
        }
    }
}