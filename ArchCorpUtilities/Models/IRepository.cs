namespace ArchCorpUtilities.Models;

public interface IRepository<T>
{
    public IEnumerable<T>? All();
    public IEnumerable<T>? GetById(int id);
    public IEnumerable<T>? GetByGUID(string guid);
    public IEnumerable<T>? GetByName(string name);
    public bool Remove(T? entity);
    public void Add(T? entity);
    public int Count();
    public List<T>? OrderByIndex();
    public bool ClearAll();
}
