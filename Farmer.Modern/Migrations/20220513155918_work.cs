using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Farmer.Modern.Migrations
{
    public partial class work : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Harvest_Garden_GardenId",
                table: "Harvest");

            migrationBuilder.DropForeignKey(
                name: "FK_Harvest_Product_ProductId",
                table: "Harvest");

            migrationBuilder.AlterColumn<long>(
                name: "ProductId",
                table: "Harvest",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "GardenId",
                table: "Harvest",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Work",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GardenId = table.Column<long>(type: "bigint", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    CategoryId = table.Column<long>(type: "bigint", nullable: false),
                    WaterMotorId = table.Column<long>(type: "bigint", nullable: true),
                    ActionDatetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Size = table.Column<int>(type: "int", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatorUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Agent = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EndActionDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Work", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Work_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Work_Garden_GardenId",
                        column: x => x.GardenId,
                        principalTable: "Garden",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Work_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Work_WaterMotor_WaterMotorId",
                        column: x => x.WaterMotorId,
                        principalTable: "WaterMotor",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Work_CategoryId",
                table: "Work",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Work_GardenId",
                table: "Work",
                column: "GardenId");

            migrationBuilder.CreateIndex(
                name: "IX_Work_ProductId",
                table: "Work",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Work_WaterMotorId",
                table: "Work",
                column: "WaterMotorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Harvest_Garden_GardenId",
                table: "Harvest",
                column: "GardenId",
                principalTable: "Garden",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Harvest_Product_ProductId",
                table: "Harvest",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Harvest_Garden_GardenId",
                table: "Harvest");

            migrationBuilder.DropForeignKey(
                name: "FK_Harvest_Product_ProductId",
                table: "Harvest");

            migrationBuilder.DropTable(
                name: "Work");

            migrationBuilder.AlterColumn<long>(
                name: "ProductId",
                table: "Harvest",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "GardenId",
                table: "Harvest",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_Harvest_Garden_GardenId",
                table: "Harvest",
                column: "GardenId",
                principalTable: "Garden",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Harvest_Product_ProductId",
                table: "Harvest",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id");
        }
    }
}
