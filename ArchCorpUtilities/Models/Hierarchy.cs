namespace ArchCorpUtilities.Models;

public class Hierarchy : Entity
{
    public string? Parent { get; set; }
    public string? Child { get; set; }
    public EntityRepository<Entity>? LinkRepository { get; set; }

    public Hierarchy(string? name, int id, string guid = "", string parent = "<None>", string child = "<None>") : base(name, id, guid)
    {
        Parent = parent;
        Child = child;
    }

    public Hierarchy() { Parent = "<None>"; Child = "<None>"; }

    public override string ToString()
    {
        if (Parent == null || Child == null) return string.Empty;
        var ParentName = LinkRepository?.GetByGUID(Parent)?.ToList()[0].Name;
        var ChildName = LinkRepository?.GetByGUID(Child)?.ToList()[0].Name;
        return $"{DisplayId}) {Name} - (p) {ParentName} (c) {ChildName}";
    }
}

