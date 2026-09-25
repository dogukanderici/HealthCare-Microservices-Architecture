using HealthCare.Descriptions.Application.IntegrationServices.RabbitMQ;
using HealthCare.Descriptions.Application.Interfaces;
using HealthCare.Descriptions.Domain.Abstracts;
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
        private readonly ICurrentUserService _userService;
        public DBContext(DbContextOptions dbContextOptions, ICurrentUserService userService) : base(dbContextOptions)
        {
            _userService = userService;
        }

        // Override SaveChangesAsync veri kaydı öncesi için ek işlemler yapılmasını sağlar ( ICurrentUserService kullanılacak. ).
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            Guid userId = _userService.UserId;

            // ChangeTracker ile IAuditProperty sahip Entity class'ları bulur.
            foreach (var entry in ChangeTracker.Entries<IAuditProperty>())
            {
                DateTimeOffset currentDate = _userService.CurrentDate;
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = userId;
                        entry.Entity.CreatedAt = currentDate;
                        entry.Entity.UpdatedBy = userId;
                        entry.Entity.UpdatedAt = currentDate;
                        break;

                    case EntityState.Modified:
                        entry.Entity.UpdatedBy = userId;
                        entry.Entity.UpdatedAt = currentDate;

                        // Mevcut CreatedAt ve CreatedBy değerlerinin güncellenmesi engeller.
                        entry.Property(x => x.CreatedAt).IsModified = false;
                        entry.Property(x => x.CreatedBy).IsModified = false;
                        break;
                }
            }

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
        public DbSet<ServicingType> ServicingTypes { get; set; }
        public DbSet<HospitalService> HospitalServices { get; set; }

        // RabbitMQ ile IdentityServer'daki Kullanıcıları Tutar. (Master-Slave)
        public DbSet<SyncUserEvent> SyncUserEvents { get; set; }
    }
}
