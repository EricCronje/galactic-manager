using ArchCorpUtilities.Models;
namespace ArchCorpUtilities.GeneratedModels.~Entity~Model;
public class ~Entity~(string? name, int id, string? rhGuid, string? lhGuid, string guid = "") : EntityBase(name, id, guid)
{
    public string? LhGuid { get; } = lhGuid;
    public string? RhGuid { get; } = rhGuid;
    public override string ToString() {return $"{DisplayId}) {Name} -- ~RhLink~-~LhLink~";}
}