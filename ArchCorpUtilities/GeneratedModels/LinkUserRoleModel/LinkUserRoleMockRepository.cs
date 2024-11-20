// Generated Code - Version: 23.11.25 - 2024/11/20 17:29:27 - {64b62547-252c-4706-a962-7f21c734af25}
using ArchCorpUtilities.Models;
using AL = ArchCorpUtilities.Models.ArchLoader;

namespace ArchCorpUtilities.GeneratedModels.LinkUserRoleModel
    {
        public class LinkUserRoleMockRepository<T> : IRepository<LinkUserRole>, IDisposable
        {
            private readonly List<LinkUserRole>? Items;
            string PostFix { get; set; }

            public LinkUserRoleMockRepository(string postFix)
            {
                PostFix = postFix;
                Items = [];
            }

            public void Add(LinkUserRole? entity)
            {
                if (Items != null && entity != null) { Items?.Add(entity); }
            }

            public IEnumerable<LinkUserRole>? All()
            {
            var RolesItems = AL.RolesHelper?.Items;
            var UsersItems = AL.UsersHelper?.Items;
            if (RolesItems != null && UsersItems != null)
                for (int i = 0; i < RolesItems.Count; i++)
                {
                    RolesModel.Roles? Roles = RolesItems[i];
                    UsersModel.Users? Users = UsersItems[i];
                    Items?.Add(new LinkUserRole($"{Users.Name}-{Roles.Name}", 0, Users.UsersGuid, Roles.RolesGuid));
                }
                return Items;
            }

            public void Dispose()
            {
                Items?.Clear();
                GC.SuppressFinalize(this);
            }

            public IEnumerable<LinkUserRole>? GetByGUID(string guid)
            {
                if (Items == null) { return null; }
                return Items.Where(p => p.LinkUserRoleGuid != null && p.LinkUserRoleGuid == guid);
            }

            public IEnumerable<LinkUserRole>? GetById(int id)
            {
                if (Items == null) { return null; }
                return Items.Where(p => p.Id == id);
            }

            public IEnumerable<LinkUserRole>? GetByName(string name)
            {
                if (Items == null) { return null; }
                return Items.Where(p => p.Name == name);
            }

            public bool Remove(LinkUserRole? entity)
            {
                if (Items != null && entity != null) { return Items.Remove(entity); }
                return false;
            }

            public int Count()
            {
                return Items == null ? 0 : Items.Count;
            }

            public List<LinkUserRole>? OrderByIndex()
            {
                return (List<LinkUserRole>?)Items?.OrderBy(p => p.Index);
            }
        }
    }
