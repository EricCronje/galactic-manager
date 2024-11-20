// Generated Code - Version: 23.11.25 - 2024/11/20 17:29:25 - {64b62547-252c-4706-a962-7f21c734af25}

using ArchCorpUtilities.Models;
namespace ArchCorpUtilities.GeneratedModels.LinkPersonnelShipModel;
using AL = ArchLoader;
public class LinkPersonnelShip
{
    public string? Name { get; }
    public string? PersonnelGuid { get; }
    public string? ShipsGuid { get; }
    public int Id { get; set; }
    public int Index { get; set; }
    public int DisplayId { get; internal set; }
    public string? LinkPersonnelShipGuid {get; internal set; }
    public LinkPersonnelShip(string? name , int id ,string? personnelGuid,string? shipsGuid, string guid = "")
    {
        Name = name;
        ShipsGuid = shipsGuid;
        PersonnelGuid = personnelGuid;
        Id = id;
        DisplayId = Id;
        if (guid != null && guid.Length > 0)
        {
            LinkPersonnelShipGuid = guid;
        }
        else
        {
            LinkPersonnelShipGuid = Guid.NewGuid().ToString();
        }
    }
    public void SetDisplayId(int displayId) { DisplayId = displayId; }
    public override string ToString() {return $"{DisplayId}) {Name} -- Ships-Personnel";}
}
