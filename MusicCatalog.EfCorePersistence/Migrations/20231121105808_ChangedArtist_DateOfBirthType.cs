using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicCatalog.EfCorePersistance.Migrations
{
    /// <inheritdoc />
    public partial class ChangedArtist_DateOfBirthType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "Artists",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "Artists",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");
        }
    }
}
