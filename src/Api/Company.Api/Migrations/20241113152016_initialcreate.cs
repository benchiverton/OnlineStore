using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Company.Api.Migrations
{
    /// <inheritdoc />
    public partial class initialcreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PetRocks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    OwnerId = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Catchphrase = table.Column<string>(type: "TEXT", nullable: true),
                    Attributes = table.Column<string>(type: "TEXT", nullable: false),
                    Images = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PetRocks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RocksUpForAdoption",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Catchphrase = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    CustomisableAttributes = table.Column<string>(type: "TEXT", nullable: false),
                    Images = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RocksUpForAdoption", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "RocksUpForAdoption",
                columns: new[] { "Id", "Catchphrase", "CustomisableAttributes", "Description", "Images", "Name" },
                values: new object[,]
                {
                    { new Guid("d4e553d5-2423-46db-8512-daf05cdc8001"), "Rolling through life one pebble at a time!", "{\"Size\":[\"Small\",\"Big\"],\"Texture\":[\"Smooth\",\"Grainy\",\"Jagged\"]}", "Small, smooth, and full of spirit, this round little explorer is always up for an adventure. Whether it’s zipping across your desk or chilling on your nightstand, Pebble Dash brings charm and good vibes wherever it rolls. Pocket-sized joy for everyday journeys.", "[\"images\\\\petrocks\\\\pebble_dash.png\"]", "Pebble Dash" },
                    { new Guid("f75326ac-c83d-468f-a99f-ec27bd2246ff"), "Ready to rock and slate the day!", "{\"Hardness\":[\"Soft\",\"Hard\"],\"Slateyness\":[\"Not very\",\"Moderately\",\"Very\"]}", "Smooth, sleek, and always chill, Slate Mate brings modern style and steady vibes to your space. Whether you're grinding through work or just vibing, this flat little legend’s got your back. Rock your day with Slate Mate – the solid sidekick you didn’t know you needed.", "[\"images\\\\petrocks\\\\slate_mate.png\"]", "Slate Mate" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PetRocks_OwnerId",
                table: "PetRocks",
                column: "OwnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PetRocks");

            migrationBuilder.DropTable(
                name: "RocksUpForAdoption");
        }
    }
}
