using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rectangle.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class add_owner_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CreatedById",
                table: "Rectangles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Rectangles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifiedById",
                table: "Rectangles",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "Rectangles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OwnedRectangleId",
                table: "Rectangles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OwnedRectangle",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OwnedRectangle", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rectangles_OwnedRectangleId",
                table: "Rectangles",
                column: "OwnedRectangleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rectangles_OwnedRectangle_OwnedRectangleId",
                table: "Rectangles",
                column: "OwnedRectangleId",
                principalTable: "OwnedRectangle",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rectangles_OwnedRectangle_OwnedRectangleId",
                table: "Rectangles");

            migrationBuilder.DropTable(
                name: "OwnedRectangle");

            migrationBuilder.DropIndex(
                name: "IX_Rectangles_OwnedRectangleId",
                table: "Rectangles");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Rectangles");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Rectangles");

            migrationBuilder.DropColumn(
                name: "LastModifiedById",
                table: "Rectangles");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "Rectangles");

            migrationBuilder.DropColumn(
                name: "OwnedRectangleId",
                table: "Rectangles");
        }
    }
}
