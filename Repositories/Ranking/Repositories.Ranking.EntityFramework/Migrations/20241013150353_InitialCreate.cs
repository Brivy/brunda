using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Funda.Assigment.Repositories.RealEstateAgentRanker.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "ForSaleRanking",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RealEstateAgentName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ForSaleCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForSaleRanking", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ForSaleWithGardenRanking",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RealEstateAgentName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ForSaleCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForSaleWithGardenRanking", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ForSaleRanking",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ForSaleWithGardenRanking",
                schema: "dbo");
        }
    }
}
