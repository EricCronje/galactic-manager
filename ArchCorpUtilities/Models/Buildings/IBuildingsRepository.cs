namespace ArchCorpUtilities.Models.Buildings
{
    public interface IBuildingsRepository
    {
        public IEnumerable<Building>? AllBuildings();
        public IEnumerable<Building>? GetBuildingById(int id);
        public IEnumerable<Building>? GetBuildingByName(string name);
        public int? GetMaxId();
    }
}
