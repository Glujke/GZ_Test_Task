using GZ_Test_Repo.Entity;
using Microsoft.EntityFrameworkCore;

namespace GZ_Test_Repo.Context
{
    public class GZ_DbContext : DbContext
    {
        internal DbSet<Area> Areas { get; set; }
        internal DbSet<Specialization> Specializations { get; set; }
        internal DbSet<Cabinet> Cabinetes { get; set; }
        internal DbSet<Doctor> Doctors { get; set; }
        internal DbSet<Gender> Genders { get; set; }
        internal DbSet<Patient> Patients { get; set; }

        public GZ_DbContext(DbContextOptions<GZ_DbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>()
                 .HasOne(p => p.Gender)
                 .WithMany()
                 .HasForeignKey(p => p.GenderId);

            modelBuilder.Entity<Patient>()
                .HasOne(p => p.Area)
                .WithMany()
                .HasForeignKey(p => p.AreaId);

            modelBuilder.Entity<Doctor>()
                .HasOne(d => d.Cabinet)
                .WithMany()
                .HasForeignKey(d => d.CabinetId);

            modelBuilder.Entity<Doctor>()
                .HasOne(d => d.Specialization)
                .WithMany()
                .HasForeignKey(d => d.SpecializationId);

            modelBuilder.Entity<Doctor>()
                .HasOne(d => d.Area)
                .WithMany()
                .HasForeignKey(d => d.AreaId);
        }
    }
}
