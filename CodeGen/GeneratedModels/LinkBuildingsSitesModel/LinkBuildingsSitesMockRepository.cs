// Generated Code - Version: 23.11.25 - 2024/11/20 17:29:24 - {64b62547-252c-4706-a962-7f21c734af25}
using ArchCorpUtilities.Models;
using AL = ArchCorpUtilities.Models.ArchLoader;

namespace ArchCorpUtilities.GeneratedModels.LinkBuildingsSitesModel
    {
        public class LinkBuildingsSitesMockRepository<T> : IRepository<LinkBuildingsSites>, IDisposable
        {
            private readonly List<LinkBuildingsSites>? Items;
            string PostFix { get; set; }

            public LinkBuildingsSitesMockRepository(string postFix)
            {
                PostFix = postFix;
                Items = [];
            }

            public void Add(LinkBuildingsSites? entity)
            {
                if (Items != null && entity != null) { Items?.Add(entity); }
            }

            public IEnumerable<LinkBuildingsSites>? All()
            {
            var SitesItems = AL.SitesHelper?.Items;
            var BuildingsItems = AL.BuildingsHelper?.Items;
            if (SitesItems != null && BuildingsItems != null)
                for (int i = 0; i < SitesItems.Count; i++)
                {
                    SitesModel.Sites? Sites = SitesItems[i];
                    BuildingsModel.Buildings? Buildings = BuildingsItems[i];
                    Items?.Add(new LinkBuildingsSites($"{Buildings.Name}-{Sites.Name}", 0, Buildings.BuildingsGuid, Sites.SitesGuid));
                }
                return Items;
            }

            public void Dispose()
            {
                Items?.Clear();
                GC.SuppressFinalize(this);
            }

            public IEnumerable<LinkBuildingsSites>? GetByGUID(string guid)
            {
                if (Items == null) { return null; }
                return Items.Where(p => p.LinkBuildingsSitesGuid != null && p.LinkBuildingsSitesGuid == guid);
            }

            public IEnumerable<LinkBuildingsSites>? GetById(int id)
            {
                if (Items == null) { return null; }
                return Items.Where(p => p.Id == id);
            }

            public IEnumerable<LinkBuildingsSites>? GetByName(string name)
            {
                if (Items == null) { return null; }
                return Items.Where(p => p.Name == name);
            }

            public bool Remove(LinkBuildingsSites? entity)
            {
                if (Items != null && entity != null) { return Items.Remove(entity); }
                return false;
            }

            public int Count()
            {
                return Items == null ? 0 : Items.Count;
            }

            public List<LinkBuildingsSites>? OrderByIndex()
            {
                return (List<LinkBuildingsSites>?)Items?.OrderBy(p => p.Index);
            }
        }
    }
