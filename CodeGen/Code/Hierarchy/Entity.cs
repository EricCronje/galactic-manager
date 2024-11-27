using ArchCorpUtilities.Models;
namespace ArchCorpUtilities.GeneratedModels.~Entity~Model;
using AL = ArchLoader;
public class ~Entity~
{
    public string? Name { get; }
    public int Id { get; set; }
    public int Index { get; set; }
    public int DisplayId { get; internal set; }
    public string? ~Entity~Guid {get; internal set; }
    public bool IsLinked {get; set; }
    public bool IsAvailable {get; set; }
    public string Parent {get; set; } = "<None>";
    public string Child {get; set; }

	public ~Entity~(string? name, int id, string guid = "", string parent = "<None>", string child = "<None>")
	{
		Name = name;
		Id = id;
		DisplayId = Id;
		if (guid != null && guid.Length > 0)
		{
			~Entity~Guid = guid;
		}
		else
		{
			~Entity~Guid = Guid.NewGuid().ToString();
		}
		Parent = parent;
		Child = child;
	}
    public void SetDisplayId(int displayId) { DisplayId = displayId; }
    public override string ToString() => $"{DisplayId}) {Name}";
}
