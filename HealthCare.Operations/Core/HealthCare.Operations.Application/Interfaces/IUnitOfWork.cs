using HealthCare.Operations.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Operations.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public IRepository<Bill> BillRepository { get; }
        public IRepository<BillingService> BillingServiceRepository { get; }

        Task<int> CommitAsync();

    }
}
