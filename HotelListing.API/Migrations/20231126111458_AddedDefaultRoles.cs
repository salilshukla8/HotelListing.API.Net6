using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelListing.API.Migrations
{
    public partial class AddedDefaultRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "929ead97-42e1-44e8-a606-4890de30fa34", "55df7360-1478-457e-b3bf-329b0eb04394", "Administrator", "ADMISNISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d6d0cfb3-4eea-4c6b-9c46-776b403fe2c8", "f1f14821-06d0-46a9-98c3-fce315ee5986", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "929ead97-42e1-44e8-a606-4890de30fa34");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d6d0cfb3-4eea-4c6b-9c46-776b403fe2c8");
        }
    }
}
