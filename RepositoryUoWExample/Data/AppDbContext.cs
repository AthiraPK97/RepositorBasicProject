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
        public DbSet<Patient> Patients { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Patient)
                .WithMany(p => p.Appointments) // ← reverse navigation
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.Cascade); // Or Restrict or SetNull as needed
        }


    }


}
