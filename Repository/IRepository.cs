namespace Repository
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        Task<T> GetById(int Id);
        Task<bool> AddAsync(T entity);
        Task<bool> AddBulkAsync(IEnumerable<T> entity);
    }
}
