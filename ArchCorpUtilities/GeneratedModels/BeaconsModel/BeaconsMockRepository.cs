// Generated Code - Version: 23.11.25 - 2024/11/20 17:29:23 - {64b62547-252c-4706-a962-7f21c734af25}
using ArchCorpUtilities.Models;
using AL = ArchCorpUtilities.Models.ArchLoader;

namespace ArchCorpUtilities.GeneratedModels.BeaconsModel
    {
        public class BeaconsMockRepository<T> : IRepository<Beacons>, IDisposable
        {
            private readonly List<Beacons>? Items;
            string PostFix { get; set; }

            public BeaconsMockRepository(string postFix)
            {
                PostFix = postFix;
                Items = [];
            }

            public void Add(Beacons? entity)
            {
                if (Items != null && entity != null) { Items?.Add(entity); }
            }

            public IEnumerable<Beacons>? All()
            {
                string[] ItemsToAdd = ["Alpha", "Beta", "Charlie", "Delta", "Echo", "Foxtrot", "Golf", "Hotel"];
                foreach (var item in ItemsToAdd)
                {
                    Beacons NewEntity = new($"{item}{PostFix}", 0, Guid.NewGuid().ToString());
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

            public IEnumerable<Beacons>? GetByGUID(string guid)
            {
                if (Items == null) { return null; }
                return Items.Where(p => p.BeaconsGuid != null && p.BeaconsGuid == guid);
            }

            public IEnumerable<Beacons>? GetById(int id)
            {
                if (Items == null) { return null; }
                return Items.Where(p => p.Id == id);
            }

            public IEnumerable<Beacons>? GetByName(string name)
            {
                if (Items == null) { return null; }
                return Items.Where(p => p.Name == name);
            }

            public bool Remove(Beacons? entity)
            {
                if (Items != null && entity != null) { return Items.Remove(entity); }
                return false;
            }

            public int Count()
            {
                return Items == null ? 0 : Items.Count;
            }

            public List<Beacons>? OrderByIndex()
            {
                return (List<Beacons>?)Items?.OrderBy(p => p.Index);
            }
        }
    }
