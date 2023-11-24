using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Model.Migrations
{
    /// <inheritdoc />
    public partial class rename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_USER_FOLLOWS_USERS_JT_USERS_MASTER",
                table: "USER_FOLLOWS_USERS_JT");

            migrationBuilder.DropForeignKey(
                name: "FK_USER_FOLLOWS_USERS_JT_USERS_Slave",
                table: "USER_FOLLOWS_USERS_JT");

            migrationBuilder.DropForeignKey(
                name: "FK_USER_LIKES_YEETS_JT_USERS_USER_ID",
                table: "USER_LIKES_YEETS_JT");

            migrationBuilder.DropForeignKey(
                name: "FK_USER_LIKES_YEETS_JT_YEETS_YEET_ID",
                table: "USER_LIKES_YEETS_JT");

            migrationBuilder.DropForeignKey(
                name: "FK_YEETS_USERS_UserId",
                table: "YEETS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_YEETS",
                table: "YEETS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_USERS",
                table: "USERS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_USER_LIKES_YEETS_JT",
                table: "USER_LIKES_YEETS_JT");

            migrationBuilder.DropPrimaryKey(
                name: "PK_USER_FOLLOWS_USERS_JT",
                table: "USER_FOLLOWS_USERS_JT");

            migrationBuilder.RenameTable(
                name: "YEETS",
                newName: "yeets");

            migrationBuilder.RenameTable(
                name: "USERS",
                newName: "users");

            migrationBuilder.RenameTable(
                name: "USER_LIKES_YEETS_JT",
                newName: "user_likes_yeets_jt");

            migrationBuilder.RenameTable(
                name: "USER_FOLLOWS_USERS_JT",
                newName: "user_follows_users_jt");

            migrationBuilder.RenameColumn(
                name: "BODY",
                table: "yeets",
                newName: "body");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "yeets",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "yeets",
                newName: "yeet_id");

            migrationBuilder.RenameIndex(
                name: "IX_YEETS_UserId",
                table: "yeets",
                newName: "IX_yeets_user_id");

            migrationBuilder.RenameColumn(
                name: "USERNAME",
                table: "users",
                newName: "username");

            migrationBuilder.RenameColumn(
                name: "PASSWORD_HASH",
                table: "users",
                newName: "password_hash");

            migrationBuilder.RenameColumn(
                name: "LAST_NAME",
                table: "users",
                newName: "last_name");

            migrationBuilder.RenameColumn(
                name: "FIRST_NAME",
                table: "users",
                newName: "first_name");

            migrationBuilder.RenameColumn(
                name: "USER_ID",
                table: "users",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "USER_ID",
                table: "user_likes_yeets_jt",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "YEET_ID",
                table: "user_likes_yeets_jt",
                newName: "yeet_id");

            migrationBuilder.RenameIndex(
                name: "IX_USER_LIKES_YEETS_JT_USER_ID",
                table: "user_likes_yeets_jt",
                newName: "IX_user_likes_yeets_jt_user_id");

            migrationBuilder.RenameColumn(
                name: "Slave",
                table: "user_follows_users_jt",
                newName: "slave_id");

            migrationBuilder.RenameColumn(
                name: "MASTER",
                table: "user_follows_users_jt",
                newName: "master_id");

            migrationBuilder.RenameIndex(
                name: "IX_USER_FOLLOWS_USERS_JT_Slave",
                table: "user_follows_users_jt",
                newName: "IX_user_follows_users_jt_slave_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_yeets",
                table: "yeets",
                column: "yeet_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_users",
                table: "users",
                column: "user_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_likes_yeets_jt",
                table: "user_likes_yeets_jt",
                columns: new[] { "yeet_id", "user_id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_follows_users_jt",
                table: "user_follows_users_jt",
                columns: new[] { "master_id", "slave_id" });

            migrationBuilder.AddForeignKey(
                name: "FK_user_follows_users_jt_users_master_id",
                table: "user_follows_users_jt",
                column: "master_id",
                principalTable: "users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_follows_users_jt_users_slave_id",
                table: "user_follows_users_jt",
                column: "slave_id",
                principalTable: "users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_likes_yeets_jt_users_user_id",
                table: "user_likes_yeets_jt",
                column: "user_id",
                principalTable: "users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_likes_yeets_jt_yeets_yeet_id",
                table: "user_likes_yeets_jt",
                column: "yeet_id",
                principalTable: "yeets",
                principalColumn: "yeet_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_yeets_users_user_id",
                table: "yeets",
                column: "user_id",
                principalTable: "users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_follows_users_jt_users_master_id",
                table: "user_follows_users_jt");

            migrationBuilder.DropForeignKey(
                name: "FK_user_follows_users_jt_users_slave_id",
                table: "user_follows_users_jt");

            migrationBuilder.DropForeignKey(
                name: "FK_user_likes_yeets_jt_users_user_id",
                table: "user_likes_yeets_jt");

            migrationBuilder.DropForeignKey(
                name: "FK_user_likes_yeets_jt_yeets_yeet_id",
                table: "user_likes_yeets_jt");

            migrationBuilder.DropForeignKey(
                name: "FK_yeets_users_user_id",
                table: "yeets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_yeets",
                table: "yeets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_users",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user_likes_yeets_jt",
                table: "user_likes_yeets_jt");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user_follows_users_jt",
                table: "user_follows_users_jt");

            migrationBuilder.RenameTable(
                name: "yeets",
                newName: "YEETS");

            migrationBuilder.RenameTable(
                name: "users",
                newName: "USERS");

            migrationBuilder.RenameTable(
                name: "user_likes_yeets_jt",
                newName: "USER_LIKES_YEETS_JT");

            migrationBuilder.RenameTable(
                name: "user_follows_users_jt",
                newName: "USER_FOLLOWS_USERS_JT");

            migrationBuilder.RenameColumn(
                name: "body",
                table: "YEETS",
                newName: "BODY");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "YEETS",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "yeet_id",
                table: "YEETS",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_yeets_user_id",
                table: "YEETS",
                newName: "IX_YEETS_UserId");

            migrationBuilder.RenameColumn(
                name: "username",
                table: "USERS",
                newName: "USERNAME");

            migrationBuilder.RenameColumn(
                name: "password_hash",
                table: "USERS",
                newName: "PASSWORD_HASH");

            migrationBuilder.RenameColumn(
                name: "last_name",
                table: "USERS",
                newName: "LAST_NAME");

            migrationBuilder.RenameColumn(
                name: "first_name",
                table: "USERS",
                newName: "FIRST_NAME");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "USERS",
                newName: "USER_ID");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "USER_LIKES_YEETS_JT",
                newName: "USER_ID");

            migrationBuilder.RenameColumn(
                name: "yeet_id",
                table: "USER_LIKES_YEETS_JT",
                newName: "YEET_ID");

            migrationBuilder.RenameIndex(
                name: "IX_user_likes_yeets_jt_user_id",
                table: "USER_LIKES_YEETS_JT",
                newName: "IX_USER_LIKES_YEETS_JT_USER_ID");

            migrationBuilder.RenameColumn(
                name: "slave_id",
                table: "USER_FOLLOWS_USERS_JT",
                newName: "Slave");

            migrationBuilder.RenameColumn(
                name: "master_id",
                table: "USER_FOLLOWS_USERS_JT",
                newName: "MASTER");

            migrationBuilder.RenameIndex(
                name: "IX_user_follows_users_jt_slave_id",
                table: "USER_FOLLOWS_USERS_JT",
                newName: "IX_USER_FOLLOWS_USERS_JT_Slave");

            migrationBuilder.AddPrimaryKey(
                name: "PK_YEETS",
                table: "YEETS",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_USERS",
                table: "USERS",
                column: "USER_ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_USER_LIKES_YEETS_JT",
                table: "USER_LIKES_YEETS_JT",
                columns: new[] { "YEET_ID", "USER_ID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_USER_FOLLOWS_USERS_JT",
                table: "USER_FOLLOWS_USERS_JT",
                columns: new[] { "MASTER", "Slave" });

            migrationBuilder.AddForeignKey(
                name: "FK_USER_FOLLOWS_USERS_JT_USERS_MASTER",
                table: "USER_FOLLOWS_USERS_JT",
                column: "MASTER",
                principalTable: "USERS",
                principalColumn: "USER_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_USER_FOLLOWS_USERS_JT_USERS_Slave",
                table: "USER_FOLLOWS_USERS_JT",
                column: "Slave",
                principalTable: "USERS",
                principalColumn: "USER_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_USER_LIKES_YEETS_JT_USERS_USER_ID",
                table: "USER_LIKES_YEETS_JT",
                column: "USER_ID",
                principalTable: "USERS",
                principalColumn: "USER_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_USER_LIKES_YEETS_JT_YEETS_YEET_ID",
                table: "USER_LIKES_YEETS_JT",
                column: "YEET_ID",
                principalTable: "YEETS",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_YEETS_USERS_UserId",
                table: "YEETS",
                column: "UserId",
                principalTable: "USERS",
                principalColumn: "USER_ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
