// Generated Code - Version: 23.11.25 - 2024/11/20 17:29:23 - {64b62547-252c-4706-a962-7f21c734af25}
using ArchCorpUtilities.Models;
using AL = ArchCorpUtilities.Models.ArchLoader;

namespace ArchCorpUtilities.GeneratedModels.RoleGroupsModel
    {
        public class RoleGroupsMockRepository<T> : IRepository<RoleGroups>, IDisposable
        {
            private readonly List<RoleGroups>? Items;
            string PostFix { get; set; }

            public RoleGroupsMockRepository(string postFix)
            {
                PostFix = postFix;
                Items = [];
            }

            public void Add(RoleGroups? entity)
            {
                if (Items != null && entity != null) { Items?.Add(entity); }
            }

            public IEnumerable<RoleGroups>? All()
            {
                string[] ItemsToAdd = ["Alpha", "Beta", "Charlie", "Delta", "Echo", "Foxtrot", "Golf", "Hotel"];
                foreach (var item in ItemsToAdd)
                {
                    RoleGroups NewEntity = new($"{item}{PostFix}", 0, Guid.NewGuid().ToString());
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

            public IEnumerable<RoleGroups>? GetByGUID(string guid)
            {
                if (Items == null) { return null; }
                return Items.Where(p => p.RoleGroupsGuid != null && p.RoleGroupsGuid == guid);
            }

            public IEnumerable<RoleGroups>? GetById(int id)
            {
                if (Items == null) { return null; }
                return Items.Where(p => p.Id == id);
            }

            public IEnumerable<RoleGroups>? GetByName(string name)
            {
                if (Items == null) { return null; }
                return Items.Where(p => p.Name == name);
            }

            public bool Remove(RoleGroups? entity)
            {
                if (Items != null && entity != null) { return Items.Remove(entity); }
                return false;
            }

            public int Count()
            {
                return Items == null ? 0 : Items.Count;
            }

            public List<RoleGroups>? OrderByIndex()
            {
                return (List<RoleGroups>?)Items?.OrderBy(p => p.Index);
            }
        }
    }
