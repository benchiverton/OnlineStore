using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Company.Api.Migrations
{
    /// <inheritdoc />
    public partial class initialCreate : Migration
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
                    VariantTypeValues = table.Column<string>(type: "TEXT", nullable: false),
                    Price_FullPriceGBP = table.Column<decimal>(type: "TEXT", nullable: true),
                    Price_DealPriceGBP = table.Column<decimal>(type: "TEXT", nullable: true),
                    Price_Details = table.Column<string>(type: "TEXT", nullable: true)
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
                    { new Guid("d5bf2e08-4324-44fb-8e50-a9b244b88947"), "Ready to rock and slate the day.", "Introducing Slate Mate, your steadfast and stylish rock friend! Crafted from sleek, smooth slate, Slate Mate is the epitome of cool and collected. With its flat, elegant surface and modern look, this rock is the perfect desk companion or decorative piece. Slate Mate is always there to offer unwavering support, whether you're tackling a tough task or relaxing after a long day. Its timeless appearance and dependable nature make Slate Mate a reliable friend for every moment. Embrace the stability and charm of Slate Mate, and let this solid companion help you slate the day!", "[\"images\\\\petrocks\\\\slate_mate.png\"]", "Slate Mate", "[\"Hardness\",\"Slateyness\"]" },
                    { new Guid("fe19a010-668b-441e-ae57-67cf63607c49"), "Rolling through life one pebble at a time.", "Meet Pebble Dash, your adventurous little companion! Small but full of energy, Pebble Dash is always ready to roll into new adventures. With a smooth surface and a perfectly rounded shape, this pebble loves to explore the world and bring joy wherever it goes. Whether it's a playful dash across the desk or a calming presence on your nightstand, Pebble Dash is here to remind you that life's journey is best enjoyed one pebble at a time. Compact and full of charm, Pebble Dash is the perfect pocket-sized buddy for all your daily adventures.", "[\"images\\\\petrocks\\\\pebble_dash.png\"]", "Pebble Dash", "[\"Size\",\"Texture\"]" }
                });

            migrationBuilder.InsertData(
                table: "Variants",
                columns: new[] { "Id", "Price_DealPriceGBP", "Price_Details", "Price_FullPriceGBP", "PetRockId", "VariantTypeValues" },
                values: new object[,]
                {
                    { new Guid("037fa2cc-1189-4889-baca-3091e2cbadb6"), 34.84m, "33% off while stocks last!", 52m, new Guid("fe19a010-668b-441e-ae57-67cf63607c49"), "{\"Size\":\"Big\",\"Texture\":\"Jagged\"}" },
                    { new Guid("054d3143-f9d8-4b23-b1c4-3dcabfb8e68c"), 26.80m, "33% off while stocks last!", 40m, new Guid("d5bf2e08-4324-44fb-8e50-a9b244b88947"), "{\"Hardness\":\"Hard\",\"Slateyness\":\"Not very\"}" },
                    { new Guid("0d3bf6d4-2760-4eb5-840d-3588f5b7bc54"), 8.71m, "33% off while stocks last!", 13m, new Guid("d5bf2e08-4324-44fb-8e50-a9b244b88947"), "{\"Hardness\":\"Soft\",\"Slateyness\":\"Extra slatey\"}" },
                    { new Guid("1822b3fa-0b27-4bed-9d4b-617967e5619f"), 36.85m, "33% off while stocks last!", 55m, new Guid("fe19a010-668b-441e-ae57-67cf63607c49"), "{\"Size\":\"Small\",\"Texture\":\"Smooth\"}" },
                    { new Guid("21f0d944-9ed1-424a-af96-944b6a056bcd"), 44.22m, "33% off today!", 66m, new Guid("d5bf2e08-4324-44fb-8e50-a9b244b88947"), "{\"Hardness\":\"Soft\",\"Slateyness\":\"Not very\"}" },
                    { new Guid("3f06dd60-3db9-42f3-8567-5143a3cd437b"), 46.23m, "33% off today!", 69m, new Guid("fe19a010-668b-441e-ae57-67cf63607c49"), "{\"Size\":\"Big\",\"Texture\":\"Smooth\"}" },
                    { new Guid("42f6cf5e-8816-4cc2-84f6-d9645e6c820c"), 28.81m, "33% off while stocks last!", 43m, new Guid("d5bf2e08-4324-44fb-8e50-a9b244b88947"), "{\"Hardness\":\"Soft\",\"Slateyness\":\"Moderately slatey\"}" },
                    { new Guid("572e0dbb-b2aa-4aac-a34f-770b7771cd6c"), 41.54m, "2/3 off for a limited time!", 62m, new Guid("fe19a010-668b-441e-ae57-67cf63607c49"), "{\"Size\":\"Small\",\"Texture\":\"Grainy\"}" },
                    { new Guid("76984ca6-2571-4011-b136-9eaa1e7897f9"), 57.62m, "2/3 off for a limited time!", 86m, new Guid("fe19a010-668b-441e-ae57-67cf63607c49"), "{\"Size\":\"Big\",\"Texture\":\"Grainy\"}" },
                    { new Guid("8a778d4d-1b6d-4326-8694-ec2c578d9b57"), 40.20m, "33% off today!", 60m, new Guid("fe19a010-668b-441e-ae57-67cf63607c49"), "{\"Size\":\"Small\",\"Texture\":\"Jagged\"}" },
                    { new Guid("a5814287-e18a-486f-952e-02648c35d64e"), 53.60m, "2/3 off for a limited time!", 80m, new Guid("d5bf2e08-4324-44fb-8e50-a9b244b88947"), "{\"Hardness\":\"Hard\",\"Slateyness\":\"Extra slatey\"}" },
                    { new Guid("b2a75541-f22c-41b8-8662-b2fa17a37d4b"), 8.71m, "33% off while stocks last!", 13m, new Guid("d5bf2e08-4324-44fb-8e50-a9b244b88947"), "{\"Hardness\":\"Hard\",\"Slateyness\":\"Moderately slatey\"}" }
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
