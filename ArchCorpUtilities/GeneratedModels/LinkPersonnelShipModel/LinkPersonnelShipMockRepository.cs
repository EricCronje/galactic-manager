// Generated Code - Version: 23.11.25 - 2024/11/20 17:29:25 - {64b62547-252c-4706-a962-7f21c734af25}
using ArchCorpUtilities.Models;
using AL = ArchCorpUtilities.Models.ArchLoader;

namespace ArchCorpUtilities.GeneratedModels.LinkPersonnelShipModel
    {
        public class LinkPersonnelShipMockRepository<T> : IRepository<LinkPersonnelShip>, IDisposable
        {
            private readonly List<LinkPersonnelShip>? Items;
            string PostFix { get; set; }

            public LinkPersonnelShipMockRepository(string postFix)
            {
                PostFix = postFix;
                Items = [];
            }

            public void Add(LinkPersonnelShip? entity)
            {
                if (Items != null && entity != null) { Items?.Add(entity); }
            }

            public IEnumerable<LinkPersonnelShip>? All()
            {
            var ShipsItems = AL.ShipsHelper?.Items;
            var PersonnelItems = AL.PersonnelHelper?.Items;
            if (ShipsItems != null && PersonnelItems != null)
                for (int i = 0; i < ShipsItems.Count; i++)
                {
                    ShipsModel.Ships? Ships = ShipsItems[i];
                    PersonnelModel.Personnel? Personnel = PersonnelItems[i];
                    Items?.Add(new LinkPersonnelShip($"{Personnel.Name}-{Ships.Name}", 0, Personnel.PersonnelGuid, Ships.ShipsGuid));
                }
                return Items;
            }

            public void Dispose()
            {
                Items?.Clear();
                GC.SuppressFinalize(this);
            }

            public IEnumerable<LinkPersonnelShip>? GetByGUID(string guid)
            {
                if (Items == null) { return null; }
                return Items.Where(p => p.LinkPersonnelShipGuid != null && p.LinkPersonnelShipGuid == guid);
            }

            public IEnumerable<LinkPersonnelShip>? GetById(int id)
            {
                if (Items == null) { return null; }
                return Items.Where(p => p.Id == id);
            }

            public IEnumerable<LinkPersonnelShip>? GetByName(string name)
            {
                if (Items == null) { return null; }
                return Items.Where(p => p.Name == name);
            }

            public bool Remove(LinkPersonnelShip? entity)
            {
                if (Items != null && entity != null) { return Items.Remove(entity); }
                return false;
            }

            public int Count()
            {
                return Items == null ? 0 : Items.Count;
            }

            public List<LinkPersonnelShip>? OrderByIndex()
            {
                return (List<LinkPersonnelShip>?)Items?.OrderBy(p => p.Index);
            }
        }
    }
