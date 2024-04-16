using Microsoft.EntityFrameworkCore.Migrations;

namespace WindowsServiceXML.Migrations
{
    public partial class cc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CountryInfo",
                columns: table => new
                {
                    sISOCode = table.Column<string>(nullable: false),
                    sName = table.Column<string>(nullable: true),
                    sCapitalCity = table.Column<string>(nullable: true),
                    sPhoneCode = table.Column<string>(nullable: true),
                    sContinentCode = table.Column<string>(nullable: true),
                    sCurrencyISOCode = table.Column<string>(nullable: true),
                    sCountryFlag = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryInfo", x => x.sISOCode);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CountryInfo");
        }
    }
}
