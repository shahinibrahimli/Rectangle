using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Rectangle.Domain.Common;

namespace Rectangle.Persistence;

public class AuditContext : IAuditContext
{
    public RectangleDbContext DbContext { get; }

    public AuditContext(RectangleDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public async Task<int> LogAndSaveAsync(CancellationToken cancellationToken = default)
    {
        UpdateUserFields();
       
        int result;

        try
        {
            result = await ((DbContext)DbContext).SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        } 
        await ((DbContext)DbContext).SaveChangesAsync(cancellationToken);
        return result;
    } 
    private void UpdateUserFields()
    {
        foreach (var entry in DbContext.ChangeTracker.Entries<AuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedDate = DateTime.UtcNow;
                    break;
                case EntityState.Modified:
                    entry.Entity.LastModifiedDate = DateTime.UtcNow;
                    break;
            }
        }
    }
      

}