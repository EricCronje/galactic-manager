// Generated Code - Version: 15.11.22 - 2024/11/12 15:35:47 - {14854933-58be-4cba-af9e-d6d346aefeec}

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
