// Generated Code - Version: 23.11.25 - 2024/11/20 17:29:26 - {64b62547-252c-4706-a962-7f21c734af25}

using ArchCorpUtilities.Models;
namespace ArchCorpUtilities.GeneratedModels.LinkShipFleetModel;
using AL = ArchLoader;
public class LinkShipFleet
{
    public string? Name { get; }
    public string? ShipsGuid { get; }
    public string? FleetsGuid { get; }
    public int Id { get; set; }
    public int Index { get; set; }
    public int DisplayId { get; internal set; }
    public string? LinkShipFleetGuid {get; internal set; }
    public LinkShipFleet(string? name , int id ,string? shipsGuid,string? fleetsGuid, string guid = "")
    {
        Name = name;
        FleetsGuid = fleetsGuid;
        ShipsGuid = shipsGuid;
        Id = id;
        DisplayId = Id;
        if (guid != null && guid.Length > 0)
        {
            LinkShipFleetGuid = guid;
        }
        else
        {
            LinkShipFleetGuid = Guid.NewGuid().ToString();
        }
    }
    public void SetDisplayId(int displayId) { DisplayId = displayId; }
    public override string ToString() {return $"{DisplayId}) {Name} -- Fleets-Ships";}
}
