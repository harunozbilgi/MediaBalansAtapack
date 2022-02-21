using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediaBalans.Persistence.Migrations
{
    public partial class Initial_V2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: new Guid("5ec2fdfc-c825-4762-844b-ecc9edb1ac1f"));

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: new Guid("6d2cb867-3612-4747-bc0f-b32d4ccb0b34"));

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: new Guid("74717530-fe6b-4724-bde5-b6642783b643"));

            migrationBuilder.DropColumn(
                name: "IsCover",
                table: "ProductFiles");

            migrationBuilder.AddColumn<string>(
                name: "AppSeoCode",
                table: "Pages",
                type: "varchar(15)",
                maxLength: 15,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "IsHome",
                table: "Pages",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "AppSeoCode",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "IsHome",
                table: "Pages");

            migrationBuilder.AddColumn<bool>(
                name: "IsCover",
                table: "ProductFiles",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Id", "CreateTime", "FullName", "IsActive", "LangCode", "ShortName" },
                values: new object[] { new Guid("5ec2fdfc-c825-4762-844b-ecc9edb1ac1f"), new DateTime(2022, 2, 16, 17, 7, 12, 985, DateTimeKind.Local).AddTicks(8304), "Ingilizce", true, "en", "en_US" });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Id", "CreateTime", "FullName", "IsActive", "LangCode", "ShortName" },
                values: new object[] { new Guid("6d2cb867-3612-4747-bc0f-b32d4ccb0b34"), new DateTime(2022, 2, 16, 17, 7, 12, 985, DateTimeKind.Local).AddTicks(8262), "Azerbaycan", true, "az", "az_Az" });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Id", "CreateTime", "FullName", "IsActive", "LangCode", "ShortName" },
                values: new object[] { new Guid("74717530-fe6b-4724-bde5-b6642783b643"), new DateTime(2022, 2, 16, 17, 7, 12, 985, DateTimeKind.Local).AddTicks(8306), "Rusca", true, "ru", "ru_RU" });
        }
    }
}
