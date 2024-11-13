// Generated Code - Version: 20.11.25 - 2024/11/14 03:19:10 - {a0d47dc6-67e7-402a-af37-33d83ed34429}

namespace ArchCorpUtilities.Models.BuildingsModel.Buildings;
public class Buildings
{
    public string? Name { get; }
    public int Id { get; set; }
    public int Index { get; set; }
    public int DisplayId { get; internal set; }
    public string? BuildingsGuid {get; internal set; }
    public Buildings(string? name, int id, string guid = "")
    {
        Name = name;
        Id = id;
        DisplayId = Id;
        if (guid != null && guid.Length > 0)
        {
            BuildingsGuid = guid;
        }
        else
        {
            BuildingsGuid = Guid.NewGuid().ToString();
        }
    }
    public void SetDisplayId(int displayId) { DisplayId = displayId; }
    public override string ToString() {return $"{DisplayId}) {Name}";}
}
