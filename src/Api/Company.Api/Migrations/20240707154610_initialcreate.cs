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
                    { new Guid("7dde14b2-de28-4826-9b9a-f76f631284ab"), "Rolling through life one pebble at a time!", "{\"Size\":[\"Small\",\"Big\"],\"Texture\":[\"Smooth\",\"Grainy\",\"Jagged\"]}", "Meet Pebble Dash, your adventurous little companion! Small but full of energy, Pebble Dash is always ready to roll into new adventures. With a smooth surface and a perfectly rounded shape, this pebble loves to explore the world and bring joy wherever it goes. Whether it's a playful dash across the desk or a calming presence on your nightstand, Pebble Dash is here to remind you that life's journey is best enjoyed one pebble at a time. Compact and full of charm, Pebble Dash is the perfect pocket-sized buddy for all your daily adventures.", "[\"images\\\\petrocks\\\\pebble_dash.png\"]", "Pebble Dash" },
                    { new Guid("9505dc3b-252e-439b-a777-df85d8292c9a"), "Ready to rock and slate the day!", "{\"Hardness\":[\"Soft\",\"Hard\"],\"Slateyness\":[\"Not very\",\"Moderately\",\"Very\"]}", "Introducing Slate Mate, your steadfast and stylish rock friend! Crafted from sleek, smooth slate, Slate Mate is the epitome of cool and collected. With its flat, elegant surface and modern look, this rock is the perfect desk companion or decorative piece. Slate Mate is always there to offer unwavering support, whether you're tackling a tough task or relaxing after a long day. Its timeless appearance and dependable nature make Slate Mate a reliable friend for every moment. Embrace the stability and charm of Slate Mate, and let this solid companion help you slate the day!", "[\"images\\\\petrocks\\\\slate_mate.png\"]", "Slate Mate" }
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
