using RepositoryUoWExample.Data;
using RepositoryUoWExample.Models;

namespace RepositoryUoWExample.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IRepository<Appointment> Appointments { get; private set; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Appointments = new Repository<Appointment>(_context);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

}
