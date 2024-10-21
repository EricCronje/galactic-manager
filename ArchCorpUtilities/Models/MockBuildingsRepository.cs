using ArchCorpUtilities.Models.Buildings;
using AC = ArchCorpUtilities.Models.Buildings.BuildingHelper;

namespace ArchCorpUtilities.Models
{
    public class MockBuildingsRepository : IBuildingsRepository
    {
        public IEnumerable<Building>? AllBuildings()
        {
            if (AC.Buildings != null)
            {
                if (AC.Buildings.Count == 0)
                {
                    AC.Buildings.Add(new("Alpha", 1));
                    AC.Buildings.Add(new("Beta", 2));
                    AC.Buildings.Add(new("Dragon One", 3));
                }
                return AC.Buildings;
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
