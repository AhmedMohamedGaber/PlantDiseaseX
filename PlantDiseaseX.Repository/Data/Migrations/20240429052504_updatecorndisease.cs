using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlantDiseaseX.Repository.Data.Migrations
{
    public partial class updatecorndisease : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "corndiseases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    corndiseasepicture1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    corndiseasepicture2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    corndiseasepicture3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    appropriatetreatment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    reasons = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    symptoms = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    prevention = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    management = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    riskfactors = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    relateddiseases = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    researchreferences = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    additionalinfo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    diagnostictests = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    geographicdistribution = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    environmentalconditions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    hostplants = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pathogentype = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    economicimpact = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    controlmethods = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_corndiseases", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "corndiseases");
        }
    }
}
