using Microsoft.EntityFrameworkCore.Migrations;

namespace Project.Web.Migrations
{
    public partial class AddedSubjectModelsMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Subject_Name",
                table: "Entity",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Subject_Name",
                table: "Entity");
        }
    }
}
