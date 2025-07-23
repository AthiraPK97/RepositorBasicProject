using RepositoryUoWExample.Models;

namespace RepositoryUoWExample.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Appointment> Appointments { get; }
        int Complete(); // Save all changes
    }

}
