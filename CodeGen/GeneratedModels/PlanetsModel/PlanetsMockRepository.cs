// Generated Code - Version: 23.11.25 - 2024/11/20 17:29:20 - {64b62547-252c-4706-a962-7f21c734af25}
using ArchCorpUtilities.Models;
using AL = ArchCorpUtilities.Models.ArchLoader;

namespace ArchCorpUtilities.GeneratedModels.PlanetsModel
    {
        public class PlanetsMockRepository<T> : IRepository<Planets>, IDisposable
        {
            private readonly List<Planets>? Items;
            string PostFix { get; set; }

            public PlanetsMockRepository(string postFix)
            {
                PostFix = postFix;
                Items = [];
            }

            public void Add(Planets? entity)
            {
                if (Items != null && entity != null) { Items?.Add(entity); }
            }

            public IEnumerable<Planets>? All()
            {
                string[] ItemsToAdd = ["Alpha", "Beta", "Charlie", "Delta", "Echo", "Foxtrot", "Golf", "Hotel"];
                foreach (var item in ItemsToAdd)
                {
                    Planets NewEntity = new($"{item}{PostFix}", 0, Guid.NewGuid().ToString());
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

            public IEnumerable<Planets>? GetByGUID(string guid)
            {
                if (Items == null) { return null; }
                return Items.Where(p => p.PlanetsGuid != null && p.PlanetsGuid == guid);
            }

            public IEnumerable<Planets>? GetById(int id)
            {
                if (Items == null) { return null; }
                return Items.Where(p => p.Id == id);
            }

            public IEnumerable<Planets>? GetByName(string name)
            {
                if (Items == null) { return null; }
                return Items.Where(p => p.Name == name);
            }

            public bool Remove(Planets? entity)
            {
                if (Items != null && entity != null) { return Items.Remove(entity); }
                return false;
            }

            public int Count()
            {
                return Items == null ? 0 : Items.Count;
            }

            public List<Planets>? OrderByIndex()
            {
                return (List<Planets>?)Items?.OrderBy(p => p.Index);
            }
        }
    }
