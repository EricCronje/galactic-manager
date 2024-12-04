namespace ArchCorpUtilities.Models
{
    public class MockRepositoryLink<T,Q,R> : MockRepository<T> where T : EntityLinkBase, new() where Q : EntityBase, new() where R: EntityBase, new()  
    {
        private List<Q>? LhLinkList;
        private List<R>? RhLinkList;
        public MockRepositoryLink(string postFix) : base(postFix)
        {
            LhLinkList = new MockRepository<Q>(postFix).All()?.ToList();
            RhLinkList = new MockRepository<R>(postFix).All()?.ToList();
        }

        public override IEnumerable<T>? All()
        {
            if (LhLinkList != null && RhLinkList != null)
            for (int i = 0; i < LhLinkList.Count; i++)
            {
                Q? LHLink = LhLinkList[i];
                R? RHLink = RhLinkList[i];
                    T Entity = new T()
                    {
                        DisplayId = 0,
                        Id = 0,
                        Name = $"{LHLink.Name}-{RHLink.Name}",
                        Index = 0,
                        Guid_ = "",
                        LhGuid = LHLink.Guid_,
                        RhGuid = RHLink.Guid_,
                        LhName = LHLink.Name,
                        RhName = RHLink.Name
                    };
            }
            return Items;
        }
    }
}
