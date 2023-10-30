using Rectangle.Domain.Contracts;
using Rectangle.Domain.Entities;
using System.Threading.Tasks;

namespace Rectangle.Persistence.Repositories
{
    public class RectangleRepository: BaseRepository<ShapeRectangle>, IRectangleRepository
    {
        public RectangleRepository(RectangleDbContext dbContext, IAuditContext auditContext)
            : base(dbContext, auditContext)
        {
        }
    }
}
