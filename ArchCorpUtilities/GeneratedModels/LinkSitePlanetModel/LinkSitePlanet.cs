// Generated Code - Version: 23.11.25 - 2024/11/20 17:29:25 - {64b62547-252c-4706-a962-7f21c734af25}

using ArchCorpUtilities.Models;
namespace ArchCorpUtilities.GeneratedModels.LinkSitePlanetModel;
using AL = ArchLoader;
public class LinkSitePlanet
{
    public string? Name { get; }
    public string? SitesGuid { get; }
    public string? PlanetsGuid { get; }
    public int Id { get; set; }
    public int Index { get; set; }
    public int DisplayId { get; internal set; }
    public string? LinkSitePlanetGuid {get; internal set; }
    public LinkSitePlanet(string? name , int id ,string? sitesGuid,string? planetsGuid, string guid = "")
    {
        Name = name;
        PlanetsGuid = planetsGuid;
        SitesGuid = sitesGuid;
        Id = id;
        DisplayId = Id;
        if (guid != null && guid.Length > 0)
        {
            LinkSitePlanetGuid = guid;
        }
        else
        {
            LinkSitePlanetGuid = Guid.NewGuid().ToString();
        }
    }
    public void SetDisplayId(int displayId) { DisplayId = displayId; }
    public override string ToString() {return $"{DisplayId}) {Name} -- Planets-Sites";}
}
