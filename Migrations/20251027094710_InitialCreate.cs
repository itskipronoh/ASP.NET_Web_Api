using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ContosoPizza.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pizzas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Price = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    IsGlutenFree = table.Column<bool>(type: "boolean", nullable: false),
                    Size = table.Column<int>(type: "integer", nullable: false),
                    Toppings = table.Column<List<string>>(type: "jsonb", nullable: true),
                    ImageUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pizzas", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Pizzas",
                columns: new[] { "Id", "Description", "ImageUrl", "IsGlutenFree", "Name", "Price", "Size", "Toppings" },
                values: new object[,]
                {
                    { 1, "Fresh tomato sauce, mozzarella, and basil", "https://example.com/margherita.jpg", false, "Classic Margherita", 12.99m, 1, null },
                    { 2, "Loaded with pepperoni and extra cheese", "https://example.com/pepperoni.jpg", false, "Pepperoni Deluxe", 15.99m, 2, null },
                    { 3, "Fresh vegetables on a gluten-free crust", "https://example.com/veggie.jpg", true, "Veggie Supreme", 14.99m, 1, null },
                    { 4, "For the carnivores - pepperoni, sausage, bacon, and ham", "https://example.com/meatlover.jpg", false, "Meat Lovers", 18.99m, 2, null },
                    { 5, "Ham and pineapple on a crispy crust", "https://example.com/hawaiian.jpg", false, "Hawaiian Paradise", 13.99m, 0, null },
                    { 6, "Grilled chicken with BBQ sauce and red onions", "https://example.com/bbq-chicken.jpg", false, "BBQ Chicken", 16.99m, 2, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pizzas");
        }
    }
}
