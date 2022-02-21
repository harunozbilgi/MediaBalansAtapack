using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediaBalans.Persistence.Migrations
{
    public partial class Initial_V3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: new Guid("08ad9d94-22c5-454c-a162-093a91ae998a"));

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: new Guid("a1a0791c-d52e-44cd-b64f-3a1a83802e03"));

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: new Guid("c164e73e-5bf6-4919-a8c1-2d477a6ca1dd"));

            migrationBuilder.CreateTable(
                name: "AppUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    FullName = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Role = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "CreateTime", "Email", "FullName", "IsActive", "Password", "Role" },
                values: new object[] { new Guid("1b5ede51-74b5-499e-abef-ca758cafdaf9"), new DateTime(2022, 2, 21, 15, 29, 24, 346, DateTimeKind.Local).AddTicks(4656), "admin@admin.com", "Admin", false, "Admin123", "Admin" });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Id", "CreateTime", "FullName", "IsActive", "LangCode", "ShortName" },
                values: new object[,]
                {
                    { new Guid("37856b91-6286-4b2d-a54e-b36455ac6b9d"), new DateTime(2022, 2, 21, 15, 29, 24, 345, DateTimeKind.Local).AddTicks(3339), "Ingilizce", true, "en", "en_US" },
                    { new Guid("556eac81-cc7f-4eb2-8311-9ad33b2c7bf4"), new DateTime(2022, 2, 21, 15, 29, 24, 345, DateTimeKind.Local).AddTicks(3341), "Rusca", true, "ru", "ru_RU" },
                    { new Guid("daead765-f1b0-491d-aa22-122e791166c1"), new DateTime(2022, 2, 21, 15, 29, 24, 345, DateTimeKind.Local).AddTicks(3314), "Azerbaycan", true, "az", "az_Az" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUsers");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: new Guid("37856b91-6286-4b2d-a54e-b36455ac6b9d"));

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: new Guid("556eac81-cc7f-4eb2-8311-9ad33b2c7bf4"));

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: new Guid("daead765-f1b0-491d-aa22-122e791166c1"));

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Id", "CreateTime", "FullName", "IsActive", "LangCode", "ShortName" },
                values: new object[] { new Guid("08ad9d94-22c5-454c-a162-093a91ae998a"), new DateTime(2022, 2, 21, 9, 51, 57, 242, DateTimeKind.Local).AddTicks(3298), "Azerbaycan", true, "az", "az_Az" });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Id", "CreateTime", "FullName", "IsActive", "LangCode", "ShortName" },
                values: new object[] { new Guid("a1a0791c-d52e-44cd-b64f-3a1a83802e03"), new DateTime(2022, 2, 21, 9, 51, 57, 242, DateTimeKind.Local).AddTicks(3356), "Rusca", true, "ru", "ru_RU" });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Id", "CreateTime", "FullName", "IsActive", "LangCode", "ShortName" },
                values: new object[] { new Guid("c164e73e-5bf6-4919-a8c1-2d477a6ca1dd"), new DateTime(2022, 2, 21, 9, 51, 57, 242, DateTimeKind.Local).AddTicks(3352), "Ingilizce", true, "en", "en_US" });
        }
    }
}
