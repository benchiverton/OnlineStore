using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Company.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Headline = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    VariantTypes = table.Column<string>(type: "TEXT", nullable: false),
                    Images = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Variants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProductId = table.Column<Guid>(type: "TEXT", nullable: false),
                    VariantTypeValues = table.Column<string>(type: "TEXT", nullable: false),
                    Price_FullPriceGBP = table.Column<decimal>(type: "TEXT", nullable: true),
                    Price_DealPriceGBP = table.Column<decimal>(type: "TEXT", nullable: true),
                    Price_Details = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Variants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Variants_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "Headline", "Images", "Name", "VariantTypes" },
                values: new object[,]
                {
                    { new Guid("b3bc7c47-bb91-48f6-baa8-808652408abb"), "Improve your mindfulness with Product Two", "A brilliant product for brilliant people", "[\"images\\\\products\\\\the_product_two.png\"]", "The Product Two", "[\"Magnitude\",\"Emotion\"]" },
                    { new Guid("c7192e81-0f5d-4c3e-aec5-d32c88aea039"), "Improve your well-being with Product One", "A great product for great people", "[\"images\\\\products\\\\the_product_one.png\"]", "The Product One", "[\"Size\",\"Colour\"]" }
                });

            migrationBuilder.InsertData(
                table: "Variants",
                columns: new[] { "Id", "Price_DealPriceGBP", "Price_Details", "Price_FullPriceGBP", "ProductId", "VariantTypeValues" },
                values: new object[,]
                {
                    { new Guid("0788c61e-9944-4e6c-8a3c-c33c1e89f1be"), 16.08m, "33% off today!", 24m, new Guid("c7192e81-0f5d-4c3e-aec5-d32c88aea039"), "{\"Size\":\"Big\",\"Colour\":\"Greener\"}" },
                    { new Guid("28b5525d-c920-44ba-9e36-990432589d83"), 54.27m, "33% off today!", 81m, new Guid("c7192e81-0f5d-4c3e-aec5-d32c88aea039"), "{\"Size\":\"Big\",\"Colour\":\"Greenest\"}" },
                    { new Guid("2901d199-c8ef-4200-9d3b-826d2256b5a1"), 35.51m, "2/3 off for a limited time!", 53m, new Guid("c7192e81-0f5d-4c3e-aec5-d32c88aea039"), "{\"Size\":\"Small\",\"Colour\":\"Greenest\"}" },
                    { new Guid("2e72ffb3-4da2-4108-93f3-8f2128c99dc1"), 6.70m, "33% off while stocks last!", 10m, new Guid("b3bc7c47-bb91-48f6-baa8-808652408abb"), "{\"Magnitude\":\"Small\",\"Emotion\":\"Excited\"}" },
                    { new Guid("3facedd3-8fb4-4757-bbc5-af9fee02fadc"), 3.35m, "2/3 off for a limited time!", 5m, new Guid("b3bc7c47-bb91-48f6-baa8-808652408abb"), "{\"Magnitude\":\"Small\",\"Emotion\":\"Happy\"}" },
                    { new Guid("51e94e79-392c-4753-886a-e751cff703a6"), 49.58m, "2/3 off for a limited time!", 74m, new Guid("b3bc7c47-bb91-48f6-baa8-808652408abb"), "{\"Magnitude\":\"Vast\",\"Emotion\":\"Flattered\"}" },
                    { new Guid("5c863956-2891-4530-8bda-218b456e0b8b"), 64.99m, "33% off while stocks last!", 97m, new Guid("c7192e81-0f5d-4c3e-aec5-d32c88aea039"), "{\"Size\":\"Small\",\"Colour\":\"Green\"}" },
                    { new Guid("5f3b4bef-3cbc-40bf-bca5-0e52f9d9bde8"), 22.78m, "33% off while stocks last!", 34m, new Guid("b3bc7c47-bb91-48f6-baa8-808652408abb"), "{\"Magnitude\":\"Vast\",\"Emotion\":\"Happy\"}" },
                    { new Guid("86e66430-6260-49fc-aeb4-771caee74ce6"), 16.75m, "33% off while stocks last!", 25m, new Guid("c7192e81-0f5d-4c3e-aec5-d32c88aea039"), "{\"Size\":\"Big\",\"Colour\":\"Green\"}" },
                    { new Guid("cb064427-54b4-47fb-a66b-95831bfa9b55"), 54.94m, "33% off while stocks last!", 82m, new Guid("c7192e81-0f5d-4c3e-aec5-d32c88aea039"), "{\"Size\":\"Small\",\"Colour\":\"Greener\"}" },
                    { new Guid("db9d3fea-4d12-4b9a-8e2a-0d5b53572708"), 16.75m, "33% off while stocks last!", 25m, new Guid("b3bc7c47-bb91-48f6-baa8-808652408abb"), "{\"Magnitude\":\"Vast\",\"Emotion\":\"Excited\"}" },
                    { new Guid("f87421b2-c1c9-4c6b-9e97-440763b5f735"), 66.33m, "33% off today!", 99m, new Guid("b3bc7c47-bb91-48f6-baa8-808652408abb"), "{\"Magnitude\":\"Small\",\"Emotion\":\"Flattered\"}" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Variants_ProductId",
                table: "Variants",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Variants");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
