// Generated Code - Version: 23.11.25 - 2024/11/20 17:29:19 - {64b62547-252c-4706-a962-7f21c734af25}
using ArchCorpUtilities.Models;
using AL = ArchCorpUtilities.Models.ArchLoader;

namespace ArchCorpUtilities.GeneratedModels.BuildingsModel
    {
        public class BuildingsMockRepository<T> : IRepository<Buildings>, IDisposable
        {
            private readonly List<Buildings>? Items;
            string PostFix { get; set; }

            public BuildingsMockRepository(string postFix)
            {
                PostFix = postFix;
                Items = [];
            }

            public void Add(Buildings? entity)
            {
                if (Items != null && entity != null) { Items?.Add(entity); }
            }

            public IEnumerable<Buildings>? All()
            {
                string[] ItemsToAdd = ["Alpha", "Beta", "Charlie", "Delta", "Echo", "Foxtrot", "Golf", "Hotel"];
                foreach (var item in ItemsToAdd)
                {
                    Buildings NewEntity = new($"{item}{PostFix}", 0, Guid.NewGuid().ToString());
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

            public IEnumerable<Buildings>? GetByGUID(string guid)
            {
                if (Items == null) { return null; }
                return Items.Where(p => p.BuildingsGuid != null && p.BuildingsGuid == guid);
            }

            public IEnumerable<Buildings>? GetById(int id)
            {
                if (Items == null) { return null; }
                return Items.Where(p => p.Id == id);
            }

            public IEnumerable<Buildings>? GetByName(string name)
            {
                if (Items == null) { return null; }
                return Items.Where(p => p.Name == name);
            }

            public bool Remove(Buildings? entity)
            {
                if (Items != null && entity != null) { return Items.Remove(entity); }
                return false;
            }

            public int Count()
            {
                return Items == null ? 0 : Items.Count;
            }

            public List<Buildings>? OrderByIndex()
            {
                return (List<Buildings>?)Items?.OrderBy(p => p.Index);
            }
        }
    }
