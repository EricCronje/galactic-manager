// Generated Code - Version: 23.11.25 - 2024/11/20 17:29:23 - {64b62547-252c-4706-a962-7f21c734af25}
using ArchCorpUtilities.Models;
using AL = ArchCorpUtilities.Models.ArchLoader;

namespace ArchCorpUtilities.GeneratedModels.RolesModel
    {
        public class RolesMockRepository<T> : IRepository<Roles>, IDisposable
        {
            private readonly List<Roles>? Items;
            string PostFix { get; set; }

            public RolesMockRepository(string postFix)
            {
                PostFix = postFix;
                Items = [];
            }

            public void Add(Roles? entity)
            {
                if (Items != null && entity != null) { Items?.Add(entity); }
            }

            public IEnumerable<Roles>? All()
            {
                string[] ItemsToAdd = ["Alpha", "Beta", "Charlie", "Delta", "Echo", "Foxtrot", "Golf", "Hotel"];
                foreach (var item in ItemsToAdd)
                {
                    Roles NewEntity = new($"{item}{PostFix}", 0, Guid.NewGuid().ToString());
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

            public IEnumerable<Roles>? GetByGUID(string guid)
            {
                if (Items == null) { return null; }
                return Items.Where(p => p.RolesGuid != null && p.RolesGuid == guid);
            }

            public IEnumerable<Roles>? GetById(int id)
            {
                if (Items == null) { return null; }
                return Items.Where(p => p.Id == id);
            }

            public IEnumerable<Roles>? GetByName(string name)
            {
                if (Items == null) { return null; }
                return Items.Where(p => p.Name == name);
            }

            public bool Remove(Roles? entity)
            {
                if (Items != null && entity != null) { return Items.Remove(entity); }
                return false;
            }

            public int Count()
            {
                return Items == null ? 0 : Items.Count;
            }

            public List<Roles>? OrderByIndex()
            {
                return (List<Roles>?)Items?.OrderBy(p => p.Index);
            }
        }
    }
