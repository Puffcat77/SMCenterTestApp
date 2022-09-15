using Microsoft.EntityFrameworkCore;

namespace SMCenterTestApp.DAL
{
    public partial class MedicDBContext : DbContext
    {
        public MedicDBContext()
        {
        }

        public MedicDBContext(DbContextOptions<MedicDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cabinet> Cabinets { get; set; } = null!;
        public virtual DbSet<Doctor> Doctors { get; set; } = null!;
        public virtual DbSet<Patient> Patients { get; set; } = null!;
        public virtual DbSet<Region> Regions { get; set; } = null!;
        public virtual DbSet<Speciality> Specialities { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=ConnectionStrings:MedicDB");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cabinet>(entity =>
            {
                entity.ToTable("Cabinet");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.ToTable("Doctor");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Initials).HasMaxLength(200);
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.ToTable("Patient");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Address).HasMaxLength(100);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.Surname).HasMaxLength(60);
            });

            modelBuilder.Entity<Region>(entity =>
            {
                entity.ToTable("Region");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<Speciality>(entity =>
            {
                entity.ToTable("Speciality");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(200);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}