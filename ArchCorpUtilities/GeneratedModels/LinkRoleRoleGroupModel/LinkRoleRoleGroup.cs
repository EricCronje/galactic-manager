// Generated Code - Version: 23.11.25 - 2024/11/20 17:29:27 - {64b62547-252c-4706-a962-7f21c734af25}

using ArchCorpUtilities.Models;
namespace ArchCorpUtilities.GeneratedModels.LinkRoleRoleGroupModel;
using AL = ArchLoader;
public class LinkRoleRoleGroup
{
    public string? Name { get; }
    public string? RolesGuid { get; }
    public string? RoleGroupsGuid { get; }
    public int Id { get; set; }
    public int Index { get; set; }
    public int DisplayId { get; internal set; }
    public string? LinkRoleRoleGroupGuid {get; internal set; }
    public LinkRoleRoleGroup(string? name , int id ,string? rolesGuid,string? rolegroupsGuid, string guid = "")
    {
        Name = name;
        RoleGroupsGuid = rolegroupsGuid;
        RolesGuid = rolesGuid;
        Id = id;
        DisplayId = Id;
        if (guid != null && guid.Length > 0)
        {
            LinkRoleRoleGroupGuid = guid;
        }
        else
        {
            LinkRoleRoleGroupGuid = Guid.NewGuid().ToString();
        }
    }
    public void SetDisplayId(int displayId) { DisplayId = displayId; }
    public override string ToString() {return $"{DisplayId}) {Name} -- RoleGroups-Roles";}
}
