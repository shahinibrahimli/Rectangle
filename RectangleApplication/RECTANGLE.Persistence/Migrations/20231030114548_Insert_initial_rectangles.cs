using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rectangle.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Insert_initial_rectangles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            for (int i = 1; i <= 200; i++)
            {
            migrationBuilder.Sql(
                                    $@"
                                    insert into [dbo].[Rectangles] ([Width] ,[Length]) values ({i},{i*2})
                                    ;");
            }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
