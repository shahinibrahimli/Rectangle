using Rectangle.Domain.Contracts;

namespace Rectangle.Persistence;

public class UnitOfWorkInstanceBuilder : IUnitOfWorkInstanceBuilder
{
    private readonly IAuditContextFactory _dbContextFactory;

    readonly object _unitOfWOrkLock = new();

    public UnitOfWorkInstanceBuilder(        
        IAuditContextFactory dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }
    public IUnitOfWork BuildNewUnitOfWork()
    {
        lock (_unitOfWOrkLock)
        {
            var (dbContext, auditContext) = _dbContextFactory.GetAuditContextFactory();
            return new UnitOfWork(dbContext,auditContext);
        }
    }
}