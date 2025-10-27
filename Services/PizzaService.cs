using ContosoPizza.Models;

namespace ContosoPizza.Services;

public static class PizzaService
{
    static List<Pizza> Pizzas { get; }
    static int nextId = 7;
    static PizzaService()
    {
        Pizzas = new List<Pizza>
        {
            new Pizza 
            { 
                Id = 1, 
                Name = "Classic Margherita", 
                Description = "Fresh tomato sauce, mozzarella cheese, and basil leaves",
                Price = 12.99m,
                IsGlutenFree = false,
                Size = PizzaSize.Medium,
                Toppings = new List<string> { "Tomato Sauce", "Mozzarella", "Basil" },
                ImageUrl = "https://example.com/images/margherita.jpg"
            },
            new Pizza 
            { 
                Id = 2, 
                Name = "Pepperoni Deluxe", 
                Description = "Loaded with premium pepperoni and extra cheese",
                Price = 15.99m,
                IsGlutenFree = false,
                Size = PizzaSize.Large,
                Toppings = new List<string> { "Tomato Sauce", "Mozzarella", "Pepperoni" },
                ImageUrl = "https://example.com/images/pepperoni.jpg"
            },
            new Pizza 
            { 
                Id = 3, 
                Name = "Veggie Supreme", 
                Description = "Fresh vegetables on a gluten-free crust",
                Price = 14.99m,
                IsGlutenFree = true,
                Size = PizzaSize.Medium,
                Toppings = new List<string> { "Tomato Sauce", "Mozzarella", "Mushrooms", "Bell Peppers", "Onions", "Olives" },
                ImageUrl = "https://example.com/images/veggie.jpg"
            },
            new Pizza 
            { 
                Id = 4, 
                Name = "Meat Lovers", 
                Description = "For carnivores - pepperoni, sausage, bacon, and ham",
                Price = 18.99m,
                IsGlutenFree = false,
                Size = PizzaSize.Large,
                Toppings = new List<string> { "Tomato Sauce", "Mozzarella", "Pepperoni", "Sausage", "Bacon", "Ham" },
                ImageUrl = "https://example.com/images/meatlover.jpg"
            },
            new Pizza 
            { 
                Id = 5, 
                Name = "Hawaiian Paradise", 
                Description = "Ham and pineapple on a crispy crust",
                Price = 13.99m,
                IsGlutenFree = false,
                Size = PizzaSize.Small,
                Toppings = new List<string> { "Tomato Sauce", "Mozzarella", "Ham", "Pineapple" },
                ImageUrl = "https://example.com/images/hawaiian.jpg"
            },
            new Pizza 
            { 
                Id = 6, 
                Name = "BBQ Chicken", 
                Description = "Grilled chicken with tangy BBQ sauce and red onions",
                Price = 16.99m,
                IsGlutenFree = false,
                Size = PizzaSize.Large,
                Toppings = new List<string> { "BBQ Sauce", "Mozzarella", "Chicken", "Red Onions", "Cilantro" },
                ImageUrl = "https://example.com/images/bbq-chicken.jpg"
            }
        };
    }

    public static List<Pizza> GetAll() => Pizzas;

    public static Pizza? Get(int id) => Pizzas.FirstOrDefault(p => p.Id == id);

    public static void Add(Pizza pizza)
    {
        pizza.Id = nextId++;
        Pizzas.Add(pizza);
    }

    public static void Delete(int id)
    {
        var pizza = Get(id);
        if(pizza is null)
            return;

        Pizzas.Remove(pizza);
    }

    public static void Update(Pizza pizza)
    {
        var index = Pizzas.FindIndex(p => p.Id == pizza.Id);
        if(index == -1)
            return;

        Pizzas[index] = pizza;
    }
}