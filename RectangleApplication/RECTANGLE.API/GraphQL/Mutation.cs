using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Drawing;
using HotChocolate;
using HotChocolate.Authorization;
using Rectangle.Domain.Entities;
using Rectangle = Rectangle.Domain.Entities.ShapeRectangle;
using Rectangle.DomainServices.Contracts.RectangleServices;

namespace Rectangle.API.GraphQL
{
    public class Mutation
    {

        /// <summary>
        /// Inserts new rectangle 
        /// </summary>
        /// <param name="service">Rectangle service.</param>
        /// <param name="model">Rectangle model fields.</param>
        /// <returns>Rectangle.</returns>
        public async Task<ShapeRectangle> CreateREctangle([Service] IRectangleServices rectangleService, ShapeRectangle model)
        {
            return await rectangleService.AddRectangle(model.Width, model.Length);
        }

        /// <summary>
        /// Inserts list of rectangle 
        /// </summary>
        /// <param name="service">Rectangle service.</param>
        /// <param name="model">List Rectangle model fields.</param>
        /// <returns>Rectangle.</returns>
        public async Task<List<ShapeRectangle>> CreateBulkRectangle([Service] IRectangleServices rectangleService, List<ShapeRectangle> listRectangle)
        {
            return await rectangleService.AddBulkRectangle(listRectangle);
        }

        /// <summary>
        /// update exist rectangle info
        /// </summary>
        /// <param name="service">Rectangle service.</param>
        /// <param name="model">Rectangle model fields.</param>
        /// <returns>Rectangle.</returns>
        public async Task<ShapeRectangle> EditRectangle([Service] IRectangleServices rectangleService, ShapeRectangle model)
        {
            return await rectangleService.EditRectangle(model);
        }

    }
}