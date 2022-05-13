using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Farmer.Modern.Migrations
{
    public partial class Experiment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Experiment",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GardenId = table.Column<long>(type: "bigint", nullable: true),
                    WaterMotorId = table.Column<long>(type: "bigint", nullable: true),
                    Result = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experiment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Experiment_Garden_GardenId",
                        column: x => x.GardenId,
                        principalTable: "Garden",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Experiment_WaterMotor_WaterMotorId",
                        column: x => x.WaterMotorId,
                        principalTable: "WaterMotor",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Experiment_GardenId",
                table: "Experiment",
                column: "GardenId");

            migrationBuilder.CreateIndex(
                name: "IX_Experiment_WaterMotorId",
                table: "Experiment",
                column: "WaterMotorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Experiment");
        }
    }
}
