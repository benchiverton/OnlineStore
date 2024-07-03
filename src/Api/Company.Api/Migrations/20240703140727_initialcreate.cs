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
                name: "RocksUpForAdoption",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Catchphrase = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    VariantTypes = table.Column<string>(type: "TEXT", nullable: false),
                    Images = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RocksUpForAdoption", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RockVariantsUpForAdoption",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    PetRockId = table.Column<Guid>(type: "TEXT", nullable: false),
                    VariantTypeValues = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RockVariantsUpForAdoption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RockVariantsUpForAdoption_RocksUpForAdoption_PetRockId",
                        column: x => x.PetRockId,
                        principalTable: "RocksUpForAdoption",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "RocksUpForAdoption",
                columns: new[] { "Id", "Catchphrase", "Description", "Images", "Name", "VariantTypes" },
                values: new object[,]
                {
                    { new Guid("207ea037-5ae5-4116-ab8d-d04c5be49eb9"), "Rolling through life one pebble at a time.", "Meet Pebble Dash, your adventurous little companion! Small but full of energy, Pebble Dash is always ready to roll into new adventures. With a smooth surface and a perfectly rounded shape, this pebble loves to explore the world and bring joy wherever it goes. Whether it's a playful dash across the desk or a calming presence on your nightstand, Pebble Dash is here to remind you that life's journey is best enjoyed one pebble at a time. Compact and full of charm, Pebble Dash is the perfect pocket-sized buddy for all your daily adventures.", "[\"images\\\\petrocks\\\\pebble_dash.png\"]", "Pebble Dash", "[\"Size\",\"Texture\"]" },
                    { new Guid("88a441a4-d031-49a4-87b6-60fb0b8da5e3"), "Ready to rock and slate the day.", "Introducing Slate Mate, your steadfast and stylish rock friend! Crafted from sleek, smooth slate, Slate Mate is the epitome of cool and collected. With its flat, elegant surface and modern look, this rock is the perfect desk companion or decorative piece. Slate Mate is always there to offer unwavering support, whether you're tackling a tough task or relaxing after a long day. Its timeless appearance and dependable nature make Slate Mate a reliable friend for every moment. Embrace the stability and charm of Slate Mate, and let this solid companion help you slate the day!", "[\"images\\\\petrocks\\\\slate_mate.png\"]", "Slate Mate", "[\"Hardness\",\"Slateyness\"]" }
                });

            migrationBuilder.InsertData(
                table: "RockVariantsUpForAdoption",
                columns: new[] { "Id", "PetRockId", "VariantTypeValues" },
                values: new object[,]
                {
                    { new Guid("28e66411-836a-4e7b-afbe-fafe3d199e23"), new Guid("207ea037-5ae5-4116-ab8d-d04c5be49eb9"), "{\"Size\":\"Small\",\"Texture\":\"Smooth\"}" },
                    { new Guid("3e285cca-8adf-4c49-963b-0f6487083c22"), new Guid("88a441a4-d031-49a4-87b6-60fb0b8da5e3"), "{\"Hardness\":\"Soft\",\"Slateyness\":\"Moderately slatey\"}" },
                    { new Guid("4b1d93db-5505-47c9-93e7-e702e3ab4d3d"), new Guid("207ea037-5ae5-4116-ab8d-d04c5be49eb9"), "{\"Size\":\"Small\",\"Texture\":\"Jagged\"}" },
                    { new Guid("6e7938fd-d40a-4e02-ba60-e7cf592694ca"), new Guid("207ea037-5ae5-4116-ab8d-d04c5be49eb9"), "{\"Size\":\"Small\",\"Texture\":\"Grainy\"}" },
                    { new Guid("7210d372-9c67-4230-b298-c6bf3d4fe81f"), new Guid("88a441a4-d031-49a4-87b6-60fb0b8da5e3"), "{\"Hardness\":\"Hard\",\"Slateyness\":\"Extra slatey\"}" },
                    { new Guid("7689c1f5-d115-4c86-ad43-2105747c6feb"), new Guid("207ea037-5ae5-4116-ab8d-d04c5be49eb9"), "{\"Size\":\"Big\",\"Texture\":\"Grainy\"}" },
                    { new Guid("8df24d32-1d8d-4f0e-ade1-5666680d0aa1"), new Guid("88a441a4-d031-49a4-87b6-60fb0b8da5e3"), "{\"Hardness\":\"Hard\",\"Slateyness\":\"Moderately slatey\"}" },
                    { new Guid("98c8a1c9-5859-43f3-ae82-8d09e9c3872d"), new Guid("88a441a4-d031-49a4-87b6-60fb0b8da5e3"), "{\"Hardness\":\"Soft\",\"Slateyness\":\"Extra slatey\"}" },
                    { new Guid("de537415-9773-4385-a297-c44849610be8"), new Guid("207ea037-5ae5-4116-ab8d-d04c5be49eb9"), "{\"Size\":\"Big\",\"Texture\":\"Jagged\"}" },
                    { new Guid("e3977776-55f1-4866-8252-787ddfb1432d"), new Guid("207ea037-5ae5-4116-ab8d-d04c5be49eb9"), "{\"Size\":\"Big\",\"Texture\":\"Smooth\"}" },
                    { new Guid("fbbcdb5b-4cc6-4722-a24f-3fc001daaa07"), new Guid("88a441a4-d031-49a4-87b6-60fb0b8da5e3"), "{\"Hardness\":\"Hard\",\"Slateyness\":\"Not very\"}" },
                    { new Guid("fe4ae075-d5d1-4fa2-b6c5-a3b92ef69ab1"), new Guid("88a441a4-d031-49a4-87b6-60fb0b8da5e3"), "{\"Hardness\":\"Soft\",\"Slateyness\":\"Not very\"}" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RockVariantsUpForAdoption_PetRockId",
                table: "RockVariantsUpForAdoption",
                column: "PetRockId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RockVariantsUpForAdoption");

            migrationBuilder.DropTable(
                name: "RocksUpForAdoption");
        }
    }
}
