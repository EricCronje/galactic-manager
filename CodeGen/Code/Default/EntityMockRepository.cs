using ArchCorpUtilities.Models;
using AL = ArchCorpUtilities.Models.ArchLoader;
using L = Logger.Logger;
namespace ArchCorpUtilities.GeneratedModels.~Entity~Model
    {
        public class ~Entity~MockRepository<T>(string postFix) : IRepository<~Entity~>, IDisposable
        {
            private readonly List<~Entity~>? Items = [];
            string PostFix { get; set; } = postFix;
            public string? SessionId { get; set; }
            public void Add(~Entity~? entity)
            {
                if (Items != null && entity != null) { Items?.Add(entity); }
            }
            public IEnumerable<~Entity~>? All()
            {
                string[] ItemsToAdd = ["Alpha", "Beta", "Charlie", "Delta", "Echo", "Foxtrot", "Golf", "Hotel"];
                foreach (var item in ItemsToAdd)
                {
                    ~Entity~ NewEntity = new($"{item}{PostFix}", 0, Guid.NewGuid().ToString());
                    if (Items != null && GetAllContainingName(NewEntity.Name)?.Count == 0)
                        Add(NewEntity);
                }
                return Items;
            }

            public void Dispose()
            {
                Items?.Clear();
                GC.SuppressFinalize(this);
            }

            public IEnumerable<~Entity~>? GetByGUID(string guid)
            {
                if (Items == null) { return null; }
                var Result = Items.Where(p => p.Guid_ != null && p.Guid_ == guid);
                if (!Result.Any()) return null;
                return Result;
            }

            public IEnumerable<~Entity~>? GetById(int id)
            {
                if (Items == null) { return null; }
                var Result = Items.Where(p => p.Id == id);
                if (!Result.Any()) return null;
                return Result;
            }

            public IEnumerable<~Entity~>? GetByName(string name)
            {
                if (Items == null) { return null; }
                var Result = Items.Where(p => p.Name != null && p.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));
                if (!Result.Any()) return null;
                return Result;
            }

            public bool Remove(~Entity~? entity)
            {
                if (Items != null && entity != null) { return Items.Remove(entity); }
                return false;
            }

            public int Count()
            {
                return Items == null ? 0 : Items.Count;
            }

            public List<~Entity~>? OrderByIndex()
            {
                return Items?.OrderBy(p => p.Index).ToList();
            }
            public List<~Entity~>? OrderByName()
            {
                return Items?.OrderBy(p => p.Name).ToList();
            }
            public List<~Entity~>? GetAllContainingName(string? input)
            {
                if (input == null) return null;
                if (Items == null) return null;
                try
                {
                    var Result = Items?.Where(c => c.Name != null && c.Name.Contains(input, StringComparison.CurrentCultureIgnoreCase)).ToList();
                    if (Result?.Count == 0) return Result;
                    return Result;
                }
                catch (Exception ex)
                {
                    if (SessionId != null)
                        L.Log($"Error in GetAllContainingName - {ex.Message} -- {ex.InnerException?.Message}", SessionId, 9);
                    return null;
                }
            }
            public IEnumerable<~Entity~>? GetLinked()
            {
                return All()?.Where(p => p.IsLinked);
            }
        }
    }
