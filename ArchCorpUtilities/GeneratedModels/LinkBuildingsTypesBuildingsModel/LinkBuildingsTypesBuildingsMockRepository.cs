// Generated Code - Version: 23.11.25 - 2024/11/20 17:29:24 - {64b62547-252c-4706-a962-7f21c734af25}
using ArchCorpUtilities.Models;
using AL = ArchCorpUtilities.Models.ArchLoader;

namespace ArchCorpUtilities.GeneratedModels.LinkBuildingsTypesBuildingsModel
    {
        public class LinkBuildingsTypesBuildingsMockRepository<T> : IRepository<LinkBuildingsTypesBuildings>, IDisposable
        {
            private readonly List<LinkBuildingsTypesBuildings>? Items;
            string PostFix { get; set; }

            public LinkBuildingsTypesBuildingsMockRepository(string postFix)
            {
                PostFix = postFix;
                Items = [];
            }

            public void Add(LinkBuildingsTypesBuildings? entity)
            {
                if (Items != null && entity != null) { Items?.Add(entity); }
            }

            public IEnumerable<LinkBuildingsTypesBuildings>? All()
            {
            var BuildingsItems = AL.BuildingsHelper?.Items;
            var BuildingTypeItems = AL.BuildingTypeHelper?.Items;
            if (BuildingsItems != null && BuildingTypeItems != null)
                for (int i = 0; i < BuildingsItems.Count; i++)
                {
                    BuildingsModel.Buildings? Buildings = BuildingsItems[i];
                    BuildingTypeModel.BuildingType? BuildingType = BuildingTypeItems[i];
                    Items?.Add(new LinkBuildingsTypesBuildings($"{BuildingType.Name}-{Buildings.Name}", 0, BuildingType.BuildingTypeGuid, Buildings.BuildingsGuid));
                }
                return Items;
            }

            public void Dispose()
            {
                Items?.Clear();
                GC.SuppressFinalize(this);
            }

            public IEnumerable<LinkBuildingsTypesBuildings>? GetByGUID(string guid)
            {
                if (Items == null) { return null; }
                return Items.Where(p => p.LinkBuildingsTypesBuildingsGuid != null && p.LinkBuildingsTypesBuildingsGuid == guid);
            }

            public IEnumerable<LinkBuildingsTypesBuildings>? GetById(int id)
            {
                if (Items == null) { return null; }
                return Items.Where(p => p.Id == id);
            }

            public IEnumerable<LinkBuildingsTypesBuildings>? GetByName(string name)
            {
                if (Items == null) { return null; }
                return Items.Where(p => p.Name == name);
            }

            public bool Remove(LinkBuildingsTypesBuildings? entity)
            {
                if (Items != null && entity != null) { return Items.Remove(entity); }
                return false;
            }

            public int Count()
            {
                return Items == null ? 0 : Items.Count;
            }

            public List<LinkBuildingsTypesBuildings>? OrderByIndex()
            {
                return (List<LinkBuildingsTypesBuildings>?)Items?.OrderBy(p => p.Index);
            }
        }
    }
