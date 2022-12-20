using Microsoft.EntityFrameworkCore;
using Repository.SQLDatabaseContext;

namespace Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly SQLDbContext _sqlDbContext;

        public Repository(SQLDbContext sqlDbContext)
        {
            _sqlDbContext = sqlDbContext;
        }

        public IQueryable<T> GetAll()
        {
            return _sqlDbContext.Set<T>().AsNoTracking();
        }

        public async Task<T> GetById(int Id)
        {
            return await GetAll().FirstOrDefaultAsync(x => x.Equals(Id));
        }

        public async Task<bool> AddAsync(T entity)
        {
            await _sqlDbContext.AddAsync(entity);
            var affectedRows = await _sqlDbContext.SaveChangesAsync();
            return affectedRows > 0;
        }

        public async Task<bool> AddBulkAsync(IEnumerable<T> entity)
        {
            try
            {
                await _sqlDbContext.AddRangeAsync(entity);
                await _sqlDbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
