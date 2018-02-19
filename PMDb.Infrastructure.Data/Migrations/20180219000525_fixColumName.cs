using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace PMDb.Infrastructure.Data.Migrations
{
    public partial class fixColumName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RotenTomatosRaing",
                table: "Ratings",
                newName: "RotenTomatosRating");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RotenTomatosRating",
                table: "Ratings",
                newName: "RotenTomatosRaing");
        }
    }
}
