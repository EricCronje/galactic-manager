// Generated Code - Version: 23.11.25 - 2024/11/20 17:29:26 - {64b62547-252c-4706-a962-7f21c734af25}
using ArchCorpUtilities.Models;
using AL = ArchCorpUtilities.Models.ArchLoader;

namespace ArchCorpUtilities.GeneratedModels.LinkPersonnelPersonnelTypeModel
    {
        public class LinkPersonnelPersonnelTypeMockRepository<T> : IRepository<LinkPersonnelPersonnelType>, IDisposable
        {
            private readonly List<LinkPersonnelPersonnelType>? Items;
            string PostFix { get; set; }

            public LinkPersonnelPersonnelTypeMockRepository(string postFix)
            {
                PostFix = postFix;
                Items = [];
            }

            public void Add(LinkPersonnelPersonnelType? entity)
            {
                if (Items != null && entity != null) { Items?.Add(entity); }
            }

            public IEnumerable<LinkPersonnelPersonnelType>? All()
            {
            var PersonnelTypeItems = AL.PersonnelTypeHelper?.Items;
            var PersonnelItems = AL.PersonnelHelper?.Items;
            if (PersonnelTypeItems != null && PersonnelItems != null)
                for (int i = 0; i < PersonnelTypeItems.Count; i++)
                {
                    PersonnelTypeModel.PersonnelType? PersonnelType = PersonnelTypeItems[i];
                    PersonnelModel.Personnel? Personnel = PersonnelItems[i];
                    Items?.Add(new LinkPersonnelPersonnelType($"{Personnel.Name}-{PersonnelType.Name}", 0, Personnel.PersonnelGuid, PersonnelType.PersonnelTypeGuid));
                }
                return Items;
            }

            public void Dispose()
            {
                Items?.Clear();
                GC.SuppressFinalize(this);
            }

            public IEnumerable<LinkPersonnelPersonnelType>? GetByGUID(string guid)
            {
                if (Items == null) { return null; }
                return Items.Where(p => p.LinkPersonnelPersonnelTypeGuid != null && p.LinkPersonnelPersonnelTypeGuid == guid);
            }

            public IEnumerable<LinkPersonnelPersonnelType>? GetById(int id)
            {
                if (Items == null) { return null; }
                return Items.Where(p => p.Id == id);
            }

            public IEnumerable<LinkPersonnelPersonnelType>? GetByName(string name)
            {
                if (Items == null) { return null; }
                return Items.Where(p => p.Name == name);
            }

            public bool Remove(LinkPersonnelPersonnelType? entity)
            {
                if (Items != null && entity != null) { return Items.Remove(entity); }
                return false;
            }

            public int Count()
            {
                return Items == null ? 0 : Items.Count;
            }

            public List<LinkPersonnelPersonnelType>? OrderByIndex()
            {
                return (List<LinkPersonnelPersonnelType>?)Items?.OrderBy(p => p.Index);
            }
        }
    }
