using ContosoPizza.Models;
using Microsoft.EntityFrameworkCore;

namespace ContosoPizza.Data;

public class PizzaDbContext : DbContext
{
    public PizzaDbContext(DbContextOptions<PizzaDbContext> options) : base(options)
    {
    }

    public DbSet<Pizza> Pizzas { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure Pizza entity
        modelBuilder.Entity<Pizza>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Price).HasColumnType("decimal(18,2)");
            entity.Property(e => e.ImageUrl).HasMaxLength(500);
            
            // Configure Toppings as JSON column
            entity.Property(e => e.Toppings)
                .HasColumnType("jsonb");
        });
        
        // Seed initial data using OwnsOne navigation for complex properties
        modelBuilder.Entity<Pizza>().HasData(
            new Pizza
            {
                Id = 1,
                Name = "Classic Margherita",
                Description = "Fresh tomato sauce, mozzarella, and basil",
                Price = 12.99m,
                IsGlutenFree = false,
                Size = PizzaSize.Medium,
                ImageUrl = "https://example.com/margherita.jpg"
            },
            new Pizza
            {
                Id = 2,
                Name = "Pepperoni Deluxe",
                Description = "Loaded with pepperoni and extra cheese",
                Price = 15.99m,
                IsGlutenFree = false,
                Size = PizzaSize.Large,
                ImageUrl = "https://example.com/pepperoni.jpg"
            },
            new Pizza
            {
                Id = 3,
                Name = "Veggie Supreme",
                Description = "Fresh vegetables on a gluten-free crust",
                Price = 14.99m,
                IsGlutenFree = true,
                Size = PizzaSize.Medium,
                ImageUrl = "https://example.com/veggie.jpg"
            },
            new Pizza
            {
                Id = 4,
                Name = "Meat Lovers",
                Description = "For the carnivores - pepperoni, sausage, bacon, and ham",
                Price = 18.99m,
                IsGlutenFree = false,
                Size = PizzaSize.Large,
                ImageUrl = "https://example.com/meatlover.jpg"
            },
            new Pizza
            {
                Id = 5,
                Name = "Hawaiian Paradise",
                Description = "Ham and pineapple on a crispy crust",
                Price = 13.99m,
                IsGlutenFree = false,
                Size = PizzaSize.Small,
                ImageUrl = "https://example.com/hawaiian.jpg"
            },
            new Pizza
            {
                Id = 6,
                Name = "BBQ Chicken",
                Description = "Grilled chicken with BBQ sauce and red onions",
                Price = 16.99m,
                IsGlutenFree = false,
                Size = PizzaSize.Large,
                ImageUrl = "https://example.com/bbq-chicken.jpg"
            }
        );
    }
}
