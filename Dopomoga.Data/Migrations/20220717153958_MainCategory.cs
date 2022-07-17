using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Dopomoga.Data.Migrations
{
    public partial class MainCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.AddColumn<int>(
                name: "MainCategoryId",
                table: "Posts",
                type: "integer",
                nullable: true);

            

          
            migrationBuilder.AddColumn<int>(
                name: "MainCategoryId",
                table: "Categories",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MainCategoryEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NameGeorgian = table.Column<string>(type: "text", nullable: true),
                    NameEnglish = table.Column<string>(type: "text", nullable: true),
                    NameUkrainian = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainCategoryEntity", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_MainCategoryId",
                table: "Posts",
                column: "MainCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_MainCategoryId",
                table: "Categories",
                column: "MainCategoryId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_MainCategoryEntity_MainCategoryId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_MainCategoryEntity_MainCategoryId",
                table: "Posts");

            migrationBuilder.DropTable(
                name: "MainCategoryEntity");

            migrationBuilder.DropIndex(
                name: "IX_Posts_MainCategoryId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Categories_MainCategoryId",
                table: "Categories");

            

            migrationBuilder.DropColumn(
                name: "MainCategoryId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "MainCategoryId",
                table: "Categories");

        }
    }
}
