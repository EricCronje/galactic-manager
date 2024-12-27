using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchCorpUtilities.Models
{
    internal class FileReppositoryLink<T, Q, R> : MockRepositoryLink<T, Q, R> where T : EntityLinkBase, new() where Q : EntityBase, new() where R : EntityBase, new()
    {
        public FileReppositoryLink(string postFix, string postFixLhLink, string postFixRhLink) : base(postFix, postFixLhLink, postFixRhLink)
        {
        }

        public override IEnumerable<T>? All()
        {
            return base.All();
        }
    }
}
