using ArchCorpUtilities.Models.Buildings;
using BH = ArchCorpUtilities.Models.Buildings.BuildingHelper;

namespace ArchCorpUtilities.Models
{
    public class MockBuildingsRepository : IBuildingsRepository
    {
        public IEnumerable<Building>? AllBuildings()
        {
            if (BH.Buildings != null)
            {
                if (BH.Buildings.Count == 0)
                {
                    BH.Buildings.Add(new("Alpha", 1));
                    BH.Buildings.Add(new("Beta", 2));
                    BH.Buildings.Add(new("Dragon One", 3));
                }
                return BH.Buildings;
            }
            return null;
        }

        public IEnumerable<Building>? GetBuildingById(int id)
        {
            return AllBuildings()?.Where(c => c.Id.Equals(id));
        }

        public IEnumerable<Building>? GetBuildingByName(string name)
        {
            return AllBuildings()?.Where(c => c.Equals(name));
        }

        public int? GetMaxId()
        {
            return AllBuildings()?.Max(c => c.Id);
        }
    }
}
