// Generated Code - Version: 23.11.25 - 2024/11/20 17:29:28 - {64b62547-252c-4706-a962-7f21c734af25}

using ArchCorpUtilities.Models;
namespace ArchCorpUtilities.GeneratedModels.LinkBeaconRouteModel;
using AL = ArchLoader;
public class LinkBeaconRoute
{
    public string? Name { get; }
    public string? BeaconsGuid { get; }
    public string? RoutesGuid { get; }
    public int Id { get; set; }
    public int Index { get; set; }
    public int DisplayId { get; internal set; }
    public string? LinkBeaconRouteGuid {get; internal set; }
    public LinkBeaconRoute(string? name , int id ,string? beaconsGuid,string? routesGuid, string guid = "")
    {
        Name = name;
        RoutesGuid = routesGuid;
        BeaconsGuid = beaconsGuid;
        Id = id;
        DisplayId = Id;
        if (guid != null && guid.Length > 0)
        {
            LinkBeaconRouteGuid = guid;
        }
        else
        {
            LinkBeaconRouteGuid = Guid.NewGuid().ToString();
        }
    }
    public void SetDisplayId(int displayId) { DisplayId = displayId; }
    public override string ToString() {return $"{DisplayId}) {Name} -- Routes-Beacons";}
}
