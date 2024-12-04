namespace ArchCorpUtilities.Models;
public class EntityLinkBase : EntityBase
{
    public string? LhGuid { get; set;}
    public string? RhGuid { get; set;}
    public string? LhName { get; set;}
    public string? RhName { get; set;}

    public EntityLinkBase(string? name, int id, string? rhGuid, string? lhGuid, string? rhName, string? lhName, string guid = "") : base(name, id, guid)
    {
        LhGuid = lhGuid;
        RhGuid = rhGuid;
        LhName = lhName;
        RhName = rhName;
    }

    public EntityLinkBase() { }

    public override string ToString() { return $"{DisplayId}) {Name} -- {LhName}-{RhName}"; }
}