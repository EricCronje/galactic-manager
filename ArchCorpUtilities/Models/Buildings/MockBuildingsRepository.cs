using BH = ArchCorpUtilities.Models.Buildings.BuildingHelper;

namespace ArchCorpUtilities.Models.Buildings
{
    public class MockBuildingsRepository : IRepository<Building>
    {
        public bool Add(Building? entity)
        {
            if (entity == null || BH.Buildings == null) return false;
            BH.Buildings.Add(entity);
            return GetByGUID(entity.BuildingGuid)?.Count() > 0;
        }

        public IEnumerable<Building>? All()
        {
            BH.Buildings ??= [];

            if (BH.Buildings != null && BH.Buildings.Count == 0)
            {
                BH.Buildings.Add(new("Alpha", 1, "1CBD2853-DFD4-47E6-8C3B-0104D9CAC581"));
                BH.Buildings.Add(new("Beta", 2, "C4EDB927-0CA3-464E-AFDC-929D5D8E2AD5"));
                BH.Buildings.Add(new("Dragon One", 3, "BE7BD93A-78C7-4511-8835-AF2B26B957B2"));
                return BH.Buildings?.OrderBy(c => c.Name);
            }
            else
                return BH.Buildings?.OrderBy(c => c.Name);
        }

        public IEnumerable<Building>? GetByGUID(string guid)
        {
            return All()?.Where(c => c.BuildingGuid.Equals(guid));
        }

        public IEnumerable<Building>? GetById(int id)
        {
            return All()?.Where(c => c.Id.Equals(id));
        }

        public IEnumerable<Building>? GetByName(string name)
        {
            return All()?.Where(c => c.Name != null && c.Name.ToUpper().Equals(name.ToUpper()));
        }

        public int? GetMaxId()
        {
            return All()?.Max(c => c.Id);
        }

        public bool Remove(Building? entity)
        {
            if (entity == null || BH.Buildings == null) return false;
            return BH.Buildings.Remove(entity);
        }

    }
}
