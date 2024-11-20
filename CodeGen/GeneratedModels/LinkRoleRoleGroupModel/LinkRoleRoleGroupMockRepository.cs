// Generated Code - Version: 23.11.25 - 2024/11/20 17:29:27 - {64b62547-252c-4706-a962-7f21c734af25}
using ArchCorpUtilities.Models;
using AL = ArchCorpUtilities.Models.ArchLoader;

namespace ArchCorpUtilities.GeneratedModels.LinkRoleRoleGroupModel
    {
        public class LinkRoleRoleGroupMockRepository<T> : IRepository<LinkRoleRoleGroup>, IDisposable
        {
            private readonly List<LinkRoleRoleGroup>? Items;
            string PostFix { get; set; }

            public LinkRoleRoleGroupMockRepository(string postFix)
            {
                PostFix = postFix;
                Items = [];
            }

            public void Add(LinkRoleRoleGroup? entity)
            {
                if (Items != null && entity != null) { Items?.Add(entity); }
            }

            public IEnumerable<LinkRoleRoleGroup>? All()
            {
            var RoleGroupsItems = AL.RoleGroupsHelper?.Items;
            var RolesItems = AL.RolesHelper?.Items;
            if (RoleGroupsItems != null && RolesItems != null)
                for (int i = 0; i < RoleGroupsItems.Count; i++)
                {
                    RoleGroupsModel.RoleGroups? RoleGroups = RoleGroupsItems[i];
                    RolesModel.Roles? Roles = RolesItems[i];
                    Items?.Add(new LinkRoleRoleGroup($"{Roles.Name}-{RoleGroups.Name}", 0, Roles.RolesGuid, RoleGroups.RoleGroupsGuid));
                }
                return Items;
            }

            public void Dispose()
            {
                Items?.Clear();
                GC.SuppressFinalize(this);
            }

            public IEnumerable<LinkRoleRoleGroup>? GetByGUID(string guid)
            {
                if (Items == null) { return null; }
                return Items.Where(p => p.LinkRoleRoleGroupGuid != null && p.LinkRoleRoleGroupGuid == guid);
            }

            public IEnumerable<LinkRoleRoleGroup>? GetById(int id)
            {
                if (Items == null) { return null; }
                return Items.Where(p => p.Id == id);
            }

            public IEnumerable<LinkRoleRoleGroup>? GetByName(string name)
            {
                if (Items == null) { return null; }
                return Items.Where(p => p.Name == name);
            }

            public bool Remove(LinkRoleRoleGroup? entity)
            {
                if (Items != null && entity != null) { return Items.Remove(entity); }
                return false;
            }

            public int Count()
            {
                return Items == null ? 0 : Items.Count;
            }

            public List<LinkRoleRoleGroup>? OrderByIndex()
            {
                return (List<LinkRoleRoleGroup>?)Items?.OrderBy(p => p.Index);
            }
        }
    }
