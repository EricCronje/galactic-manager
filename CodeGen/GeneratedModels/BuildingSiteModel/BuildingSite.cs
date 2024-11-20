// Generated Code - Version: 23.11.25 - 2024/11/20 17:17:26 - {bc074cbe-aae5-4d1e-aa83-fa69cd2e48f4}

using ArchCorpUtilities.Models;
namespace ArchCorpUtilities.GeneratedModels.BuildingSiteModel;
using AL = ArchLoader;
public class BuildingSite
{
    public string? Name { get; }
    public string? BuildingsGuid { get; }
    public string? SitesGuid { get; }
    public int Id { get; set; }
    public int Index { get; set; }
    public int DisplayId { get; internal set; }
    public string? BuildingSiteGuid {get; internal set; }
    public BuildingSite(string? name , int id ,string? buildingsGuid,string? sitesGuid, string guid = "")
    {
        Name = name;
        SitesGuid = sitesGuid;
        BuildingsGuid = buildingsGuid;
        Id = id;
        DisplayId = Id;
        if (guid != null && guid.Length > 0)
        {
            BuildingSiteGuid = guid;
        }
        else
        {
            BuildingSiteGuid = Guid.NewGuid().ToString();
        }
    }
    public void SetDisplayId(int displayId) { DisplayId = displayId; }
    public override string ToString() {return $"{DisplayId}) {Name} -- Sites-Buildings";}
}
