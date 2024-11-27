﻿using ArchCorpUtilities.Utilities.CodeGen;
using E = EnumLib.EnumLib;
using M = MenuEnumLib.MenuEnumLib;

namespace ArchCorpUtilities.Utilities
{
    internal class CodePartCreateHelper(string baseFolder, string targetFile, string entity, string searchString, string workingFolder, string heading, string searchStringPostPart, string sessionID, string? lHLinkGuid = null, string? rHLinkGuid = null) : CodePart(baseFolder, targetFile, entity, searchString, workingFolder, heading, searchStringPostPart, sessionID, lHLinkGuid, rHLinkGuid)
    {
        public override bool AlterCode()
        {
            if (BaseFolder == null) { return false; }
            return CreateCode(M.CodeTemplateEnum.Helper, Entity, BaseFolder, LhLink, RhLink);
        }
    }
}