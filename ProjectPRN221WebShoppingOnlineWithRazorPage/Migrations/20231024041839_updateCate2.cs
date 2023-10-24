using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectPRN221WebShoppingOnlineWithRazorPage.Migrations
{
    public partial class updateCate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "aaa",
                table: "Categories");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "aaa",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
