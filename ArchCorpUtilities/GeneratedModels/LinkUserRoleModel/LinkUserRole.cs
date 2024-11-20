// Generated Code - Version: 23.11.25 - 2024/11/20 17:29:27 - {64b62547-252c-4706-a962-7f21c734af25}

using ArchCorpUtilities.Models;
namespace ArchCorpUtilities.GeneratedModels.LinkUserRoleModel;
using AL = ArchLoader;
public class LinkUserRole
{
    public string? Name { get; }
    public string? UsersGuid { get; }
    public string? RolesGuid { get; }
    public int Id { get; set; }
    public int Index { get; set; }
    public int DisplayId { get; internal set; }
    public string? LinkUserRoleGuid {get; internal set; }
    public LinkUserRole(string? name , int id ,string? usersGuid,string? rolesGuid, string guid = "")
    {
        Name = name;
        RolesGuid = rolesGuid;
        UsersGuid = usersGuid;
        Id = id;
        DisplayId = Id;
        if (guid != null && guid.Length > 0)
        {
            LinkUserRoleGuid = guid;
        }
        else
        {
            LinkUserRoleGuid = Guid.NewGuid().ToString();
        }
    }
    public void SetDisplayId(int displayId) { DisplayId = displayId; }
    public override string ToString() {return $"{DisplayId}) {Name} -- Roles-Users";}
}
