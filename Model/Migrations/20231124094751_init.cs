using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Model.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "USERS",
                columns: table => new
                {
                    USER_ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    USERNAME = table.Column<string>(type: "text", nullable: false),
                    FIRST_NAME = table.Column<string>(type: "text", nullable: false),
                    LAST_NAME = table.Column<string>(type: "text", nullable: false),
                    PASSWORD_HASH = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USERS", x => x.USER_ID);
                });

            migrationBuilder.CreateTable(
                name: "USER_FOLLOWS_USERS_JT",
                columns: table => new
                {
                    Slave = table.Column<int>(type: "integer", nullable: false),
                    MASTER = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER_FOLLOWS_USERS_JT", x => new { x.MASTER, x.Slave });
                    table.ForeignKey(
                        name: "FK_USER_FOLLOWS_USERS_JT_USERS_MASTER",
                        column: x => x.MASTER,
                        principalTable: "USERS",
                        principalColumn: "USER_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_USER_FOLLOWS_USERS_JT_USERS_Slave",
                        column: x => x.Slave,
                        principalTable: "USERS",
                        principalColumn: "USER_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "YEETS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BODY = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YEETS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_YEETS_USERS_UserId",
                        column: x => x.UserId,
                        principalTable: "USERS",
                        principalColumn: "USER_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "USER_LIKES_YEETS_JT",
                columns: table => new
                {
                    USER_ID = table.Column<int>(type: "integer", nullable: false),
                    YEET_ID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER_LIKES_YEETS_JT", x => new { x.YEET_ID, x.USER_ID });
                    table.ForeignKey(
                        name: "FK_USER_LIKES_YEETS_JT_USERS_USER_ID",
                        column: x => x.USER_ID,
                        principalTable: "USERS",
                        principalColumn: "USER_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_USER_LIKES_YEETS_JT_YEETS_YEET_ID",
                        column: x => x.YEET_ID,
                        principalTable: "YEETS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_USER_FOLLOWS_USERS_JT_Slave",
                table: "USER_FOLLOWS_USERS_JT",
                column: "Slave");

            migrationBuilder.CreateIndex(
                name: "IX_USER_LIKES_YEETS_JT_USER_ID",
                table: "USER_LIKES_YEETS_JT",
                column: "USER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_YEETS_UserId",
                table: "YEETS",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "USER_FOLLOWS_USERS_JT");

            migrationBuilder.DropTable(
                name: "USER_LIKES_YEETS_JT");

            migrationBuilder.DropTable(
                name: "YEETS");

            migrationBuilder.DropTable(
                name: "USERS");
        }
    }
}
