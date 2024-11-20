// Generated Code - Version: 23.11.25 - 2024/11/20 17:29:20 - {64b62547-252c-4706-a962-7f21c734af25}
using ArchCorpUtilities.Models;
using AL = ArchCorpUtilities.Models.ArchLoader;

namespace ArchCorpUtilities.GeneratedModels.SitesModel
    {
        public class SitesMockRepository<T> : IRepository<Sites>, IDisposable
        {
            private readonly List<Sites>? Items;
            string PostFix { get; set; }

            public SitesMockRepository(string postFix)
            {
                PostFix = postFix;
                Items = [];
            }

            public void Add(Sites? entity)
            {
                if (Items != null && entity != null) { Items?.Add(entity); }
            }

            public IEnumerable<Sites>? All()
            {
                string[] ItemsToAdd = ["Alpha", "Beta", "Charlie", "Delta", "Echo", "Foxtrot", "Golf", "Hotel"];
                foreach (var item in ItemsToAdd)
                {
                    Sites NewEntity = new($"{item}{PostFix}", 0, Guid.NewGuid().ToString());
                    if (Items != null && !Items.Contains(NewEntity))
                        Add(NewEntity);
                }
                return Items;
            }

            public void Dispose()
            {
                Items?.Clear();
                GC.SuppressFinalize(this);
            }

            public IEnumerable<Sites>? GetByGUID(string guid)
            {
                if (Items == null) { return null; }
                return Items.Where(p => p.SitesGuid != null && p.SitesGuid == guid);
            }

            public IEnumerable<Sites>? GetById(int id)
            {
                if (Items == null) { return null; }
                return Items.Where(p => p.Id == id);
            }

            public IEnumerable<Sites>? GetByName(string name)
            {
                if (Items == null) { return null; }
                return Items.Where(p => p.Name == name);
            }

            public bool Remove(Sites? entity)
            {
                if (Items != null && entity != null) { return Items.Remove(entity); }
                return false;
            }

            public int Count()
            {
                return Items == null ? 0 : Items.Count;
            }

            public List<Sites>? OrderByIndex()
            {
                return (List<Sites>?)Items?.OrderBy(p => p.Index);
            }
        }
    }
