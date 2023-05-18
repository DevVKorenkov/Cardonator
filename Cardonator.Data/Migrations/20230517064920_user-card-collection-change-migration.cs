using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cardonator.Data.Migrations
{
    /// <inheritdoc />
    public partial class usercardcollectionchangemigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_CardCollections_CardCollectionsId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CardCollectionsId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CardCollectionsId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "CardonatorUserId",
                table: "CardCollections",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CardCollections_CardonatorUserId",
                table: "CardCollections",
                column: "CardonatorUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CardCollections_AspNetUsers_CardonatorUserId",
                table: "CardCollections",
                column: "CardonatorUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CardCollections_AspNetUsers_CardonatorUserId",
                table: "CardCollections");

            migrationBuilder.DropIndex(
                name: "IX_CardCollections_CardonatorUserId",
                table: "CardCollections");

            migrationBuilder.DropColumn(
                name: "CardonatorUserId",
                table: "CardCollections");

            migrationBuilder.AddColumn<int>(
                name: "CardCollectionsId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CardCollectionsId",
                table: "AspNetUsers",
                column: "CardCollectionsId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_CardCollections_CardCollectionsId",
                table: "AspNetUsers",
                column: "CardCollectionsId",
                principalTable: "CardCollections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
