namespace ArchCorpUtilities.Models.Buildings
{
    public interface IRepository <T>
    {
        public IEnumerable<T>? All();
        public IEnumerable<T>? GetById(int id);
        public IEnumerable<T>? GetByGUID(string guid);
        public IEnumerable<T>? GetByName(string name);
        public int? GetMaxId();

        public bool Remove(T? entity);

        public bool Add(T? entity);

    }
}
