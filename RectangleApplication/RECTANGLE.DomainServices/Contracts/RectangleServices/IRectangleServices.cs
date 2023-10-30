using Rectangle.Domain.Entities;

namespace Rectangle.DomainServices.Contracts.RectangleServices;

public interface IRectangleServices
{
    Task<ShapeRectangle> AddRectangle(decimal width, decimal length);
    Task<List<ShapeRectangle>> AddBulkRectangle(List<ShapeRectangle> listRectangle);
    Task<ShapeRectangle> EditRectangle(ShapeRectangle model);
    Task<ShapeRectangle> GetRectangleWithID(int id);
    Task<List<ShapeRectangle>> GetRectangleWithWidth(decimal width);
    Task<List<ShapeRectangle>> GetRectangleWithLength(decimal length);
    Task<List<ShapeRectangle>> GetAllRectangles();
}