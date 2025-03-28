﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Farmer.Modern.Migrations
{
    public partial class AddCreationDatetime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDatetime",
                table: "Work",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationDatetime",
                table: "Work");
        }
    }
}
