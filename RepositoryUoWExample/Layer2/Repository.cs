using Microsoft.EntityFrameworkCore;
using RepositoryUoWExample.Data;
using RepositoryUoWExample.Layer1;

namespace RepositoryUoWExample.Layer2
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<T> GetAll() => _context.Set<T>().ToList();

        public T GetById(int id) => _context.Set<T>().Find(id);

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public void Remove(T entity) => _context.Set<T>().Remove(entity);
        public async Task<T> GetAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }
    }

}
