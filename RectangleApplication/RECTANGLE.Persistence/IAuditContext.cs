using System;
using System.Threading;
using System.Threading.Tasks;

namespace Rectangle.Persistence;

public interface IAuditContext
{
    public RectangleDbContext DbContext { get; }
    Task<int> LogAndSaveAsync(CancellationToken cancellationToken = default);
}