using Microsoft.EntityFrameworkCore;

using Medyana.Domain.Entities;

namespace Medyana.DataAccess.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Equipment>()
                         .HasOne(e => e.Clinic)
                         .WithMany(e => e.Equipments)
                         .HasForeignKey(e => e.ClinicId);
        }

        public DbSet<Clinic> Clinics { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
    }
}