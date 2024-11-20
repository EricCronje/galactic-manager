// Generated Code - Version: 23.11.25 - 2024/11/20 17:29:21 - {64b62547-252c-4706-a962-7f21c734af25}
using ArchCorpUtilities.Models;
using AL = ArchCorpUtilities.Models.ArchLoader;

namespace ArchCorpUtilities.GeneratedModels.PersonnelTypeModel
    {
        public class PersonnelTypeMockRepository<T> : IRepository<PersonnelType>, IDisposable
        {
            private readonly List<PersonnelType>? Items;
            string PostFix { get; set; }

            public PersonnelTypeMockRepository(string postFix)
            {
                PostFix = postFix;
                Items = [];
            }

            public void Add(PersonnelType? entity)
            {
                if (Items != null && entity != null) { Items?.Add(entity); }
            }

            public IEnumerable<PersonnelType>? All()
            {
                string[] ItemsToAdd = ["Alpha", "Beta", "Charlie", "Delta", "Echo", "Foxtrot", "Golf", "Hotel"];
                foreach (var item in ItemsToAdd)
                {
                    PersonnelType NewEntity = new($"{item}{PostFix}", 0, Guid.NewGuid().ToString());
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

            public IEnumerable<PersonnelType>? GetByGUID(string guid)
            {
                if (Items == null) { return null; }
                return Items.Where(p => p.PersonnelTypeGuid != null && p.PersonnelTypeGuid == guid);
            }

            public IEnumerable<PersonnelType>? GetById(int id)
            {
                if (Items == null) { return null; }
                return Items.Where(p => p.Id == id);
            }

            public IEnumerable<PersonnelType>? GetByName(string name)
            {
                if (Items == null) { return null; }
                return Items.Where(p => p.Name == name);
            }

            public bool Remove(PersonnelType? entity)
            {
                if (Items != null && entity != null) { return Items.Remove(entity); }
                return false;
            }

            public int Count()
            {
                return Items == null ? 0 : Items.Count;
            }

            public List<PersonnelType>? OrderByIndex()
            {
                return (List<PersonnelType>?)Items?.OrderBy(p => p.Index);
            }
        }
    }
