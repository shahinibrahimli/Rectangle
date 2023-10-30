using System.Collections.Generic;
using System.Threading.Tasks;
using HotChocolate;
using Rectangle.Domain.Entities;
using Rectangle.DomainServices.Contracts.RectangleServices;

namespace Rectangle.API.GraphQL
{
    [GraphQLDescription("Represents the queries available.")]
    public class Query
    { 
        public async Task<ShapeRectangle> GetRectangleWithID([Service] IRectangleServices rectangleService, int id)
        {
            return await rectangleService.GetRectangleWithID(id);
        }


        public async Task<List<ShapeRectangle>> GetRectangleWithWidth([Service] IRectangleServices rectangleService, decimal width)
        {
            return await rectangleService.GetRectangleWithWidth(width);
        }


        public async Task<List<ShapeRectangle>> GetRectangleWithLength([Service] IRectangleServices rectangleService, decimal length)
        {
            return await rectangleService.GetRectangleWithLength(length);
        }



        /// <summary>
        /// Get all rectangles
        /// </summary>
        /// <param name="service">Rectangle service.</param>
        /// <returns>Rectangles.</returns>
        public async Task<List<ShapeRectangle>> GetAllRectangles([Service] IRectangleServices rectangleService)
        {
            return await rectangleService.GetAllRectangles();
        }

    }
}