namespace DumAPI.Persistence.Services
{
    public interface IService<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T?> Get(int id);
        Task<int?> Add(T entity);
        Task<bool> Remove(int id);
        Task<T?> Update(T entity);
        Task<bool> Exists(string username);
    }
}
