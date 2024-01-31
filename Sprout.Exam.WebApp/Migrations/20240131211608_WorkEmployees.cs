using Microsoft.EntityFrameworkCore.Migrations;

namespace Sprout.Exam.WebApp.Migrations
{
    public partial class WorkEmployees : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorkEmployee",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Birthdate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tin = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkEmployee", x => x.Id);
                    table.UniqueConstraint("AK_WorkEmployee_Tin", x => x.Tin);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkEmployee");
        }
    }
}
