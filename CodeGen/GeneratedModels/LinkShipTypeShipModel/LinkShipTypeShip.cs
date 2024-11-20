// Generated Code - Version: 23.11.25 - 2024/11/20 17:29:26 - {64b62547-252c-4706-a962-7f21c734af25}

using ArchCorpUtilities.Models;
namespace ArchCorpUtilities.GeneratedModels.LinkShipTypeShipModel;
using AL = ArchLoader;
public class LinkShipTypeShip
{
    public string? Name { get; }
    public string? ShipTypeGuid { get; }
    public string? ShipsGuid { get; }
    public int Id { get; set; }
    public int Index { get; set; }
    public int DisplayId { get; internal set; }
    public string? LinkShipTypeShipGuid {get; internal set; }
    public LinkShipTypeShip(string? name , int id ,string? shiptypeGuid,string? shipsGuid, string guid = "")
    {
        Name = name;
        ShipsGuid = shipsGuid;
        ShipTypeGuid = shiptypeGuid;
        Id = id;
        DisplayId = Id;
        if (guid != null && guid.Length > 0)
        {
            LinkShipTypeShipGuid = guid;
        }
        else
        {
            LinkShipTypeShipGuid = Guid.NewGuid().ToString();
        }
    }
    public void SetDisplayId(int displayId) { DisplayId = displayId; }
    public override string ToString() {return $"{DisplayId}) {Name} -- Ships-ShipType";}
}
