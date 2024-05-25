using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using web_store_server.Domain.Entities.Interfaces;

namespace web_store_server.Domain.Interceptors
{
    public class AuditableEntitiesInterceptor : SaveChangesInterceptor
    {
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData, 
            InterceptionResult<int> result, 
            CancellationToken cancellationToken = default)
        {
            if (eventData.Context is not null)
            {
                Console.WriteLine(eventData.Context.ChangeTracker.DebugView.LongView);

                var entities = eventData.Context.ChangeTracker.Entries<IAuditable>().ToList();

                foreach (EntityEntry<IAuditable> entry in entities)
                {
                    switch (entry.State)
                    {
                        case EntityState.Modified: // Auditoria Actualización
                            entry.Entity.UpdatedAt = DateTimeOffset.Now;
                            break;
                        case EntityState.Added: // Auditoria Creación
                            entry.Entity.CreatedAt = DateTimeOffset.Now;
                            break;
                        case EntityState.Deleted: // Estrategia Soft Delete
                            entry.State = EntityState.Modified;
                            entry.Entity.DeletedAt = DateTimeOffset.Now;
                            entry.Entity.IsDeleted = true;
                            break;
                    }
                }
            }
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}
