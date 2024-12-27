using ArchCorpUtilities.Models;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using L = Logger.Logger;
using U = ArchCorpUtilities.Utilities.UniversalUtilities;

namespace Auth
{
    public class MockEntityRepositoryAuth<T> : EntityRepository<T> where T : User, new()
    {
        public MockEntityRepositoryAuth()
        {
        }

        public MockEntityRepositoryAuth(string postFix) : base(postFix)
        {
        }

        public override IEnumerable<T>? All()
        {
            string[] ItemsToAdd = ["Super|Super|9|Super|Super", "Clerk|Clerk|1|Clerk|Clerk"];
            foreach (var item in ItemsToAdd)
            {
                var Items = item.Split('|');

                if (Items.Length > 0)
                {
                    var Pass = U.Encrypt(Items[1]) ?? "";
                    T NewEntity = new()
                    {
                        UserName = Items[0],
                        PassWord = Pass,
                        Level = Convert.ToInt32(Items[2]),
                        Name = Items[3],
                        Surname = Items[4],
                        Id = 0,
                        Index = 0,
                        DisplayId = 0,
                        Guid_ = Guid.NewGuid().ToString()
                    };
                    if (Items != null && GetAllContainingName(NewEntity.UserName)?.Count == 0)
                        Add(NewEntity);
                }
            }
            return Items;
        }

        public override List<T>? GetAllContainingName(string? input)
        {
            if (input == null || Items == null) return null;
            try
            {
                var Result = Items?.Where(c => c.UserName != null && c.UserName.Contains(input, StringComparison.CurrentCultureIgnoreCase)).ToList();
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

    }
}