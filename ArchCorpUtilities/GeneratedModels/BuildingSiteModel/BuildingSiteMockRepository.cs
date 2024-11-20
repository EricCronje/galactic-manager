// Generated Code - Version: 23.11.25 - 2024/11/20 17:17:26 - {bc074cbe-aae5-4d1e-aa83-fa69cd2e48f4}
using ArchCorpUtilities.Models;
using AL = ArchCorpUtilities.Models.ArchLoader;

namespace ArchCorpUtilities.GeneratedModels.BuildingSiteModel
    {
        public class BuildingSiteMockRepository<T> : IRepository<BuildingSite>, IDisposable
        {
            private readonly List<BuildingSite>? Items;
            string PostFix { get; set; }

            public BuildingSiteMockRepository(string postFix)
            {
                PostFix = postFix;
                Items = [];
            }

            public void Add(BuildingSite? entity)
            {
                if (Items != null && entity != null) { Items?.Add(entity); }
            }

            public IEnumerable<BuildingSite>? All()
            {
            var SitesItems = AL.SitesHelper?.Items;
            var BuildingsItems = AL.BuildingsHelper?.Items;
            if (SitesItems != null && BuildingsItems != null)
                for (int i = 0; i < SitesItems.Count; i++)
                {
                    SitesModel.Sites? Sites = SitesItems[i];
                    BuildingsModel.Buildings? Buildings = BuildingsItems[i];
                    Items?.Add(new BuildingSite($"{Buildings.Name}-{Sites.Name}", 0, Buildings.BuildingsGuid, Sites.SitesGuid));
                }
                return Items;
            }

            public void Dispose()
            {
                Items?.Clear();
                GC.SuppressFinalize(this);
            }

            public IEnumerable<BuildingSite>? GetByGUID(string guid)
            {
                if (Items == null) { return null; }
                return Items.Where(p => p.BuildingSiteGuid != null && p.BuildingSiteGuid == guid);
            }

            public IEnumerable<BuildingSite>? GetById(int id)
            {
                if (Items == null) { return null; }
                return Items.Where(p => p.Id == id);
            }

            public IEnumerable<BuildingSite>? GetByName(string name)
            {
                if (Items == null) { return null; }
                return Items.Where(p => p.Name == name);
            }

            public bool Remove(BuildingSite? entity)
            {
                if (Items != null && entity != null) { return Items.Remove(entity); }
                return false;
            }

            public int Count()
            {
                return Items == null ? 0 : Items.Count;
            }

            public List<BuildingSite>? OrderByIndex()
            {
                return (List<BuildingSite>?)Items?.OrderBy(p => p.Index);
            }
        }
    }
