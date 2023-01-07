using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp.Migrations
{
    /// <inheritdoc />
    public partial class UserSocialDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserNetworks_SocialNetwork_SocialNetworkId",
                table: "UserNetworks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SocialNetwork",
                table: "SocialNetwork");

            migrationBuilder.RenameTable(
                name: "SocialNetwork",
                newName: "SocialNetworks");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SocialNetworks",
                table: "SocialNetworks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserNetworks_SocialNetworks_SocialNetworkId",
                table: "UserNetworks",
                column: "SocialNetworkId",
                principalTable: "SocialNetworks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserNetworks_SocialNetworks_SocialNetworkId",
                table: "UserNetworks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SocialNetworks",
                table: "SocialNetworks");

            migrationBuilder.RenameTable(
                name: "SocialNetworks",
                newName: "SocialNetwork");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SocialNetwork",
                table: "SocialNetwork",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserNetworks_SocialNetwork_SocialNetworkId",
                table: "UserNetworks",
                column: "SocialNetworkId",
                principalTable: "SocialNetwork",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
