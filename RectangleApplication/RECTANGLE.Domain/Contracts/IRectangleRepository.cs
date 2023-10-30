using Rectangle.Domain.Entities;

namespace Rectangle.Domain.Contracts
{
    public interface IRectangleRepository : IAsyncRepository<ShapeRectangle>
    {
    }
}
