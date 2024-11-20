// Generated Code - Version: 23.11.25 - 2024/11/20 17:29:24 - {64b62547-252c-4706-a962-7f21c734af25}

using ArchCorpUtilities.Models;
namespace ArchCorpUtilities.GeneratedModels.LinkBuildingsTypesBuildingsModel;
using AL = ArchLoader;
public class LinkBuildingsTypesBuildings
{
    public string? Name { get; }
    public string? BuildingTypeGuid { get; }
    public string? BuildingsGuid { get; }
    public int Id { get; set; }
    public int Index { get; set; }
    public int DisplayId { get; internal set; }
    public string? LinkBuildingsTypesBuildingsGuid {get; internal set; }
    public LinkBuildingsTypesBuildings(string? name , int id ,string? buildingtypeGuid,string? buildingsGuid, string guid = "")
    {
        Name = name;
        BuildingsGuid = buildingsGuid;
        BuildingTypeGuid = buildingtypeGuid;
        Id = id;
        DisplayId = Id;
        if (guid != null && guid.Length > 0)
        {
            LinkBuildingsTypesBuildingsGuid = guid;
        }
        else
        {
            LinkBuildingsTypesBuildingsGuid = Guid.NewGuid().ToString();
        }
    }
    public void SetDisplayId(int displayId) { DisplayId = displayId; }
    public override string ToString() {return $"{DisplayId}) {Name} -- Buildings-BuildingType";}
}
