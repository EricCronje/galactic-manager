// Generated Code - Version: 23.11.25 - 2024/11/20 17:29:24 - {64b62547-252c-4706-a962-7f21c734af25}
using ArchCorpUtilities.Models;
using AL = ArchCorpUtilities.Models.ArchLoader;

namespace ArchCorpUtilities.GeneratedModels.RoutesModel
    {
        public class RoutesMockRepository<T> : IRepository<Routes>, IDisposable
        {
            private readonly List<Routes>? Items;
            string PostFix { get; set; }

            public RoutesMockRepository(string postFix)
            {
                PostFix = postFix;
                Items = [];
            }

            public void Add(Routes? entity)
            {
                if (Items != null && entity != null) { Items?.Add(entity); }
            }

            public IEnumerable<Routes>? All()
            {
                string[] ItemsToAdd = ["Alpha", "Beta", "Charlie", "Delta", "Echo", "Foxtrot", "Golf", "Hotel"];
                foreach (var item in ItemsToAdd)
                {
                    Routes NewEntity = new($"{item}{PostFix}", 0, Guid.NewGuid().ToString());
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

            public IEnumerable<Routes>? GetByGUID(string guid)
            {
                if (Items == null) { return null; }
                return Items.Where(p => p.RoutesGuid != null && p.RoutesGuid == guid);
            }

            public IEnumerable<Routes>? GetById(int id)
            {
                if (Items == null) { return null; }
                return Items.Where(p => p.Id == id);
            }

            public IEnumerable<Routes>? GetByName(string name)
            {
                if (Items == null) { return null; }
                return Items.Where(p => p.Name == name);
            }

            public bool Remove(Routes? entity)
            {
                if (Items != null && entity != null) { return Items.Remove(entity); }
                return false;
            }

            public int Count()
            {
                return Items == null ? 0 : Items.Count;
            }

            public List<Routes>? OrderByIndex()
            {
                return (List<Routes>?)Items?.OrderBy(p => p.Index);
            }
        }
    }
