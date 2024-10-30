namespace ArchCorpUtilities.Models.Buildings
{
    public class Building
    {
        public string? Name { get; }
        public int Id { get; set; }
        public int DisplayId { get; internal set; }

        public Building(string? buildingName, int id)
        {
            Name = buildingName;
            Id = id;
            DisplayId = Id;
        }

        public void SetDisplayId(int displayId)
        {
            DisplayId = displayId;
        }

    }
}