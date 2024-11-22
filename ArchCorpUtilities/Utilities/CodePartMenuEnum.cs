using ArchCorpUtilities.Utilities.CodeGen;
using System.Text;
using U = ArchCorpUtilities.Utilities.UniversalUtilities;
namespace ArchCorpUtilities.Utilities
{
    internal class CodePartMenuEnum : CodePart
    {
        private readonly string _entity;
        private readonly string _tabs;

        public CodePartMenuEnum(string baseFolder, string targetFile, string entity, string searchString, string workingFolder, string heading, string tabs, string sessionID) : base(baseFolder, targetFile, entity, searchString, workingFolder, heading, tabs, sessionID)
        {
            _entity = entity;
            _tabs = tabs;
        }

        internal override string ModifyCode(string codeToAlter)
        {
            var AlteredCode = U.ProcessingCodeToAlter(_entity, _tabs, codeToAlter);
            return AlteredCode;
        }

        
    }
}