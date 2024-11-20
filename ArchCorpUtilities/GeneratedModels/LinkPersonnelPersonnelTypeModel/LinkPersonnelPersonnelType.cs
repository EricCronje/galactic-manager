// Generated Code - Version: 23.11.25 - 2024/11/20 17:29:26 - {64b62547-252c-4706-a962-7f21c734af25}

using ArchCorpUtilities.Models;
namespace ArchCorpUtilities.GeneratedModels.LinkPersonnelPersonnelTypeModel;
using AL = ArchLoader;
public class LinkPersonnelPersonnelType
{
    public string? Name { get; }
    public string? PersonnelGuid { get; }
    public string? PersonnelTypeGuid { get; }
    public int Id { get; set; }
    public int Index { get; set; }
    public int DisplayId { get; internal set; }
    public string? LinkPersonnelPersonnelTypeGuid {get; internal set; }
    public LinkPersonnelPersonnelType(string? name , int id ,string? personnelGuid,string? personneltypeGuid, string guid = "")
    {
        Name = name;
        PersonnelTypeGuid = personneltypeGuid;
        PersonnelGuid = personnelGuid;
        Id = id;
        DisplayId = Id;
        if (guid != null && guid.Length > 0)
        {
            LinkPersonnelPersonnelTypeGuid = guid;
        }
        else
        {
            LinkPersonnelPersonnelTypeGuid = Guid.NewGuid().ToString();
        }
    }
    public void SetDisplayId(int displayId) { DisplayId = displayId; }
    public override string ToString() {return $"{DisplayId}) {Name} -- PersonnelType-Personnel";}
}
