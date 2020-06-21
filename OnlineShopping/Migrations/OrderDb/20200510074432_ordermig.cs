using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineShopping.Migrations.OrderDb
{
    public partial class ordermig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ordersTb",
                columns: table => new
                {
                    OrderID = table.Column<string>(nullable: false),
                    OrderDate = table.Column<DateTime>(nullable: false),
                    TotalQunatity = table.Column<string>(nullable: true),
                    OrderAmount = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ordersTb", x => x.OrderID);
                });

            migrationBuilder.CreateTable(
                name: "orderDetailsTb",
                columns: table => new
                {
                    OrderId = table.Column<string>(nullable: false),
                    ProductID = table.Column<string>(nullable: false),
                    RequiredQunatity = table.Column<string>(nullable: true),
                    Amount = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orderDetailsTb", x => new { x.OrderId, x.ProductID });
                    table.ForeignKey(
                        name: "FK_orderDetailsTb_ordersTb_OrderId",
                        column: x => x.OrderId,
                        principalTable: "ordersTb",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "orderDetailsTb");

            migrationBuilder.DropTable(
                name: "ordersTb");
        }
    }
}
