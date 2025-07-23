using Microsoft.EntityFrameworkCore;
using RepositoryUoWExample.Models;
using System.Collections.Generic;


namespace RepositoryUoWExample.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Appointment> Appointments { get; set; }
    }


}
