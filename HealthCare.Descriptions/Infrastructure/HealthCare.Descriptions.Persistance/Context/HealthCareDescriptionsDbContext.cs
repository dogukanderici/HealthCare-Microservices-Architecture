using HealthCare.Descriptions.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Persistance.Context
{
    public class HealthCareDescriptionsDbContext : DbContext
    {
        public HealthCareDescriptionsDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>()
                .HasAlternateKey(c => c.Plate);

            modelBuilder.Entity<District>()
                .HasOne(d => d.City)
                .WithMany(d => d.Districts)
                .HasForeignKey(d => d.Plate)
                .HasPrincipalKey(d => d.Plate);

            modelBuilder.Entity<Hospital>()
                .HasOne(h => h.City)
                .WithMany(h => h.Hospitals)
                .HasForeignKey(h => h.HospitalCity)
                .HasPrincipalKey(h => h.Plate);

            modelBuilder.Entity<Hospital>()
                  .HasOne(h => h.District)
                  .WithMany(h => h.Hospitals)
                  .HasForeignKey(h => h.HospitalDistrict)
                  .HasPrincipalKey(d => d.Id);

            modelBuilder.Entity<HospitalPoliclinicQuota>()
                .HasOne(hpq => hpq.HospitalPoliclinic)
                .WithMany(hp => hp.HospitalPoliclinicQuotas)
                .HasForeignKey(hpq => hpq.HospitalPoliclinicId)
                .HasPrincipalKey(hp => hp.Id);
        }

        public DbSet<AppointmentStatus> AppointmentStatuses { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Hospital> Hospitals { get; set; }
        public DbSet<HospitalPoliclinic> HospitalPoliclinics { get; set; }
        public DbSet<HospitalPoliclinicQuota> HospitalPoliclinicQuotas { get; set; }
        public DbSet<Policlinic> Policlinics { get; set; }
        public DbSet<QuotaType> QuotaTypes { get; set; }
        public DbSet<ServiceType> ServiceTypes { get; set; }
        public DbSet<UserModifiedEvent> UserModifiedEvents { get; set; }
    }
}
