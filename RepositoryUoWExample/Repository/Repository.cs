using RepositoryUoWExample.Data;

namespace RepositoryUoWExample.Repository
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

        public void Add(T entity) => _context.Set<T>().Add(entity);

        public void Remove(T entity) => _context.Set<T>().Remove(entity);
    }

}
