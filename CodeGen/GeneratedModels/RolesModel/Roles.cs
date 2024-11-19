// Generated Code - Version: 23.11.25 - 2024/11/19 19:23:44 - {2a4371ca-84c4-469c-a5be-31fe909f01e4}

namespace ArchCorpUtilities.Models.RolesModel.Roles;

using AL = ArchCorpUtilities.Models.ArchLoader;

public class Roles
{
    public string? Name { get; }
    public int Id { get; set; }
    public int Index { get; set; }
    public int DisplayId { get; internal set; }
    public string? RolesGuid {get; internal set; }
    public Roles(string? name, int id, string guid = "")
    {
        Name = name;
        Id = id;
        DisplayId = Id;
        if (guid != null && guid.Length > 0)
        {
            RolesGuid = guid;
        }
        else
        {
            RolesGuid = Guid.NewGuid().ToString();
        }
    }
    public void SetDisplayId(int displayId) { DisplayId = displayId; }
    public override string ToString() {return $"{DisplayId}) {Name}";}
}
