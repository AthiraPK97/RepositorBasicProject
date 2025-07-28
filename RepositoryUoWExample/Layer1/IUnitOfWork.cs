using Microsoft.EntityFrameworkCore.Storage;
using RepositoryUoWExample.Models;

namespace RepositoryUoWExample.Layer1
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Appointment> Appointments { get; }
        IRepository<Patient> Patients { get; }
        Task<int> Complete(); // Save all changes
        Task<IDbContextTransaction> BeginTransactionAsync();
    }

}
