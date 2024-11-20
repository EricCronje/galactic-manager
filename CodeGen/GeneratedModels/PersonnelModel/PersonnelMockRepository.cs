// Generated Code - Version: 23.11.25 - 2024/11/20 17:29:21 - {64b62547-252c-4706-a962-7f21c734af25}
using ArchCorpUtilities.Models;
using AL = ArchCorpUtilities.Models.ArchLoader;

namespace ArchCorpUtilities.GeneratedModels.PersonnelModel
    {
        public class PersonnelMockRepository<T> : IRepository<Personnel>, IDisposable
        {
            private readonly List<Personnel>? Items;
            string PostFix { get; set; }

            public PersonnelMockRepository(string postFix)
            {
                PostFix = postFix;
                Items = [];
            }

            public void Add(Personnel? entity)
            {
                if (Items != null && entity != null) { Items?.Add(entity); }
            }

            public IEnumerable<Personnel>? All()
            {
                string[] ItemsToAdd = ["Alpha", "Beta", "Charlie", "Delta", "Echo", "Foxtrot", "Golf", "Hotel"];
                foreach (var item in ItemsToAdd)
                {
                    Personnel NewEntity = new($"{item}{PostFix}", 0, Guid.NewGuid().ToString());
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

            public IEnumerable<Personnel>? GetByGUID(string guid)
            {
                if (Items == null) { return null; }
                return Items.Where(p => p.PersonnelGuid != null && p.PersonnelGuid == guid);
            }

            public IEnumerable<Personnel>? GetById(int id)
            {
                if (Items == null) { return null; }
                return Items.Where(p => p.Id == id);
            }

            public IEnumerable<Personnel>? GetByName(string name)
            {
                if (Items == null) { return null; }
                return Items.Where(p => p.Name == name);
            }

            public bool Remove(Personnel? entity)
            {
                if (Items != null && entity != null) { return Items.Remove(entity); }
                return false;
            }

            public int Count()
            {
                return Items == null ? 0 : Items.Count;
            }

            public List<Personnel>? OrderByIndex()
            {
                return (List<Personnel>?)Items?.OrderBy(p => p.Index);
            }
        }
    }
