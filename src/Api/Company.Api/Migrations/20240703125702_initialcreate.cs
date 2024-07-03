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
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Catchphrase = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    VariantTypes = table.Column<string>(type: "TEXT", nullable: false),
                    Images = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PetRocks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Variants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    PetRockId = table.Column<Guid>(type: "TEXT", nullable: false),
                    VariantTypeValues = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Variants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Variants_PetRocks_PetRockId",
                        column: x => x.PetRockId,
                        principalTable: "PetRocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "PetRocks",
                columns: new[] { "Id", "Catchphrase", "Description", "Images", "Name", "VariantTypes" },
                values: new object[,]
                {
                    { new Guid("651ce7e2-0d9d-4ed9-81af-db7dc483e49a"), "Ready to rock and slate the day.", "Introducing Slate Mate, your steadfast and stylish rock friend! Crafted from sleek, smooth slate, Slate Mate is the epitome of cool and collected. With its flat, elegant surface and modern look, this rock is the perfect desk companion or decorative piece. Slate Mate is always there to offer unwavering support, whether you're tackling a tough task or relaxing after a long day. Its timeless appearance and dependable nature make Slate Mate a reliable friend for every moment. Embrace the stability and charm of Slate Mate, and let this solid companion help you slate the day!", "[\"images\\\\petrocks\\\\slate_mate.png\"]", "Slate Mate", "[\"Hardness\",\"Slateyness\"]" },
                    { new Guid("9dd853c8-4776-4ab1-ad37-e90e9e523bcc"), "Rolling through life one pebble at a time.", "Meet Pebble Dash, your adventurous little companion! Small but full of energy, Pebble Dash is always ready to roll into new adventures. With a smooth surface and a perfectly rounded shape, this pebble loves to explore the world and bring joy wherever it goes. Whether it's a playful dash across the desk or a calming presence on your nightstand, Pebble Dash is here to remind you that life's journey is best enjoyed one pebble at a time. Compact and full of charm, Pebble Dash is the perfect pocket-sized buddy for all your daily adventures.", "[\"images\\\\petrocks\\\\pebble_dash.png\"]", "Pebble Dash", "[\"Size\",\"Texture\"]" }
                });

            migrationBuilder.InsertData(
                table: "Variants",
                columns: new[] { "Id", "PetRockId", "VariantTypeValues" },
                values: new object[,]
                {
                    { new Guid("4e6a2f59-be6c-45fc-a815-4fddaa124088"), new Guid("9dd853c8-4776-4ab1-ad37-e90e9e523bcc"), "{\"Size\":\"Small\",\"Texture\":\"Smooth\"}" },
                    { new Guid("4f065f9a-8dbd-49ad-9098-5d95ca6e6b8c"), new Guid("9dd853c8-4776-4ab1-ad37-e90e9e523bcc"), "{\"Size\":\"Big\",\"Texture\":\"Smooth\"}" },
                    { new Guid("50425cbd-7732-4b61-a820-4240a39bdc78"), new Guid("9dd853c8-4776-4ab1-ad37-e90e9e523bcc"), "{\"Size\":\"Big\",\"Texture\":\"Jagged\"}" },
                    { new Guid("5298d8cb-bd43-4b1d-be97-b2ad1197653e"), new Guid("651ce7e2-0d9d-4ed9-81af-db7dc483e49a"), "{\"Hardness\":\"Hard\",\"Slateyness\":\"Extra slatey\"}" },
                    { new Guid("5f9d8ca2-25d3-4d87-a509-13cfaa21eb21"), new Guid("9dd853c8-4776-4ab1-ad37-e90e9e523bcc"), "{\"Size\":\"Big\",\"Texture\":\"Grainy\"}" },
                    { new Guid("652ff3bf-4cdd-4fb1-80bb-3ec7ec6fc0dc"), new Guid("9dd853c8-4776-4ab1-ad37-e90e9e523bcc"), "{\"Size\":\"Small\",\"Texture\":\"Jagged\"}" },
                    { new Guid("6e055e20-9a49-4323-a1e6-e9c73386546c"), new Guid("651ce7e2-0d9d-4ed9-81af-db7dc483e49a"), "{\"Hardness\":\"Soft\",\"Slateyness\":\"Not very\"}" },
                    { new Guid("9f214908-8b4e-4312-8768-3f942b96dfd7"), new Guid("651ce7e2-0d9d-4ed9-81af-db7dc483e49a"), "{\"Hardness\":\"Soft\",\"Slateyness\":\"Extra slatey\"}" },
                    { new Guid("a9f01390-0d4c-4f8c-bf0e-a2d8d0236735"), new Guid("651ce7e2-0d9d-4ed9-81af-db7dc483e49a"), "{\"Hardness\":\"Soft\",\"Slateyness\":\"Moderately slatey\"}" },
                    { new Guid("ca2f1e58-85f0-4a4a-bb88-e620a5835afe"), new Guid("651ce7e2-0d9d-4ed9-81af-db7dc483e49a"), "{\"Hardness\":\"Hard\",\"Slateyness\":\"Not very\"}" },
                    { new Guid("e5e8504b-ad37-4c74-b5e7-6a1a1ee8074e"), new Guid("9dd853c8-4776-4ab1-ad37-e90e9e523bcc"), "{\"Size\":\"Small\",\"Texture\":\"Grainy\"}" },
                    { new Guid("f6d26e13-6519-42c1-b8d5-405a58598c84"), new Guid("651ce7e2-0d9d-4ed9-81af-db7dc483e49a"), "{\"Hardness\":\"Hard\",\"Slateyness\":\"Moderately slatey\"}" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Variants_PetRockId",
                table: "Variants",
                column: "PetRockId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Variants");

            migrationBuilder.DropTable(
                name: "PetRocks");
        }
    }
}
