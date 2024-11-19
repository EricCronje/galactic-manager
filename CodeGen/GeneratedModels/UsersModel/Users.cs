// Generated Code - Version: 23.11.25 - 2024/11/19 19:23:44 - {2a4371ca-84c4-469c-a5be-31fe909f01e4}

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
