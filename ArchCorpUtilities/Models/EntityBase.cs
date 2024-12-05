namespace ArchCorpUtilities.Models
{
    public class EntityBase : IEntityBase
    {
        public int DisplayId { get; internal set; }
        public string? Guid_ { get; set; }
        public int Id { get; set; }
        public int Index { get; set; }
        public bool IsLinked { get; set; } = false;
        public string? Name { get; set; }

        public EntityBase(string? name, int id, string guid = "")
        {
            DisplayId = id;
            Guid_ = guid != null && guid.Length > 0 ? guid : Guid.NewGuid().ToString();
            Id = id;
            Index = id;
            Name = name;
        }

        public EntityBase()
        {

        }
        public void SetDisplayId(int displayId) { DisplayId = displayId; }
        public override string ToString() => $"{DisplayId}) {Name}";
    }
}