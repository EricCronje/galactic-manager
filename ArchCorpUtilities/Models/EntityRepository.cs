using L = Logger.Logger;
namespace ArchCorpUtilities.Models
{
    public class EntityRepository<T> : IRepository<T>, IDisposable where T : EntityBase, new()
    {
        public readonly List<T>? Items = [];

        public EntityRepository(string postFix)
        {
            PostFix = postFix;
        }


        public EntityRepository() { PostFix = ""; }
        string PostFix { get; set; }
        public string? SessionId { get; set; }
        public void Add(T? entity)
        {
            if (Items != null && entity != null) { Items?.Add(entity); }
        }
        public virtual IEnumerable<T>? All()
        {
            string[] ItemsToAdd = ["Alpha", "Beta", "Charlie", "Delta", "Echo", "Foxtrot", "Golf", "Hotel"];
            foreach (var item in ItemsToAdd)
            {
                T NewEntity = new()
                {
                    Name = $"{item}-{PostFix}",
                    Id = 0,
                    Index = 0,
                    DisplayId = 0,
                    Guid_ = Guid.NewGuid().ToString()
                };
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

        public IEnumerable<T>? GetByGUID(string guid)
        {
            if (Items == null) { return null; }
            var Result = Items.Where(p => p.Guid_ != null && p.Guid_ == guid);
            if (!Result.Any()) return null;
            return Result;
        }

        public IEnumerable<T>? GetById(int id)
        {
            if (Items == null) { return null; }
            var Result = Items.Where(p => p.Id == id);
            if (!Result.Any()) return null;
            return Result;
        }

        public IEnumerable<T>? GetByName(string name)
        {
            if (Items == null) { return null; }
            var Result = Items.Where(p => p.Name != null && p.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));
            if (!Result.Any()) return null;
            return Result;
        }

        public bool Remove(T? entity)
        {
            if (Items != null && entity != null) { return Items.Remove(entity); }
            return false;
        }

        public int Count()
        {
            return Items == null ? 0 : Items.Count;
        }

        public List<T>? OrderByIndex()
        {
            return Items?.OrderBy(p => p.Index).ToList();
        }
        public List<T>? OrderByName()
        {
            return Items?.OrderBy(p => p.Name).ToList();
        }
        public List<T>? GetAllContainingName(string? input)
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
        public IEnumerable<T>? GetLinked()
        {
            return All()?.Where(p => p.IsLinked);
        }

        public bool ClearAll()
        {
            if (Items == null) return false;
            Items?.Clear();
            return true;
        }
    }
}
