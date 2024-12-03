using ArchCorpUtilities.Models;
namespace ArchCorpUtilities.GeneratedModels.~Entity~Model;
using AL = ArchLoader;
public class ~Entity~ : EntityBase
{
    public string? Parent { get; set; }
    public string? Child { get; set; }

    public ~Entity~(string? name, int id, string guid = "", string parent = "<None>", string child = "<None>") : base(name, id, guid)
    {
        Parent = parent;
        Child = child;
    }

    public ~Entity~() { Parent = "<None>"; Child = "<None>"; }

    public override string ToString()
    {
        if (Parent == null || Child == null ) return string.Empty;
        var ParentName = AL.BuildingsHelper?.Repository?.GetByGUID(Parent)?.ToList()[0].Name;
        var ChildName = AL.BuildingsHelper?.Repository?.GetByGUID(Child)?.ToList()[0].Name;
        return $"{DisplayId}) {Name} - (p) {ParentName} (c) {ChildName}";
    }
}

