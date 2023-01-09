using System;
using Microsoft.EntityFrameworkCore.Migrations;
using InternetShop.Data.Context;

#nullable disable

namespace InternetShop.Migrations
{
    public partial class AddNullProductsSupplierRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SuperMarkets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuperMarkets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    UnitInStock = table.Column<double>(type: "float", nullable: false),
                    SupplierId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitsCount = table.Column<float>(type: "real", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Suppliers",
                columns: new[] { "Id", "CompanyName", "FirstName", "LastName", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "Magical Tea", "Gregory", "Mcmillan", "0512223192" },
                    { 2, "Awesome Tea", "Alice", "Scott", "057376629" },
                    { 3, "Futuristic Tea", "Tony", "Weiss", "057456629" },
                    { 4, "Creatively Tea", "Joseph", "Beard", "057345233" },
                    { 5, "My Tea", "Edward", "Ruiz", "0971523684" },
                    { 6, "Holy Tea", "Cecilia", "Conway", "0935412687" },
                    { 7, "Tea Side", "Herbie", "Haley", "0964512756" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "Price", "SupplierId", "UnitInStock" },
                values: new object[,]
                {
                    { 127, "Eceri Green Georgian Tea", 93.0, 2, 200.0 },
                    { 204, "Milk OLoong Tea", 111.84999999999999, 4, 50.0 },
                    { 206, "Sky blue Tegu Tea", 105.0, 6, 110.0 },
                    { 300, "English Breakfast Tea", 65.650000000000006, 3, 1000.0 },
                    { 502, "Masala Tea", 86.849999999999994, 7, 50.0 },
                    { 534, "Royal Berghamot Tea", 118.34999999999999, 6, 5.0 },
                    { 560, "Garnet Black Tea", 86.849999999999994, 1, 150.0 },
                    { 700, "Alpen Plains Tea", 86.849999999999994, 5, 10.0 },
                    { 715, "NonMatch Tea", 375.0, 2, 25.0 },
                    { 817, "Shen Puer Tea «Lapis Dragon»", 223.0, 1, 50.0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ProductId",
                table: "Orders",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SupplierId",
                table: "Products",
                column: "SupplierId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "SuperMarkets");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Suppliers");
        }
    }
}
