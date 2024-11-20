// Generated Code - Version: 23.11.25 - 2024/11/20 17:29:25 - {64b62547-252c-4706-a962-7f21c734af25}
using ArchCorpUtilities.Models;
using AL = ArchCorpUtilities.Models.ArchLoader;

namespace ArchCorpUtilities.GeneratedModels.LinkPlanetTypesPlanetModel
    {
        public class LinkPlanetTypesPlanetMockRepository<T> : IRepository<LinkPlanetTypesPlanet>, IDisposable
        {
            private readonly List<LinkPlanetTypesPlanet>? Items;
            string PostFix { get; set; }

            public LinkPlanetTypesPlanetMockRepository(string postFix)
            {
                PostFix = postFix;
                Items = [];
            }

            public void Add(LinkPlanetTypesPlanet? entity)
            {
                if (Items != null && entity != null) { Items?.Add(entity); }
            }

            public IEnumerable<LinkPlanetTypesPlanet>? All()
            {
            var PlanetsItems = AL.PlanetsHelper?.Items;
            var PlanetTypeItems = AL.PlanetTypeHelper?.Items;
            if (PlanetsItems != null && PlanetTypeItems != null)
                for (int i = 0; i < PlanetsItems.Count; i++)
                {
                    PlanetsModel.Planets? Planets = PlanetsItems[i];
                    PlanetTypeModel.PlanetType? PlanetType = PlanetTypeItems[i];
                    Items?.Add(new LinkPlanetTypesPlanet($"{PlanetType.Name}-{Planets.Name}", 0, PlanetType.PlanetTypeGuid, Planets.PlanetsGuid));
                }
                return Items;
            }

            public void Dispose()
            {
                Items?.Clear();
                GC.SuppressFinalize(this);
            }

            public IEnumerable<LinkPlanetTypesPlanet>? GetByGUID(string guid)
            {
                if (Items == null) { return null; }
                return Items.Where(p => p.LinkPlanetTypesPlanetGuid != null && p.LinkPlanetTypesPlanetGuid == guid);
            }

            public IEnumerable<LinkPlanetTypesPlanet>? GetById(int id)
            {
                if (Items == null) { return null; }
                return Items.Where(p => p.Id == id);
            }

            public IEnumerable<LinkPlanetTypesPlanet>? GetByName(string name)
            {
                if (Items == null) { return null; }
                return Items.Where(p => p.Name == name);
            }

            public bool Remove(LinkPlanetTypesPlanet? entity)
            {
                if (Items != null && entity != null) { return Items.Remove(entity); }
                return false;
            }

            public int Count()
            {
                return Items == null ? 0 : Items.Count;
            }

            public List<LinkPlanetTypesPlanet>? OrderByIndex()
            {
                return (List<LinkPlanetTypesPlanet>?)Items?.OrderBy(p => p.Index);
            }
        }
    }
