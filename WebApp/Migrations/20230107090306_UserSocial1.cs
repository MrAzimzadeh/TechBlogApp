using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp.Migrations
{
    /// <inheritdoc />
    public partial class UserSocial1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserNetwork_SocialNetwork_SocialNetworkId",
                table: "UserNetwork");

            migrationBuilder.DropForeignKey(
                name: "FK_UserNetwork_Users_UserId1",
                table: "UserNetwork");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserNetwork",
                table: "UserNetwork");

            migrationBuilder.RenameTable(
                name: "UserNetwork",
                newName: "UserNetworks");

            migrationBuilder.RenameIndex(
                name: "IX_UserNetwork_UserId1",
                table: "UserNetworks",
                newName: "IX_UserNetworks_UserId1");

            migrationBuilder.RenameIndex(
                name: "IX_UserNetwork_SocialNetworkId",
                table: "UserNetworks",
                newName: "IX_UserNetworks_SocialNetworkId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId1",
                table: "UserNetworks",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserNetworks",
                table: "UserNetworks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserNetworks_SocialNetwork_SocialNetworkId",
                table: "UserNetworks",
                column: "SocialNetworkId",
                principalTable: "SocialNetwork",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserNetworks_Users_UserId1",
                table: "UserNetworks",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserNetworks_SocialNetwork_SocialNetworkId",
                table: "UserNetworks");

            migrationBuilder.DropForeignKey(
                name: "FK_UserNetworks_Users_UserId1",
                table: "UserNetworks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserNetworks",
                table: "UserNetworks");

            migrationBuilder.RenameTable(
                name: "UserNetworks",
                newName: "UserNetwork");

            migrationBuilder.RenameIndex(
                name: "IX_UserNetworks_UserId1",
                table: "UserNetwork",
                newName: "IX_UserNetwork_UserId1");

            migrationBuilder.RenameIndex(
                name: "IX_UserNetworks_SocialNetworkId",
                table: "UserNetwork",
                newName: "IX_UserNetwork_SocialNetworkId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId1",
                table: "UserNetwork",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserNetwork",
                table: "UserNetwork",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserNetwork_SocialNetwork_SocialNetworkId",
                table: "UserNetwork",
                column: "SocialNetworkId",
                principalTable: "SocialNetwork",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserNetwork_Users_UserId1",
                table: "UserNetwork",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
