using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class Updat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                schema: "banks",
                name: "Integrator",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false).Annotation("SqlServer:Identity", "1, 1"),

                    Name = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    Host = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    Port = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),

                    CreateDate = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2(7)", nullable: true),

                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Integrator", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                schema: "banks",
                name: "Integrator");
        }
    }
}
