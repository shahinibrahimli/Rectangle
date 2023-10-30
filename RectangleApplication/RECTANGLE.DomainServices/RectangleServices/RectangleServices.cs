using System.Collections.Generic;
using System.Text.Json.Nodes;
using DocumentFormat.OpenXml.Office2010.Excel;
using Rectangle.Domain.Entities;
using Rectangle.DomainServices.Contracts.RectangleServices;
using Rectangle.Persistence;

namespace Rectangle.DomainServices.StepServices;

public class RectangleServices : IRectangleServices
{
    private readonly IAuditContextFactory _dbContextFactory;
    public RectangleServices(IAuditContextFactory dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }
    public async Task<ShapeRectangle> AddRectangle(decimal width, decimal length)
    {
        var (dbContext, _) = await _dbContextFactory.GetAuditContextFactoryAsync();

        var newRectangle = new ShapeRectangle() { Length = length, Width = width };
        dbContext.Rectangles.Add(newRectangle);
        dbContext.SaveChanges();

        return newRectangle;
    }

    public async Task<List<ShapeRectangle>> AddBulkRectangle(List<ShapeRectangle> listRectangle)
    {
        var (dbContext, _) = await _dbContextFactory.GetAuditContextFactoryAsync();
        List <ShapeRectangle> newListRectangles = new List<ShapeRectangle>();

        foreach (var rect in listRectangle)
        {
            var newRectangle = new ShapeRectangle() { Length = rect.Length, Width = rect.Width };
            newListRectangles.Add(newRectangle);
        }

        dbContext.Rectangles.AddRange(newListRectangles);
        dbContext.SaveChanges();

        return newListRectangles;
    }

    
    public async Task<ShapeRectangle> EditRectangle(ShapeRectangle model)
    {
        var (dbContext, _) = await _dbContextFactory.GetAuditContextFactoryAsync();

        var currentRectangle = dbContext.Rectangles.FirstOrDefault(x=>x.Id == model.Id);
        currentRectangle.Width = model.Width;
        currentRectangle.Length = model.Length;

        dbContext.Update(currentRectangle);

        return currentRectangle;
    } 

    public async Task<List<ShapeRectangle>> GetAllRectangles()
    {
        var (dbContext, _) = await _dbContextFactory.GetAuditContextFactoryAsync();
        return dbContext.Rectangles.ToList();
    }

    public async Task<ShapeRectangle> GetRectangleWithID(int id)
    {
        var (dbContext, _) = await _dbContextFactory.GetAuditContextFactoryAsync();
        var currentRectangle = dbContext.Rectangles.FirstOrDefault(x => x.Id == id);
        return currentRectangle;
    }

    public async Task<List<ShapeRectangle>> GetRectangleWithWidth(decimal width)
    {
        var (dbContext, _) = await _dbContextFactory.GetAuditContextFactoryAsync();
        return dbContext.Rectangles.Where(x => x.Width == width).ToList();
    }

    public async Task<List<ShapeRectangle>> GetRectangleWithLength(decimal length)
    {
        var (dbContext, _) = await _dbContextFactory.GetAuditContextFactoryAsync();
        return dbContext.Rectangles.Where(x => x.Length == length).ToList();
         
    }
}