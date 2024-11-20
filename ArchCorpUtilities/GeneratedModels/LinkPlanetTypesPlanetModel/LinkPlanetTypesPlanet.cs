// Generated Code - Version: 23.11.25 - 2024/11/20 17:29:25 - {64b62547-252c-4706-a962-7f21c734af25}

using ArchCorpUtilities.Models;
namespace ArchCorpUtilities.GeneratedModels.LinkPlanetTypesPlanetModel;
using AL = ArchLoader;
public class LinkPlanetTypesPlanet
{
    public string? Name { get; }
    public string? PlanetTypeGuid { get; }
    public string? PlanetsGuid { get; }
    public int Id { get; set; }
    public int Index { get; set; }
    public int DisplayId { get; internal set; }
    public string? LinkPlanetTypesPlanetGuid {get; internal set; }
    public LinkPlanetTypesPlanet(string? name , int id ,string? planettypeGuid,string? planetsGuid, string guid = "")
    {
        Name = name;
        PlanetsGuid = planetsGuid;
        PlanetTypeGuid = planettypeGuid;
        Id = id;
        DisplayId = Id;
        if (guid != null && guid.Length > 0)
        {
            LinkPlanetTypesPlanetGuid = guid;
        }
        else
        {
            LinkPlanetTypesPlanetGuid = Guid.NewGuid().ToString();
        }
    }
    public void SetDisplayId(int displayId) { DisplayId = displayId; }
    public override string ToString() {return $"{DisplayId}) {Name} -- Planets-PlanetType";}
}
