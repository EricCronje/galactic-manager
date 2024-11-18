// Generated Code - Version: <Version> - <Date> - {cc78f0cc-4ab8-4dd6-a01e-546c48d76b98}

namespace ArchCorpUtilities.Models.BeaconsModel.Beacons;
public class Beacons
{
    public string? Name { get; }
    public int Id { get; set; }
    public int Index { get; set; }
    public int DisplayId { get; internal set; }
    public string? BeaconsGuid {get; internal set; }
    public Beacons(string? name, int id, string guid = "")
    {
        Name = name;
        Id = id;
        DisplayId = Id;
        if (guid != null && guid.Length > 0)
        {
            BeaconsGuid = guid;
        }
        else
        {
            BeaconsGuid = Guid.NewGuid().ToString();
        }
    }
    public void SetDisplayId(int displayId) { DisplayId = displayId; }
    public override string ToString() {return $"{DisplayId}) {Name}";}
}
