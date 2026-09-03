using HealthCare.Descriptions.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Persistence.DBContexts
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        // Override SaveChangesAsync veri kaydı öncesi için ek işlemler yapılmasını sağlar ( ICurrentUserService kullanılacak. ).
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<AppointmentStatus> AppointmentStatuses { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Hospital> Hospitals { get; set; }
        public DbSet<Policlinic> Policlinics { get; set; }
        public DbSet<HospitalPoliclinic> HospitalPoliclinics { get; set; }
        public DbSet<QuotaType> QuotaTypes { get; set; }
        public DbSet<HospitalPoliclinicQuota> HospitalPoliclinicQuotas { get; set; }
        public DbSet<ServiceType> ServiceTypes { get; set; }
        public DbSet<HospitalService> HospitalServices { get; set; }
    }
}
