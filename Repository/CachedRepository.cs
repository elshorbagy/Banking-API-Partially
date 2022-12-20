using Core.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace Repository
{
    public class CachedRepository<T> : ICachedRepository<T> where T : class
    {
        readonly IRepository<T> _repository;
        readonly IMemoryCache _memoryCache;

        public CachedRepository(IRepository<T> repository, IMemoryCache memoryCache)
        {
            _repository = repository;
            _memoryCache = memoryCache;
        }

        public IQueryable<T> GetAll(string cacheKey)
        {
            var data = _memoryCache.GetOrCreate(
                    cacheKey,
                     entry =>
                     {
                         entry.SetAbsoluteExpiration(TimeSpan.FromDays(1));

                         return _repository.GetAll();
                     }
                    );

            return data;
        }

        public async Task<T> GetById(int Id)
        {
            var t = await _repository.GetById(Id);
            return t;
        }

        public async Task<bool> AddAsync(T entity)
        {
            return await _repository.AddAsync(entity);
        }

        public async Task<bool> AddBulkAsync(IEnumerable<T> entity)
        {
            return await _repository.AddBulkAsync(entity);
        }
    }
}
