using HealthCareSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthCareSystem.Data;

public class HealthCareContext : DbContext
{
    public HealthCareContext() { }

    public HealthCareContext(DbContextOptions<HealthCareContext> options) : base(options) { }

    public DbSet<Patient> Patients => Set<Patient>();
    public DbSet<Doctor> Doctors => Set<Doctor>();
    public DbSet<Appointment> Appointments => Set<Appointment>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseSqlServer("Server=.;Database=HealthCareDb;Trusted_Connection=True;TrustServerCertificate=True");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Name).HasMaxLength(120).IsRequired();
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Name).HasMaxLength(120).IsRequired();
            entity.Property(x => x.Specialization).HasMaxLength(120).IsRequired();
        });

        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(x => new { x.PatientId, x.DoctorId, x.AppointmentDate });
            entity.HasOne(x => x.Patient)
                .WithMany(x => x.Appointments)
                .HasForeignKey(x => x.PatientId);
            entity.HasOne(x => x.Doctor)
                .WithMany(x => x.Appointments)
                .HasForeignKey(x => x.DoctorId);
        });
    }
}
