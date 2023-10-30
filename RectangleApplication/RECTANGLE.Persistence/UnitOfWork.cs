using System;
using System.Threading.Tasks;
using Rectangle.Domain.Contracts;
using Rectangle.Domain.Entities;
using Rectangle.Persistence.Repositories;

namespace Rectangle.Persistence
{
    public sealed class UnitOfWork : IUnitOfWork
    { 
        private IRectangleRepository rectangleRepository; 

        public UnitOfWork(RectangleDbContext context)
        {
            Context = context;
        }

        public UnitOfWork(RectangleDbContext context, IAuditContext auditContext)
        {
            Context = context;
            AuditContext = auditContext;
        }

        public RectangleDbContext Context { get; private set; }
        public IAuditContext AuditContext { get; }

        public IRectangleRepository RectangleRepository => rectangleRepository ??= new RectangleRepository(Context, AuditContext);  

        public async Task SaveChangesAsync()
        {
            await AuditContext.LogAndSaveAsync();
        }

        public void Dispose()
        {
            
        }

    }
}
