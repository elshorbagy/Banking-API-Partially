namespace Core.Interfaces
{
    public interface ICachedRepository<T> where T : class
    {
        IQueryable<T> GetAll(string cacheKey);
        Task<T> GetById(int Id);
        Task<bool> AddAsync(T entity);
        Task<bool> AddBulkAsync(IEnumerable<T> entity);
    }
}
