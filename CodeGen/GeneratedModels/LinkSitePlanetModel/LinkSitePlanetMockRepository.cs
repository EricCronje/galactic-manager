// Generated Code - Version: 23.11.25 - 2024/11/20 17:29:25 - {64b62547-252c-4706-a962-7f21c734af25}
using ArchCorpUtilities.Models;
using AL = ArchCorpUtilities.Models.ArchLoader;

namespace ArchCorpUtilities.GeneratedModels.LinkSitePlanetModel
    {
        public class LinkSitePlanetMockRepository<T> : IRepository<LinkSitePlanet>, IDisposable
        {
            private readonly List<LinkSitePlanet>? Items;
            string PostFix { get; set; }

            public LinkSitePlanetMockRepository(string postFix)
            {
                PostFix = postFix;
                Items = [];
            }

            public void Add(LinkSitePlanet? entity)
            {
                if (Items != null && entity != null) { Items?.Add(entity); }
            }

            public IEnumerable<LinkSitePlanet>? All()
            {
            var PlanetsItems = AL.PlanetsHelper?.Items;
            var SitesItems = AL.SitesHelper?.Items;
            if (PlanetsItems != null && SitesItems != null)
                for (int i = 0; i < PlanetsItems.Count; i++)
                {
                    PlanetsModel.Planets? Planets = PlanetsItems[i];
                    SitesModel.Sites? Sites = SitesItems[i];
                    Items?.Add(new LinkSitePlanet($"{Sites.Name}-{Planets.Name}", 0, Sites.SitesGuid, Planets.PlanetsGuid));
                }
                return Items;
            }

            public void Dispose()
            {
                Items?.Clear();
                GC.SuppressFinalize(this);
            }

            public IEnumerable<LinkSitePlanet>? GetByGUID(string guid)
            {
                if (Items == null) { return null; }
                return Items.Where(p => p.LinkSitePlanetGuid != null && p.LinkSitePlanetGuid == guid);
            }

            public IEnumerable<LinkSitePlanet>? GetById(int id)
            {
                if (Items == null) { return null; }
                return Items.Where(p => p.Id == id);
            }

            public IEnumerable<LinkSitePlanet>? GetByName(string name)
            {
                if (Items == null) { return null; }
                return Items.Where(p => p.Name == name);
            }

            public bool Remove(LinkSitePlanet? entity)
            {
                if (Items != null && entity != null) { return Items.Remove(entity); }
                return false;
            }

            public int Count()
            {
                return Items == null ? 0 : Items.Count;
            }

            public List<LinkSitePlanet>? OrderByIndex()
            {
                return (List<LinkSitePlanet>?)Items?.OrderBy(p => p.Index);
            }
        }
    }
