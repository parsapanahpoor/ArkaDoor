using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArkaDoor.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddRoleSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreateDate", "IsDelete", "RoleUniqueName", "Title", "UpdateDate" },
                values: new object[] { 1m, new DateTime(2023, 12, 6, 15, 22, 57, 9, DateTimeKind.Local).AddTicks(2518), false, "Admin", "Admin", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1m);
        }
    }
}
