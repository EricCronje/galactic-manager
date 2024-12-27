using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using U = ArchCorpUtilities.Utilities.UniversalUtilities;

namespace ArchCorpUtilities.Models
{
    public class LinkOneToManyRHHelper(string? sessionID, string postFix = "", string path = "output", DefaultHelper? lhHelper = null, DefaultHelper? rhHelper = null, string lhCaption = "LH", string rhCaption = "RH") : LinkHelper(sessionID, postFix, path, lhHelper, rhHelper, lhCaption, rhCaption)
    {
        public override string AddInternalHelper(string[]? simInput, string InputLinks, string selectionHeading, string heading, out Entity? entity, DefaultHelper? helper)
        {
            entity = U.SelectEntityFromTheList(simInput, ref InputLinks, heading, selectionHeading, helper, true, true);
            return InputLinks;
        }

    }
}
