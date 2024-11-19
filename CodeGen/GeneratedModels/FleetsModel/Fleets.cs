// Generated Code - Version: 23.11.25 - 2024/11/19 19:23:42 - {2a4371ca-84c4-469c-a5be-31fe909f01e4}

namespace ArchCorpUtilities.Models.FleetsModel.Fleets;

using AL = ArchCorpUtilities.Models.ArchLoader;

public class Fleets
{
    public string? Name { get; }
    public int Id { get; set; }
    public int Index { get; set; }
    public int DisplayId { get; internal set; }
    public string? FleetsGuid {get; internal set; }
    public Fleets(string? name, int id, string guid = "")
    {
        Name = name;
        Id = id;
        DisplayId = Id;
        if (guid != null && guid.Length > 0)
        {
            FleetsGuid = guid;
        }
        else
        {
            FleetsGuid = Guid.NewGuid().ToString();
        }
    }
    public void SetDisplayId(int displayId) { DisplayId = displayId; }
    public override string ToString() {return $"{DisplayId}) {Name}";}
}
