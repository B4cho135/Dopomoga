using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dopomoga.Data.Migrations
{
    public partial class renamemaincategoriestable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_MainCategoryEntity_MainCategoryId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_MainCategoryEntity_MainCategoryId",
                table: "Posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MainCategoryEntity",
                table: "MainCategoryEntity");

            migrationBuilder.RenameTable(
                name: "MainCategoryEntity",
                newName: "MainCategories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MainCategories",
                table: "MainCategories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_MainCategories_MainCategoryId",
                table: "Categories",
                column: "MainCategoryId",
                principalTable: "MainCategories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_MainCategories_MainCategoryId",
                table: "Posts",
                column: "MainCategoryId",
                principalTable: "MainCategories",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_MainCategories_MainCategoryId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_MainCategories_MainCategoryId",
                table: "Posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MainCategories",
                table: "MainCategories");

            migrationBuilder.RenameTable(
                name: "MainCategories",
                newName: "MainCategoryEntity");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MainCategoryEntity",
                table: "MainCategoryEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_MainCategoryEntity_MainCategoryId",
                table: "Categories",
                column: "MainCategoryId",
                principalTable: "MainCategoryEntity",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_MainCategoryEntity_MainCategoryId",
                table: "Posts",
                column: "MainCategoryId",
                principalTable: "MainCategoryEntity",
                principalColumn: "Id");
        }
    }
}
