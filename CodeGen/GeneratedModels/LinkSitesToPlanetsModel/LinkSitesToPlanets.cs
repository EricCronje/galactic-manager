// Generated Code - Version: 23.11.25 - 2024/11/19 18:50:40 - {e16e756f-a27f-493b-bfa9-6667c239b926}

namespace ArchCorpUtilities.Models.LinkSitesToPlanetsModel.LinkSitesToPlanets;

using AL = ArchCorpUtilities.Models.ArchLoader;

public class LinkSitesToPlanets
{
    public string? Name { get; }
    public string? SitesGuid { get; }
    public string? PlanetsGuid { get; }
    public int Id { get; set; }
    public int Index { get; set; }
    public int DisplayId { get; internal set; }
    public string? LinkSitesToPlanetsGuid {get; internal set; }
    public LinkSitesToPlanets(string? name , int id ,string? sitesGuid,string? planetsGuid, string guid = "")
    {
        Name = name;
        PlanetsGuid = planetsGuid;
        SitesGuid = sitesGuid;
        Id = id;
        DisplayId = Id;
        if (guid != null && guid.Length > 0)
        {
            LinkSitesToPlanetsGuid = guid;
        }
        else
        {
            LinkSitesToPlanetsGuid = Guid.NewGuid().ToString();
        }
    }
    public void SetDisplayId(int displayId) { DisplayId = displayId; }
    public override string ToString() {return $"{DisplayId}) {Name} -- Planets-Sites";}
}
