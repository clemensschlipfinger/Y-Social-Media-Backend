using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Model.Migrations
{
    /// <inheritdoc />
    public partial class fixtagname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_yeet_has_tags_tag_tag_id",
                table: "yeet_has_tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tag",
                table: "tag");

            migrationBuilder.RenameTable(
                name: "tag",
                newName: "tags");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tags",
                table: "tags",
                column: "tag_id");

            migrationBuilder.AddForeignKey(
                name: "FK_yeet_has_tags_tags_tag_id",
                table: "yeet_has_tags",
                column: "tag_id",
                principalTable: "tags",
                principalColumn: "tag_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_yeet_has_tags_tags_tag_id",
                table: "yeet_has_tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tags",
                table: "tags");

            migrationBuilder.RenameTable(
                name: "tags",
                newName: "tag");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tag",
                table: "tag",
                column: "tag_id");

            migrationBuilder.AddForeignKey(
                name: "FK_yeet_has_tags_tag_tag_id",
                table: "yeet_has_tags",
                column: "tag_id",
                principalTable: "tag",
                principalColumn: "tag_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
