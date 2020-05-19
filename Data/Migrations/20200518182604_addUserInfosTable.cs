using Microsoft.EntityFrameworkCore.Migrations;

namespace SRS.Data.Migrations
{
    public partial class addUserInfosTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserInfos",
                columns: table => new
                {
                    Email = table.Column<string>(nullable: false),
                    FullName = table.Column<string>(maxLength: 256, nullable: true),
                    Phone = table.Column<string>(maxLength: 20, nullable: true),
                    Location = table.Column<string>(maxLength: 50, nullable: true),
                    BusinessArea = table.Column<string>(maxLength: 50, nullable: true),
                    TestFiled = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfos", x => x.Email);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserInfos");
        }
    }
}
