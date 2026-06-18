using HealthCare.Operations.Application.Interfaces;
using HealthCare.Operations.Domain.Entities;
using HealthCare.Operations.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Operations.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HealthCareOperationsDbContext _context;
        public IRepository<Bill> BillRepository { get; private set; }
        public IRepository<BillingService> BillingServiceRepository { get; private set; }

        public UnitOfWork(HealthCareOperationsDbContext context)
        {
            _context = context;
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
