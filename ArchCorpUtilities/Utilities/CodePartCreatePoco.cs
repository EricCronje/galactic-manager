using ArchCorpUtilities.Utilities.CodeGen;
using static ArchCorpUtilities.Utilities.CodeGenHelper;

namespace ArchCorpUtilities.Utilities
{
    internal class CodePartCreatePogo(string baseFolder, string targetFile, string entity, string searchString, string workingFolder, string heading, string searchStringPostPart, string sessionID) : CodePart(baseFolder, targetFile, entity, searchString, workingFolder, heading, searchStringPostPart, sessionID)
    {
        public override bool AlterCode()
        {
            return CreateCode(CodeTemplateEnum.POCO, Entity, BaseFolder);
        }
    }
}