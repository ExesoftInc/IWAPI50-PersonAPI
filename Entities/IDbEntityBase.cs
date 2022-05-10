using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace PersonAPI.Entities
{
    public interface IDbEntityBase : IDisposable
    {
        EntityEntry<T> Entry<T>(T entity) where T : class;

        Task<int> SaveChangesAsync();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}