using Microsoft.EntityFrameworkCore.Migrations;

namespace Bai14.Migrations
{
    public partial class v3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    MaKH = table.Column<string>(maxLength: 20, nullable: false),
                    HoTen = table.Column<string>(maxLength: 50, nullable: false),
                    DienThoai = table.Column<string>(maxLength: 50, nullable: true),
                    Email = table.Column<string>(maxLength: 150, nullable: true),
                    Discriminator = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.MaKH);
                });

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "Customers",
                maxLength: 250,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Note",
                table: "Customers");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
