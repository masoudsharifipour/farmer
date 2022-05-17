using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Farmer.Modern.Migrations
{
    public partial class UpdateAgentId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Agent",
                table: "Work",
                newName: "AgentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AgentId",
                table: "Work",
                newName: "Agent");
        }
    }
}
