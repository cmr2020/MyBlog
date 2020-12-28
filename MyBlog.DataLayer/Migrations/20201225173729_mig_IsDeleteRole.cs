using Microsoft.EntityFrameworkCore.Migrations;

namespace MyBlog.DataLayer.Migrations
{
    public partial class mig_IsDeleteRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Roles");
        }
    }
}
