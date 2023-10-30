using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Rectangle.Persistence;

public class AuditContextFactory : IAuditContextFactory
{
    private readonly IDbContextFactory<RectangleDbContext> _dbContextFactory;

    public AuditContextFactory(IDbContextFactory<RectangleDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }
    
    public async Task<RectangleDbContext> GetDbContextOnlyFactoryAsync(CancellationToken cancellationToken = default)
    {
        return  await _dbContextFactory.CreateDbContextAsync(cancellationToken);
    }
    
    public async Task<(RectangleDbContext, IAuditContext)> GetAuditContextFactoryAsync(CancellationToken cancellationToken = default)
    {
        var db = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        var logContext = new AuditContext(db);
        return (db, logContext);
    }
    
    public (RectangleDbContext, IAuditContext) GetAuditContextFactory()
    {
        var db = _dbContextFactory.CreateDbContext();
        var logContext = new AuditContext(db);
        return (db, logContext);
    }
    
    public async Task<(RectangleDbContext, IAuditContext)> GetAuditContextFactoryForSystemUserAsync(CancellationToken cancellationToken = default)
    {
        var db = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        var logContext = new AuditContext(db);
        return (db, logContext);
    }
}