using ArchCorpUtilities.Models;
namespace ArchCorpUtilities.GeneratedModels.~Entity~Model;
public class ~Entity~ : EntityLinkBase
{
    public ~Entity~(string? name, int id, string? rhGuid, string? lhGuid, string? rhName, string? lhName,  string guid = "") : base(name, id, rhGuid, lhGuid, rhName, lhName, guid ) {}
    public ~Entity~() { }
    public override string ToString() {return $"{DisplayId}) {Name} -- {LhName} - {RhName}";}
}