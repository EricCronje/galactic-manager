// Generated Code - Version: 23.11.25 - 2024/11/20 17:29:23 - {64b62547-252c-4706-a962-7f21c734af25}
using ArchCorpUtilities.Models;
using AL = ArchCorpUtilities.Models.ArchLoader;

namespace ArchCorpUtilities.GeneratedModels.UsersModel
    {
        public class UsersMockRepository<T> : IRepository<Users>, IDisposable
        {
            private readonly List<Users>? Items;
            string PostFix { get; set; }

            public UsersMockRepository(string postFix)
            {
                PostFix = postFix;
                Items = [];
            }

            public void Add(Users? entity)
            {
                if (Items != null && entity != null) { Items?.Add(entity); }
            }

            public IEnumerable<Users>? All()
            {
                string[] ItemsToAdd = ["Alpha", "Beta", "Charlie", "Delta", "Echo", "Foxtrot", "Golf", "Hotel"];
                foreach (var item in ItemsToAdd)
                {
                    Users NewEntity = new($"{item}{PostFix}", 0, Guid.NewGuid().ToString());
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

            public IEnumerable<Users>? GetByGUID(string guid)
            {
                if (Items == null) { return null; }
                return Items.Where(p => p.UsersGuid != null && p.UsersGuid == guid);
            }

            public IEnumerable<Users>? GetById(int id)
            {
                if (Items == null) { return null; }
                return Items.Where(p => p.Id == id);
            }

            public IEnumerable<Users>? GetByName(string name)
            {
                if (Items == null) { return null; }
                return Items.Where(p => p.Name == name);
            }

            public bool Remove(Users? entity)
            {
                if (Items != null && entity != null) { return Items.Remove(entity); }
                return false;
            }

            public int Count()
            {
                return Items == null ? 0 : Items.Count;
            }

            public List<Users>? OrderByIndex()
            {
                return (List<Users>?)Items?.OrderBy(p => p.Index);
            }
        }
    }
