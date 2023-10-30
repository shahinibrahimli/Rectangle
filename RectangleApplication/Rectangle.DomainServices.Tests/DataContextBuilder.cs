using System.Security.Claims;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;
using Rectangle.Persistence;

namespace Rectangle.DomainServices.Tests;

internal static class DataContextBuilder
{
    internal static Mock<IDbContextFactory<RectangleDbContext>> GetDbContextFactoryMock(RectangleDbContext dbContext)
    {
        var mock = new Mock<IDbContextFactory<RectangleDbContext>>();
        mock.SetupAllProperties();
        mock.Setup(x => x.CreateDbContextAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(dbContext);
        mock.Setup(x => x.CreateDbContext())
            .Returns(dbContext);
        return mock;
    }
    
    internal static Mock<IAuditContextFactory> GetIAuditContextFactoryMock(IDbContextFactory<RectangleDbContext> dbContextFactory)
    {
        var mock = new Mock<IAuditContextFactory>();
        var dbContext = dbContextFactory.CreateDbContext();
        mock.SetupAllProperties();
        mock.Setup(x => x.GetAuditContextFactoryAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync((dbContext, new AuditContext(dbContext) ));
        mock.Setup(x => x.GetAuditContextFactory())
            .Returns((dbContext, new AuditContext(dbContext) ));
        mock.Setup(x => x.GetDbContextOnlyFactoryAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(dbContext);
        return mock;
    } 
    internal static RectangleDbContext CreateContextForSqLite()
    {
        var connection = new SqliteConnection($"Data Source={Guid.NewGuid():N};Mode=Memory;Cache=Shared");
        connection.Open();

        var option = new DbContextOptionsBuilder<RectangleDbContext>().UseSqlite(connection).Options;
        
        var context = new RectangleDbContext(option);
        context.Database.EnsureCreated();
        return context;
    }  
}