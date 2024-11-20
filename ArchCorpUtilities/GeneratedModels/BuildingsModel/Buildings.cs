// Generated Code - Version: <Version> - <Date> - {91430f8b-6f80-4c3e-9376-72b046fb8d41}

using ArchCorpUtilities.Models;
namespace ArchCorpUtilities.GeneratedModels.BuildingsModel;
using AL = ArchLoader;
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
    public override string ToString() => $"{DisplayId}) {Name}";
}
