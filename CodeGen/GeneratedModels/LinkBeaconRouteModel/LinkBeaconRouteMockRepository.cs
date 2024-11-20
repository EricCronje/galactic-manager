// Generated Code - Version: 23.11.25 - 2024/11/20 17:29:28 - {64b62547-252c-4706-a962-7f21c734af25}
using ArchCorpUtilities.Models;
using AL = ArchCorpUtilities.Models.ArchLoader;

namespace ArchCorpUtilities.GeneratedModels.LinkBeaconRouteModel
    {
        public class LinkBeaconRouteMockRepository<T> : IRepository<LinkBeaconRoute>, IDisposable
        {
            private readonly List<LinkBeaconRoute>? Items;
            string PostFix { get; set; }

            public LinkBeaconRouteMockRepository(string postFix)
            {
                PostFix = postFix;
                Items = [];
            }

            public void Add(LinkBeaconRoute? entity)
            {
                if (Items != null && entity != null) { Items?.Add(entity); }
            }

            public IEnumerable<LinkBeaconRoute>? All()
            {
            var RoutesItems = AL.RoutesHelper?.Items;
            var BeaconsItems = AL.BeaconsHelper?.Items;
            if (RoutesItems != null && BeaconsItems != null)
                for (int i = 0; i < RoutesItems.Count; i++)
                {
                    RoutesModel.Routes? Routes = RoutesItems[i];
                    BeaconsModel.Beacons? Beacons = BeaconsItems[i];
                    Items?.Add(new LinkBeaconRoute($"{Beacons.Name}-{Routes.Name}", 0, Beacons.BeaconsGuid, Routes.RoutesGuid));
                }
                return Items;
            }

            public void Dispose()
            {
                Items?.Clear();
                GC.SuppressFinalize(this);
            }

            public IEnumerable<LinkBeaconRoute>? GetByGUID(string guid)
            {
                if (Items == null) { return null; }
                return Items.Where(p => p.LinkBeaconRouteGuid != null && p.LinkBeaconRouteGuid == guid);
            }

            public IEnumerable<LinkBeaconRoute>? GetById(int id)
            {
                if (Items == null) { return null; }
                return Items.Where(p => p.Id == id);
            }

            public IEnumerable<LinkBeaconRoute>? GetByName(string name)
            {
                if (Items == null) { return null; }
                return Items.Where(p => p.Name == name);
            }

            public bool Remove(LinkBeaconRoute? entity)
            {
                if (Items != null && entity != null) { return Items.Remove(entity); }
                return false;
            }

            public int Count()
            {
                return Items == null ? 0 : Items.Count;
            }

            public List<LinkBeaconRoute>? OrderByIndex()
            {
                return (List<LinkBeaconRoute>?)Items?.OrderBy(p => p.Index);
            }
        }
    }
