// Generated Code - Version: 23.11.25 - 2024/11/14 23:54:43 - {7328f621-8c9b-4ae9-b2c6-e5c96ce8746e}

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
