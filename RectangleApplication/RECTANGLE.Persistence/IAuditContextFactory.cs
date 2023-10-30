using System.Threading;
using System.Threading.Tasks;

namespace Rectangle.Persistence;

public interface IAuditContextFactory
{
    Task<(RectangleDbContext, IAuditContext)> GetAuditContextFactoryAsync(CancellationToken cancellationToken = default);
    Task<(RectangleDbContext, IAuditContext)> GetAuditContextFactoryForSystemUserAsync(CancellationToken cancellationToken = default);
    (RectangleDbContext, IAuditContext) GetAuditContextFactory();
    Task<RectangleDbContext> GetDbContextOnlyFactoryAsync(CancellationToken cancellationToken = default);
}