using ContosoPizza.Models;
using ContosoPizza.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContosoPizza.Controllers;

[ApiController]
[Route("[controller]")]
public class PizzaController : ControllerBase
{
    private readonly PizzaDbContext _context;

    public PizzaController(PizzaDbContext context)
    {
        _context = context;
    }

    // GET all action
    [HttpGet]
    public async Task<ActionResult<List<Pizza>>> GetAll()
    {
        return await _context.Pizzas.ToListAsync();
    }

    // GET by Id action
    [HttpGet("{id}")]
    public async Task<ActionResult<Pizza>> Get(int id)
    {
        var pizza = await _context.Pizzas.FindAsync(id);

        if (pizza is null)
            return NotFound();

        return pizza;
    }

    // POST action
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Pizza pizza)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _context.Pizzas.Add(pizza);
        await _context.SaveChangesAsync();
        
        return CreatedAtAction(nameof(Get), new { id = pizza.Id }, pizza);
    }

    // PUT action
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Pizza pizza)
    {
        if (id != pizza.Id)
            return BadRequest("ID mismatch");

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var existingPizza = await _context.Pizzas.FindAsync(id);
        if (existingPizza is null)
            return NotFound();

        // Update properties
        existingPizza.Name = pizza.Name;
        existingPizza.Description = pizza.Description;
        existingPizza.Price = pizza.Price;
        existingPizza.IsGlutenFree = pizza.IsGlutenFree;
        existingPizza.Size = pizza.Size;
        existingPizza.Toppings = pizza.Toppings;
        existingPizza.ImageUrl = pizza.ImageUrl;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE action
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var pizza = await _context.Pizzas.FindAsync(id);

        if (pizza is null)
            return NotFound();

        _context.Pizzas.Remove(pizza);
        await _context.SaveChangesAsync();
        
        return NoContent();
    }
}