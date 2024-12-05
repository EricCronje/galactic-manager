using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchCorpUtilities.Models
{
    public class MockRepository<T> : EntityRepository<T> where T : EntityBase, new()
    {
        public MockRepository(string postFix) : base(postFix)
        {
        }

        public MockRepository() { }

    }
}
