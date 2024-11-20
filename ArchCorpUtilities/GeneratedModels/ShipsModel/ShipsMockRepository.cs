// Generated Code - Version: 23.11.25 - 2024/11/20 17:29:22 - {64b62547-252c-4706-a962-7f21c734af25}
using ArchCorpUtilities.Models;
using AL = ArchCorpUtilities.Models.ArchLoader;

namespace ArchCorpUtilities.GeneratedModels.ShipsModel
    {
        public class ShipsMockRepository<T> : IRepository<Ships>, IDisposable
        {
            private readonly List<Ships>? Items;
            string PostFix { get; set; }

            public ShipsMockRepository(string postFix)
            {
                PostFix = postFix;
                Items = [];
            }

            public void Add(Ships? entity)
            {
                if (Items != null && entity != null) { Items?.Add(entity); }
            }

            public IEnumerable<Ships>? All()
            {
                string[] ItemsToAdd = ["Alpha", "Beta", "Charlie", "Delta", "Echo", "Foxtrot", "Golf", "Hotel"];
                foreach (var item in ItemsToAdd)
                {
                    Ships NewEntity = new($"{item}{PostFix}", 0, Guid.NewGuid().ToString());
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

            public IEnumerable<Ships>? GetByGUID(string guid)
            {
                if (Items == null) { return null; }
                return Items.Where(p => p.ShipsGuid != null && p.ShipsGuid == guid);
            }

            public IEnumerable<Ships>? GetById(int id)
            {
                if (Items == null) { return null; }
                return Items.Where(p => p.Id == id);
            }

            public IEnumerable<Ships>? GetByName(string name)
            {
                if (Items == null) { return null; }
                return Items.Where(p => p.Name == name);
            }

            public bool Remove(Ships? entity)
            {
                if (Items != null && entity != null) { return Items.Remove(entity); }
                return false;
            }

            public int Count()
            {
                return Items == null ? 0 : Items.Count;
            }

            public List<Ships>? OrderByIndex()
            {
                return (List<Ships>?)Items?.OrderBy(p => p.Index);
            }
        }
    }
