using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventAlbumApp.Migrations
{
    /// <inheritdoc />
    public partial class ChangePhotoToR2Storage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "datas",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "idAlbum1",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "idQr1",
                table: "Album");

            migrationBuilder.DropColumn(
                name: "idUser1",
                table: "Album");

            migrationBuilder.AddColumn<string>(
                name: "path",
                table: "Photos",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "path",
                table: "Photos");

            migrationBuilder.AddColumn<byte[]>(
                name: "datas",
                table: "Photos",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "idAlbum1",
                table: "Photos",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "idQr1",
                table: "Album",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "idUser1",
                table: "Album",
                type: "uniqueidentifier",
                nullable: true);
        }
    }
}
