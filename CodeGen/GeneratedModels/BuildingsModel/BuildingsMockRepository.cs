// Generated Code - Version: <Version> - <Date> - {91430f8b-6f80-4c3e-9376-72b046fb8d41}
using ArchCorpUtilities.Models;
using AL = ArchCorpUtilities.Models.ArchLoader;
namespace ArchCorpUtilities.GeneratedModels.BuildingsModel
    {
        public class BuildingsMockRepository<T>(string postFix) : IRepository<Buildings>, IDisposable
        {
            private readonly List<Buildings>? Items = [];
            string PostFix { get; set; } = postFix;
            public void Add(Buildings? entity)
            {
                if (Items != null && entity != null) { Items?.Add(entity); }
            }
            public IEnumerable<Buildings>? All()
            {
                string[] ItemsToAdd = ["Alpha", "Beta", "Charlie", "Delta", "Echo", "Foxtrot", "Golf", "Hotel", "India", "Juliet", "Kilo", "Lima", "Mike", "November", "October", "Papa", "Quebec", "Romeo", "Sierra", "Tango"];
                foreach (var item in ItemsToAdd)
                {
                    Buildings NewEntity = new($"{item}{PostFix}", 0, Guid.NewGuid().ToString());
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

            public IEnumerable<Buildings>? GetByGUID(string guid)
            {
                if (Items == null) { return null; }
                var Result = Items.Where(p => p.BuildingsGuid != null && p.BuildingsGuid == guid);
                if (!Result.Any()) return null;
                return Result;
            }

            public IEnumerable<Buildings>? GetById(int id)
            {
                if (Items == null) { return null; }
                var Result = Items.Where(p => p.Id == id);
                if (!Result.Any()) return null;
                return Result;
            }

            public IEnumerable<Buildings>? GetByName(string name)
            {
                if (Items == null) { return null; }
                var Result = Items.Where(p => p.Name != null && p.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));
                if (!Result.Any()) return null;
                return Result;
            }

            public bool Remove(Buildings? entity)
            {
                if (Items != null && entity != null) { return Items.Remove(entity); }
                return false;
            }

            public int Count()
            {
                return Items == null ? 0 : Items.Count;
            }

            public List<Buildings>? OrderByIndex()
            {
                return Items?.OrderBy(p => p.Index).ToList();
            }
            public List<Buildings>? OrderByName()
            {
                return Items?.OrderBy(p => p.Name).ToList();
            }
            public List<Buildings>? GetAllContainingName(string? input)
            {
                if (input == null) return null;
                var Result = Items?.Where(c => c.Name != null && c.Name.ToUpper().Contains(input.ToUpper(), StringComparison.CurrentCultureIgnoreCase)).ToList();
                if (Result?.Count == 0) return null;
                return Result;
            }
        }
    }
