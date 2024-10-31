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
                    BH.Buildings.Add(new("Alpha", 1, "1CBD2853-DFD4-47E6-8C3B-0104D9CAC581"));
                    BH.Buildings.Add(new("Beta", 2,  "C4EDB927-0CA3-464E-AFDC-929D5D8E2AD5"));
                    BH.Buildings.Add(new("Dragon One", 3, "BE7BD93A-78C7-4511-8835-AF2B26B957B2"));
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
