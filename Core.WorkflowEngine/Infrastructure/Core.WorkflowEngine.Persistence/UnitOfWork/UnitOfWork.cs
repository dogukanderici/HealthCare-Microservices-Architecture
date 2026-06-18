using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Persistence.Context;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DBContext _dbContext;
        private bool _disposed;
        private IDbContextTransaction _currentTransaction;

        public UnitOfWork(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task BeginTransactionAsync()
        {
            if (_currentTransaction != null)
            {
                return;
            }

            _currentTransaction = await _dbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_currentTransaction == null)
            {
                return;
            }

            await _currentTransaction.CommitAsync();
            await _currentTransaction.DisposeAsync();
            _currentTransaction = null;
        }

        public async Task RollbackTransactionAsync()
        {
            if (_currentTransaction == null)
            {
                return;
            }

            await _currentTransaction.RollbackAsync();
            await _currentTransaction.DisposeAsync();
            _currentTransaction = null;
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _dbContext.Dispose();
                _currentTransaction?.Dispose();
                _disposed = true;
            }

            GC.SuppressFinalize(this);
        }

        public async ValueTask DisposeAsync()
        {
            if (!_disposed)
            {
                await _dbContext.DisposeAsync();

                if (_currentTransaction != null)
                {
                    await _currentTransaction.DisposeAsync();
                }

                _disposed = true;
            }

            GC.SuppressFinalize(this);
        }
    }
}