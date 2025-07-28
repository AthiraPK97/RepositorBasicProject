using Microsoft.EntityFrameworkCore.Storage;
using RepositoryUoWExample.Data;
using RepositoryUoWExample.Layer1;
using RepositoryUoWExample.Models;

namespace RepositoryUoWExample.Layer2
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IRepository<Appointment> Appointments { get; private set; }
        public IRepository<Patient> Patients { get; private set; }


        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Appointments = new Repository<Appointment>(_context);
            Patients = new Repository<Patient>(_context);
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }
        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

}
