namespace ArchCorpUtilities.Models
{
    public interface IEntityBase
    {
        int DisplayId { get; }
        string? Guid_ { get; set; }
        int Id { get; set; }
        int Index { get; set; }
        bool IsLinked { get; set; }
        string? Name { get; set; }

        void SetDisplayId(int displayId);
        string ToString();
    }
}