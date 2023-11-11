using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectPRN221WebShoppingOnlineWithRazorPage.Migrations
{
    public partial class chinhsuaBangOrderThemTruongEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Orders");
        }
    }
}
