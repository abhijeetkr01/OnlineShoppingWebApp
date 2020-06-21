using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineShopping.Migrations.ProductDb
{
    public partial class productmig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "productTb",
                columns: table => new
                {
                    ProductID = table.Column<string>(maxLength: 10, nullable: false),
                    ProductName = table.Column<string>(maxLength: 30, nullable: false),
                    UnitPrice = table.Column<string>(nullable: true),
                    Quantity = table.Column<string>(nullable: true),
                    PhotoPath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productTb", x => x.ProductID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "productTb");
        }
    }
}
