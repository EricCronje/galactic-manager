using ArchCorpUtilities.Models;
using AL = ArchCorpUtilities.Models.ArchLoader;
using L = Logger.Logger;
namespace ArchCorpUtilities.GeneratedModels.~Entity~Model
{
    public class ~Entity~MockRepository<T>(string postFix) : IRepository<Link>, IDisposable
        {
            private readonly List<Link>? Items = [];
            string PostFix { get; set; } = postFix;
            public string? SessionId { get; set; }
            public void Add(Link? entity)
            {
                if (Items != null && entity != null) { Items?.Add(entity); }
            }
            public IEnumerable<Link>? All()
            {
            var ~LhLink~Items = AL.~LhLink~Helper?.Repository?.All()?.ToList();
            var ~RhLink~Items = AL.~RhLink~Helper?.Repository?.All()?.ToList();
            if (~LhLink~Items != null && ~RhLink~Items != null)
                for (int i = 0; i < ~LhLink~Items.Count; i++)
                {
                    ~LhLink~Model.~LhLink~? ~LhLink~ = ~LhLink~Items[i];
                    ~RhLink~Model.~RhLink~? ~RhLink~ = ~RhLink~Items[i];
                    Items?.Add(new Link($"{~RhLink~.Name}-{~LhLink~.Name}", 0, ~RhLink~.Guid_, ~LhLink~.Guid_, ~RhLink~.Name, ~LhLink~.Name));
                }
                return Items;
            }

            public void Dispose()
            {
                Items?.Clear();
                GC.SuppressFinalize(this);
            }

            public IEnumerable<Link>? GetByGUID(string guid)
            {
                if (Items == null) { return null; }
                var Result = Items.Where(p => p.Guid_ != null && p.Guid_ == guid);
                if (!Result.Any()) return null;
                return Result;
            }

            public IEnumerable<Link>? GetById(int id)
            {
                if (Items == null) { return null; }
                var Result = Items.Where(p => p.Id == id);
                if (!Result.Any()) return null;
                return Result;
            }

            public IEnumerable<Link>? GetByName(string name)
            {
                if (Items == null) { return null; }
                var Result = Items.Where(p => p.Name != null && p.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));
                if (!Result.Any()) return null;
                return Result;
            }

            public bool Remove(Link? entity)
            {
                if (Items != null && entity != null) { return Items.Remove(entity); }
                return false;
            }

            public int Count()
            {
                return Items == null ? 0 : Items.Count;
            }

            public List<Link>? OrderByIndex()
            {
                return Items?.OrderBy(p => p.Index).ToList();
            }
            public List<Link>? OrderByName()
            {
                return Items?.OrderBy(p => p.Name).ToList();
            }
            public List<Link>? GetAllContainingName(string? input)
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
            public IEnumerable<Link>? GetLinked()
            {
                return All()?.Where(p => p.IsLinked);
            }
        }
    }