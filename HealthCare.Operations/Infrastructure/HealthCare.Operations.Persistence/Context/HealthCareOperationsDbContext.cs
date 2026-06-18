using HealthCare.Operations.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Operations.Persistence.Context
{
    public class HealthCareOperationsDbContext : DbContext
    {
        public HealthCareOperationsDbContext(DbContextOptions options) : base(options)
        {
        }

        DbSet<Appointment> Appoinments { get; set; }
        DbSet<Bill> Bills { get; set; }
        DbSet<BillingService> BillingServices { get; set; }
    }
}
