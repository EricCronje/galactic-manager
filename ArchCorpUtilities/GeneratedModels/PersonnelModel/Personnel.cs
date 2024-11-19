// Generated Code - Version: 23.11.25 - 2024/11/19 19:23:43 - {2a4371ca-84c4-469c-a5be-31fe909f01e4}

namespace ArchCorpUtilities.Models.PersonnelModel.Personnel;

using AL = ArchCorpUtilities.Models.ArchLoader;

public class Personnel
{
    public string? Name { get; }
    public int Id { get; set; }
    public int Index { get; set; }
    public int DisplayId { get; internal set; }
    public string? PersonnelGuid {get; internal set; }
    public Personnel(string? name, int id, string guid = "")
    {
        Name = name;
        Id = id;
        DisplayId = Id;
        if (guid != null && guid.Length > 0)
        {
            PersonnelGuid = guid;
        }
        else
        {
            PersonnelGuid = Guid.NewGuid().ToString();
        }
    }
    public void SetDisplayId(int displayId) { DisplayId = displayId; }
    public override string ToString() {return $"{DisplayId}) {Name}";}
}
