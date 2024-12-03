using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchCorpUtilities.Models
{
    public class MockRepositoryHierarchy<T,Q> : MockRepository<T> where T : EntityBase, new() where Q : EntityBase, new()
    {
        private readonly List<Q>? EntityLinkList;

        public MockRepositoryHierarchy(string postFix) : base(postFix) { EntityLinkList = new MockRepository<Q>("")?.All()?.ToList(); }
        public override IEnumerable<T>? All()
        {
            if (EntityLinkList != null)
            {
                foreach (var item in EntityLinkList)
                {
                    T Entity = new();
                    {
                        Entity.Id = 0;
                        Entity.Name = item.Name;
                        Entity.Guid_ = item.Guid_;
                    }
                    Items?.Add(Entity);
                }
                return Items;
            }
            return null;
        }
    }
}
