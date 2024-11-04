namespace ArchCorpUtilities.Models.Buildings
{
    public class Building
    {
        public string? Name { get; }
        public int Id { get; set; }
        public int DisplayId { get; internal set; }
        public string BuildingGuid { get; internal set; }

        public Building(string? buildingName, int id, string guid = "")
        {
            Name = buildingName;
            Id = id;
            DisplayId = Id;
            if (guid != null && guid.Length > 0)
            {
                BuildingGuid = guid;
            }
            else
            {
                BuildingGuid = Guid.NewGuid().ToString();
            }
        }

        public void SetDisplayId(int displayId)
        {
            DisplayId = displayId;
        }

        public override string ToString()
        {
            return $"{DisplayId}) {Name}";
        }

    }
}