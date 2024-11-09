// Generated Code - Version: 1.0.0 - 2024/11/09 22:15:42 - {2991c9ad-4969-45a7-87cc-0857a2dd7797} 
//Generated code : 2024/11/09 22:15:42 - Session : 2991c9ad-4969-45a7-87cc-0857a2dd7797
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
