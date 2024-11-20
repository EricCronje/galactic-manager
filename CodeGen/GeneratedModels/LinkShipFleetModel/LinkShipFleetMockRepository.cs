// Generated Code - Version: 23.11.25 - 2024/11/20 17:29:26 - {64b62547-252c-4706-a962-7f21c734af25}
using ArchCorpUtilities.Models;
using AL = ArchCorpUtilities.Models.ArchLoader;

namespace ArchCorpUtilities.GeneratedModels.LinkShipFleetModel
    {
        public class LinkShipFleetMockRepository<T> : IRepository<LinkShipFleet>, IDisposable
        {
            private readonly List<LinkShipFleet>? Items;
            string PostFix { get; set; }

            public LinkShipFleetMockRepository(string postFix)
            {
                PostFix = postFix;
                Items = [];
            }

            public void Add(LinkShipFleet? entity)
            {
                if (Items != null && entity != null) { Items?.Add(entity); }
            }

            public IEnumerable<LinkShipFleet>? All()
            {
            var FleetsItems = AL.FleetsHelper?.Items;
            var ShipsItems = AL.ShipsHelper?.Items;
            if (FleetsItems != null && ShipsItems != null)
                for (int i = 0; i < FleetsItems.Count; i++)
                {
                    FleetsModel.Fleets? Fleets = FleetsItems[i];
                    ShipsModel.Ships? Ships = ShipsItems[i];
                    Items?.Add(new LinkShipFleet($"{Ships.Name}-{Fleets.Name}", 0, Ships.ShipsGuid, Fleets.FleetsGuid));
                }
                return Items;
            }

            public void Dispose()
            {
                Items?.Clear();
                GC.SuppressFinalize(this);
            }

            public IEnumerable<LinkShipFleet>? GetByGUID(string guid)
            {
                if (Items == null) { return null; }
                return Items.Where(p => p.LinkShipFleetGuid != null && p.LinkShipFleetGuid == guid);
            }

            public IEnumerable<LinkShipFleet>? GetById(int id)
            {
                if (Items == null) { return null; }
                return Items.Where(p => p.Id == id);
            }

            public IEnumerable<LinkShipFleet>? GetByName(string name)
            {
                if (Items == null) { return null; }
                return Items.Where(p => p.Name == name);
            }

            public bool Remove(LinkShipFleet? entity)
            {
                if (Items != null && entity != null) { return Items.Remove(entity); }
                return false;
            }

            public int Count()
            {
                return Items == null ? 0 : Items.Count;
            }

            public List<LinkShipFleet>? OrderByIndex()
            {
                return (List<LinkShipFleet>?)Items?.OrderBy(p => p.Index);
            }
        }
    }
