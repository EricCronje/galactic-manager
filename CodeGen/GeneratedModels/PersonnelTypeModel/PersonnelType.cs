// Generated Code - Version: 23.11.25 - 2024/11/20 17:29:21 - {64b62547-252c-4706-a962-7f21c734af25}

using ArchCorpUtilities.Models;
namespace ArchCorpUtilities.GeneratedModels.PersonnelTypeModel;
using AL = ArchLoader;
public class PersonnelType
{
    public string? Name { get; }
    public int Id { get; set; }
    public int Index { get; set; }
    public int DisplayId { get; internal set; }
    public string? PersonnelTypeGuid {get; internal set; }
    public PersonnelType(string? name, int id, string guid = "")
    {
        Name = name;
        Id = id;
        DisplayId = Id;
        if (guid != null && guid.Length > 0)
        {
            PersonnelTypeGuid = guid;
        }
        else
        {
            PersonnelTypeGuid = Guid.NewGuid().ToString();
        }
    }
    public void SetDisplayId(int displayId) { DisplayId = displayId; }
    public override string ToString() {return $"{DisplayId}) {Name}";}
}
