using Microsoft.EntityFrameworkCore; 
using Rectangle.Domain.Entities;

namespace Rectangle.Persistence
{
    public sealed class RectangleDbContext : DbContext
    {
        public RectangleDbContext(DbContextOptions<RectangleDbContext> options)
            : base(options)
        {

        }
         
        public DbSet<ShapeRectangle> Rectangles { get; set; }     
         
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder); 
        }
    }
}