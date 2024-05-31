using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Meally.Repository.Data.Migrations
{
    public partial class OrderModule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Subscriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PackgeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalPrice = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberOfDays = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationDateOrder = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShappingAddress_Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShappingAddress_City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShappingAddress_Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShappingAddress_Department = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShappingAddress_BuildingNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumOfMeals = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubscriptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Subscriptions_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalTable: "Subscriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Meal_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Meal_MealName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Meal_MealPictureUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Meal_MealComponents = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Meal_MealCalories = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Meal_MealType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_SubscriptionId",
                table: "Orders",
                column: "SubscriptionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Subscriptions");
        }
    }
}
