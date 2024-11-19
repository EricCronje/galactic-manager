// Generated Code - Version: 23.11.25 - 2024/11/19 18:50:39 - {e16e756f-a27f-493b-bfa9-6667c239b926}

namespace ArchCorpUtilities.Models.UsersModel.Users;

using AL = ArchCorpUtilities.Models.ArchLoader;

public class Users
{
    public string? Name { get; }
    public int Id { get; set; }
    public int Index { get; set; }
    public int DisplayId { get; internal set; }
    public string? UsersGuid {get; internal set; }
    public Users(string? name, int id, string guid = "")
    {
        Name = name;
        Id = id;
        DisplayId = Id;
        if (guid != null && guid.Length > 0)
        {
            UsersGuid = guid;
        }
        else
        {
            UsersGuid = Guid.NewGuid().ToString();
        }
    }
    public void SetDisplayId(int displayId) { DisplayId = displayId; }
    public override string ToString() {return $"{DisplayId}) {Name}";}
}
