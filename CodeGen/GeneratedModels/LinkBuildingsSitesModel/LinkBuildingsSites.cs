// Generated Code - Version: 23.11.25 - 2024/11/20 17:29:24 - {64b62547-252c-4706-a962-7f21c734af25}

using ArchCorpUtilities.Models;
namespace ArchCorpUtilities.GeneratedModels.LinkBuildingsSitesModel;
using AL = ArchLoader;
public class LinkBuildingsSites
{
    public string? Name { get; }
    public string? BuildingsGuid { get; }
    public string? SitesGuid { get; }
    public int Id { get; set; }
    public int Index { get; set; }
    public int DisplayId { get; internal set; }
    public string? LinkBuildingsSitesGuid {get; internal set; }
    public LinkBuildingsSites(string? name , int id ,string? buildingsGuid,string? sitesGuid, string guid = "")
    {
        Name = name;
        SitesGuid = sitesGuid;
        BuildingsGuid = buildingsGuid;
        Id = id;
        DisplayId = Id;
        if (guid != null && guid.Length > 0)
        {
            LinkBuildingsSitesGuid = guid;
        }
        else
        {
            LinkBuildingsSitesGuid = Guid.NewGuid().ToString();
        }
    }
    public void SetDisplayId(int displayId) { DisplayId = displayId; }
    public override string ToString() {return $"{DisplayId}) {Name} -- Sites-Buildings";}
}
