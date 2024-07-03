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
                    Owner = table.Column<Guid>(type: "TEXT", nullable: false),
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
                    AdoptableRockId = table.Column<Guid>(type: "TEXT", nullable: false),
                    VariantTypeValues = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RockVariantsUpForAdoption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RockVariantsUpForAdoption_RocksUpForAdoption_AdoptableRockId",
                        column: x => x.AdoptableRockId,
                        principalTable: "RocksUpForAdoption",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "RocksUpForAdoption",
                columns: new[] { "Id", "Catchphrase", "Description", "Images", "Name", "VariantTypes" },
                values: new object[,]
                {
                    { new Guid("278a4d93-53f1-49a6-bf62-5e3d0d9a9a37"), "Rolling through life one pebble at a time!", "Meet Pebble Dash, your adventurous little companion! Small but full of energy, Pebble Dash is always ready to roll into new adventures. With a smooth surface and a perfectly rounded shape, this pebble loves to explore the world and bring joy wherever it goes. Whether it's a playful dash across the desk or a calming presence on your nightstand, Pebble Dash is here to remind you that life's journey is best enjoyed one pebble at a time. Compact and full of charm, Pebble Dash is the perfect pocket-sized buddy for all your daily adventures.", "[\"images\\\\petrocks\\\\pebble_dash.png\"]", "Pebble Dash", "[\"Size\",\"Texture\"]" },
                    { new Guid("91e8c938-58e3-433c-a90a-47fa1ace3f06"), "Ready to rock and slate the day!", "Introducing Slate Mate, your steadfast and stylish rock friend! Crafted from sleek, smooth slate, Slate Mate is the epitome of cool and collected. With its flat, elegant surface and modern look, this rock is the perfect desk companion or decorative piece. Slate Mate is always there to offer unwavering support, whether you're tackling a tough task or relaxing after a long day. Its timeless appearance and dependable nature make Slate Mate a reliable friend for every moment. Embrace the stability and charm of Slate Mate, and let this solid companion help you slate the day!", "[\"images\\\\petrocks\\\\slate_mate.png\"]", "Slate Mate", "[\"Hardness\",\"Slateyness\"]" }
                });

            migrationBuilder.InsertData(
                table: "RockVariantsUpForAdoption",
                columns: new[] { "Id", "AdoptableRockId", "VariantTypeValues" },
                values: new object[,]
                {
                    { new Guid("0053d7e5-1a3e-4306-ae81-d0381637c1c1"), new Guid("91e8c938-58e3-433c-a90a-47fa1ace3f06"), "{\"Hardness\":\"Hard\",\"Slateyness\":\"Extra slatey\"}" },
                    { new Guid("06db05f0-e8f4-44cc-9f65-155f754da999"), new Guid("278a4d93-53f1-49a6-bf62-5e3d0d9a9a37"), "{\"Size\":\"Big\",\"Texture\":\"Smooth\"}" },
                    { new Guid("1f1c3977-3824-4e07-829e-32099dcd74c5"), new Guid("278a4d93-53f1-49a6-bf62-5e3d0d9a9a37"), "{\"Size\":\"Small\",\"Texture\":\"Grainy\"}" },
                    { new Guid("243de795-e35c-4b03-84ec-a0e2526e3ea8"), new Guid("91e8c938-58e3-433c-a90a-47fa1ace3f06"), "{\"Hardness\":\"Soft\",\"Slateyness\":\"Extra slatey\"}" },
                    { new Guid("31edf9b2-8e1a-4ce6-8618-cbb7300b4b28"), new Guid("91e8c938-58e3-433c-a90a-47fa1ace3f06"), "{\"Hardness\":\"Soft\",\"Slateyness\":\"Moderately slatey\"}" },
                    { new Guid("3b0a01f6-9b31-48e8-89fd-b6b615794a58"), new Guid("278a4d93-53f1-49a6-bf62-5e3d0d9a9a37"), "{\"Size\":\"Big\",\"Texture\":\"Jagged\"}" },
                    { new Guid("76ac369e-c51b-4460-9cea-fda047907221"), new Guid("91e8c938-58e3-433c-a90a-47fa1ace3f06"), "{\"Hardness\":\"Hard\",\"Slateyness\":\"Not very\"}" },
                    { new Guid("823aba63-c557-474b-b277-13bdc63cbf52"), new Guid("91e8c938-58e3-433c-a90a-47fa1ace3f06"), "{\"Hardness\":\"Soft\",\"Slateyness\":\"Not very\"}" },
                    { new Guid("9232c3bb-2684-479a-95cd-f2c5b6c5a540"), new Guid("278a4d93-53f1-49a6-bf62-5e3d0d9a9a37"), "{\"Size\":\"Small\",\"Texture\":\"Smooth\"}" },
                    { new Guid("930ba646-08f8-4ea9-9456-7b9e796dd03b"), new Guid("278a4d93-53f1-49a6-bf62-5e3d0d9a9a37"), "{\"Size\":\"Big\",\"Texture\":\"Grainy\"}" },
                    { new Guid("9cca2ac5-7787-4c82-b1bb-35aaab8f0900"), new Guid("91e8c938-58e3-433c-a90a-47fa1ace3f06"), "{\"Hardness\":\"Hard\",\"Slateyness\":\"Moderately slatey\"}" },
                    { new Guid("fba21e9e-2123-4684-bc1c-5cf9f78bb505"), new Guid("278a4d93-53f1-49a6-bf62-5e3d0d9a9a37"), "{\"Size\":\"Small\",\"Texture\":\"Jagged\"}" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PetRocks_Owner",
                table: "PetRocks",
                column: "Owner");

            migrationBuilder.CreateIndex(
                name: "IX_RockVariantsUpForAdoption_AdoptableRockId",
                table: "RockVariantsUpForAdoption",
                column: "AdoptableRockId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PetRocks");

            migrationBuilder.DropTable(
                name: "RockVariantsUpForAdoption");

            migrationBuilder.DropTable(
                name: "RocksUpForAdoption");
        }
    }
}
