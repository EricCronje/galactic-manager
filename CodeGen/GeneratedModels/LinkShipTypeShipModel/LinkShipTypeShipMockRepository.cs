// Generated Code - Version: 23.11.25 - 2024/11/20 17:29:26 - {64b62547-252c-4706-a962-7f21c734af25}
using ArchCorpUtilities.Models;
using AL = ArchCorpUtilities.Models.ArchLoader;

namespace ArchCorpUtilities.GeneratedModels.LinkShipTypeShipModel
    {
        public class LinkShipTypeShipMockRepository<T> : IRepository<LinkShipTypeShip>, IDisposable
        {
            private readonly List<LinkShipTypeShip>? Items;
            string PostFix { get; set; }

            public LinkShipTypeShipMockRepository(string postFix)
            {
                PostFix = postFix;
                Items = [];
            }

            public void Add(LinkShipTypeShip? entity)
            {
                if (Items != null && entity != null) { Items?.Add(entity); }
            }

            public IEnumerable<LinkShipTypeShip>? All()
            {
            var ShipsItems = AL.ShipsHelper?.Items;
            var ShipTypeItems = AL.ShipTypeHelper?.Items;
            if (ShipsItems != null && ShipTypeItems != null)
                for (int i = 0; i < ShipsItems.Count; i++)
                {
                    ShipsModel.Ships? Ships = ShipsItems[i];
                    ShipTypeModel.ShipType? ShipType = ShipTypeItems[i];
                    Items?.Add(new LinkShipTypeShip($"{ShipType.Name}-{Ships.Name}", 0, ShipType.ShipTypeGuid, Ships.ShipsGuid));
                }
                return Items;
            }

            public void Dispose()
            {
                Items?.Clear();
                GC.SuppressFinalize(this);
            }

            public IEnumerable<LinkShipTypeShip>? GetByGUID(string guid)
            {
                if (Items == null) { return null; }
                return Items.Where(p => p.LinkShipTypeShipGuid != null && p.LinkShipTypeShipGuid == guid);
            }

            public IEnumerable<LinkShipTypeShip>? GetById(int id)
            {
                if (Items == null) { return null; }
                return Items.Where(p => p.Id == id);
            }

            public IEnumerable<LinkShipTypeShip>? GetByName(string name)
            {
                if (Items == null) { return null; }
                return Items.Where(p => p.Name == name);
            }

            public bool Remove(LinkShipTypeShip? entity)
            {
                if (Items != null && entity != null) { return Items.Remove(entity); }
                return false;
            }

            public int Count()
            {
                return Items == null ? 0 : Items.Count;
            }

            public List<LinkShipTypeShip>? OrderByIndex()
            {
                return (List<LinkShipTypeShip>?)Items?.OrderBy(p => p.Index);
            }
        }
    }
