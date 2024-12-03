using ArchCorpUtilities.Models;
namespace ArchCorpUtilities.GeneratedModels.~Entity~Model;
using AL = ArchLoader;
public class ~Entity~(string? name, int id, string guid = "", string parent = "<None>", string child = "<None>") : EntityBase(name, id, guid)
{
    public string Parent { get; set; } = parent;
    public string Child { get; set; } = child;
    public override string ToString()
    {
        var ParentName = AL.~LhLink~Helper?.Repository?.GetByGUID(Parent)?.ToList()[0].Name;
        var ChildName = AL.~LhLink~Helper?.Repository?.GetByGUID(Child)?.ToList()[0].Name;
        return $"{DisplayId}) {Name} - (p) {ParentName} (c) {ChildName}";
    }
}
