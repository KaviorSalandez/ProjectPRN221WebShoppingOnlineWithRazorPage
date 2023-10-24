using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectPRN221WebShoppingOnlineWithRazorPage.Migrations
{
    public partial class updateCate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "aaa",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "aaa",
                table: "Categories");
        }
    }
}
