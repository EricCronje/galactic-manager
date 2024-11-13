// Generated Code - Version: 15.11.22 - 2024/11/12 15:37:14 - {67647dcc-da4d-495a-9afd-f552d6df7bd3}

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
