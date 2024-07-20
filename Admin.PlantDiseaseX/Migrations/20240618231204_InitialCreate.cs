using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Admin.PlantDiseaseX.Identity.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastSignInDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastSignInDate",
                table: "AspNetUsers");
        }
    }
}
