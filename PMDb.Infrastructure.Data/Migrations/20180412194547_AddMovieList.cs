using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace PMDb.Infrastructure.Data.Migrations
{
    public partial class AddMovieList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MovieList",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsDefault = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieList", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MovieListMovie",
                columns: table => new
                {
                    MovieId = table.Column<int>(nullable: false),
                    MovieListId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieListMovie", x => new { x.MovieId, x.MovieListId });
                    table.ForeignKey(
                        name: "FK_MovieListMovie_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieListMovie_MovieList_MovieListId",
                        column: x => x.MovieListId,
                        principalTable: "MovieList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovieListMovie_MovieListId",
                table: "MovieListMovie",
                column: "MovieListId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieListMovie");

            migrationBuilder.DropTable(
                name: "MovieList");
        }
    }
}
