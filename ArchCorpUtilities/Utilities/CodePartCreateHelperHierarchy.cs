using ArchCorpUtilities.Utilities.CodeGen;

namespace ArchCorpUtilities.Utilities
{
    internal class CodePartCreateHelperHierarchy(string baseFolder, string targetFile, string entity, string searchString, string workingFolder, string heading, string searchStringPostPart, string sessionID, string? lHLink = null, string? rHLink = null) : CodePart(baseFolder, targetFile, entity, searchString, workingFolder, heading, searchStringPostPart, sessionID, lHLink, rHLink)
    {
        public override bool AlterCode()
        {
            if (BaseFolder == null) { return false; }
            return CreateCode(MenuEnumLib.MenuEnumLib.CodeTemplateEnum.HierarchyHelper, Entity, BaseFolder,LhLink, RhLink);
        }

    }
}