using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Farmer.Modern.Migrations
{
    public partial class Harvest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Experiment_Garden_GardenId",
                table: "Experiment");

            migrationBuilder.DropForeignKey(
                name: "FK_Experiment_WaterMotor_WaterMotorId",
                table: "Experiment");

            migrationBuilder.AlterColumn<long>(
                name: "WaterMotorId",
                table: "Experiment",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "GardenId",
                table: "Experiment",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Harvest",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<long>(type: "bigint", nullable: true),
                    GardenId = table.Column<long>(type: "bigint", nullable: true),
                    Size = table.Column<int>(type: "int", nullable: false),
                    HarvestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Harvest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Harvest_Garden_GardenId",
                        column: x => x.GardenId,
                        principalTable: "Garden",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Harvest_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Harvest_GardenId",
                table: "Harvest",
                column: "GardenId");

            migrationBuilder.CreateIndex(
                name: "IX_Harvest_ProductId",
                table: "Harvest",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Experiment_Garden_GardenId",
                table: "Experiment",
                column: "GardenId",
                principalTable: "Garden",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Experiment_WaterMotor_WaterMotorId",
                table: "Experiment",
                column: "WaterMotorId",
                principalTable: "WaterMotor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Experiment_Garden_GardenId",
                table: "Experiment");

            migrationBuilder.DropForeignKey(
                name: "FK_Experiment_WaterMotor_WaterMotorId",
                table: "Experiment");

            migrationBuilder.DropTable(
                name: "Harvest");

            migrationBuilder.AlterColumn<long>(
                name: "WaterMotorId",
                table: "Experiment",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "GardenId",
                table: "Experiment",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_Experiment_Garden_GardenId",
                table: "Experiment",
                column: "GardenId",
                principalTable: "Garden",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Experiment_WaterMotor_WaterMotorId",
                table: "Experiment",
                column: "WaterMotorId",
                principalTable: "WaterMotor",
                principalColumn: "Id");
        }
    }
}
