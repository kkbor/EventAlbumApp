using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventAlbumApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Qr",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Token = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Qr", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    names = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    surname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    password_hash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Album",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idQr = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    names = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    startEvent = table.Column<DateTime>(type: "datetime2", nullable: false),
                    endEvent = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    idQr1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    idUser1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Album", x => x.id);
                    table.ForeignKey(
                        name: "FK_Album_Qr_idQr",
                        column: x => x.idQr,
                        principalTable: "Qr",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Album_Users_idUser",
                        column: x => x.idUser,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idAlbum = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    datas = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    idAlbum1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.id);
                    table.ForeignKey(
                        name: "FK_Photos_Album_idAlbum",
                        column: x => x.idAlbum,
                        principalTable: "Album",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Album_idQr",
                table: "Album",
                column: "idQr",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Album_idUser",
                table: "Album",
                column: "idUser");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_idAlbum",
                table: "Photos",
                column: "idAlbum");

            migrationBuilder.CreateIndex(
                name: "IX_Qr_Token",
                table: "Qr",
                column: "Token",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_email",
                table: "Users",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropTable(
                name: "Album");

            migrationBuilder.DropTable(
                name: "Qr");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
