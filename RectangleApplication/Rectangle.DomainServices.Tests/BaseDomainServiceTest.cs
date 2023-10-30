using AutoBogus;
using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using Rectangle.Domain.Common;
using Rectangle.Domain.Entities;
using Rectangle.Persistence;

namespace Rectangle.DomainServices.Tests;

public abstract class BaseDomainServiceTest
{
    internal readonly Faker<ShapeRectangle> _createRectangleFaker;


    protected BaseDomainServiceTest()
    {
        _createRectangleFaker = new Faker<ShapeRectangle>();
    }
     
 
    protected Mock<IAuditContextFactory> GetIAuditContextFactoryMock(IDbContextFactory<RectangleDbContext> dbContextFactory)
    {
        return DataContextBuilder.GetIAuditContextFactoryMock(dbContextFactory);
    }
    protected Mock<IDbContextFactory<RectangleDbContext>> GetDbContextFactoryMock(RectangleDbContext dbContext)
    {
        return DataContextBuilder.GetDbContextFactoryMock(dbContext);
    }
    protected RectangleDbContext CreateContextForSqLite()
    {
        return DataContextBuilder.CreateContextForSqLite();
    } 
}