using Microsoft.AspNetCore.Mvc;
using Test.Class;
using Test.Data;
using Test.ViewModels;

public class AdminController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly string _imageFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");

    public AdminController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var model = new AdminViewModel
        {
            Pizzas = _context.Pizzas.ToList(),
            Orders = _context.Orders.ToList()
        };
        return View(model);
    }

    [HttpGet]
    public IActionResult AddPizza()
    {
        var model = new AdminViewModel
        {
            NewPizza = new Pizza()
        };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> AddPizza(AdminViewModel model, IFormFile? imageFile)
    {
        if (ModelState.IsValid)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                var fileName = Path.GetFileName(imageFile.FileName);
                var filePath = Path.Combine(_imageFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                model.NewPizza.ImagePath = $"/images/{fileName}";
            }
            else
            {
                model.NewPizza.ImagePath = "/images/default.jpg"; // Значення за замовчуванням
            }

            _context.Pizzas.Add(model.NewPizza);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        return View(model);
    }

    [HttpPost]
    public IActionResult DeletePizza(int id)
    {
        var pizza = _context.Pizzas.Find(id);
        if (pizza != null)
        {
            _context.Pizzas.Remove(pizza);
            _context.SaveChanges();
        }
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult CompleteOrder(int id)
    {
        var order = _context.Orders.Find(id);
        if (order != null)
        {
            order.IsCompleted = true;
            _context.Orders.Remove(order);
            _context.SaveChanges();
        }
        return RedirectToAction("Index");
    }
}
