namespace ArchCorpUtilities.Models;
public class Link : EntityLinkBase
{
    public Link(string? name, int id, string? rhGuid, string? lhGuid, string? rhName, string? lhName, string guid = "") : base(name, id, rhGuid, lhGuid, rhName, lhName, guid) { }
    public Link() { }
    public override string ToString() { return $"{DisplayId}) {Name} -- {LhName} - {RhName}"; }
}