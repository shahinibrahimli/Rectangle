using DocumentFormat.OpenXml.Drawing;
using FluentAssertions;
using Rectangle.DomainServices.StepServices;

namespace Rectangle.DomainServices.Tests.PhaseServices;

public class RectangleServicesTests : BaseDomainServiceTest
{
    [Fact]
    public async Task AddRectangle_WhenWidthOrLengthIsZero_ShouldFail()
    {
        // Arrange
        var moqAuditContext = GetIAuditContextFactoryMock(GetDbContextFactoryMock(CreateContextForSqLite()).Object).Object;
        var (dbContext, auditContext) = await moqAuditContext.GetAuditContextFactoryAsync();


        var newRec = new Domain.Entities.ShapeRectangle() { Width =  50 ,Length = 100};

        var rectangleServices = new RectangleServices(moqAuditContext);
        // Act 

        //// Assert 
    }

    [Fact]
    public async Task UpdateRectangle_WhenWidthOrLengthIsZero_ShouldFail()
    {
        // Arrange
        var moqAuditContext = GetIAuditContextFactoryMock(GetDbContextFactoryMock(CreateContextForSqLite()).Object).Object;
        var (dbContext, auditContext) = await moqAuditContext.GetAuditContextFactoryAsync();


        var newRec = new Domain.Entities.ShapeRectangle() { Width = 50, Length = 100 };

        var rectangleServices = new RectangleServices(moqAuditContext);
        // Act

        var act = () => rectangleServices.EditRectangle(newRec);

        // Assert  
        await act.Should().ThrowAsync<Exception>();

    }
}