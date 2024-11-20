// Generated Code - Version: 23.11.25 - 2024/11/20 17:29:21 - {64b62547-252c-4706-a962-7f21c734af25}
using ArchCorpUtilities.Models;
using AL = ArchCorpUtilities.Models.ArchLoader;

namespace ArchCorpUtilities.GeneratedModels.PlanetTypeModel
    {
        public class PlanetTypeMockRepository<T> : IRepository<PlanetType>, IDisposable
        {
            private readonly List<PlanetType>? Items;
            string PostFix { get; set; }

            public PlanetTypeMockRepository(string postFix)
            {
                PostFix = postFix;
                Items = [];
            }

            public void Add(PlanetType? entity)
            {
                if (Items != null && entity != null) { Items?.Add(entity); }
            }

            public IEnumerable<PlanetType>? All()
            {
                string[] ItemsToAdd = ["Alpha", "Beta", "Charlie", "Delta", "Echo", "Foxtrot", "Golf", "Hotel"];
                foreach (var item in ItemsToAdd)
                {
                    PlanetType NewEntity = new($"{item}{PostFix}", 0, Guid.NewGuid().ToString());
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

            public IEnumerable<PlanetType>? GetByGUID(string guid)
            {
                if (Items == null) { return null; }
                return Items.Where(p => p.PlanetTypeGuid != null && p.PlanetTypeGuid == guid);
            }

            public IEnumerable<PlanetType>? GetById(int id)
            {
                if (Items == null) { return null; }
                return Items.Where(p => p.Id == id);
            }

            public IEnumerable<PlanetType>? GetByName(string name)
            {
                if (Items == null) { return null; }
                return Items.Where(p => p.Name == name);
            }

            public bool Remove(PlanetType? entity)
            {
                if (Items != null && entity != null) { return Items.Remove(entity); }
                return false;
            }

            public int Count()
            {
                return Items == null ? 0 : Items.Count;
            }

            public List<PlanetType>? OrderByIndex()
            {
                return (List<PlanetType>?)Items?.OrderBy(p => p.Index);
            }
        }
    }
