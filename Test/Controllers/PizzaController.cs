using Microsoft.AspNetCore.Mvc;
using Test.Class;
using Test.Data;
using Test.ViewModels;

namespace Test.Controllers
{
    public class PizzaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PizzaController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Privacy()
        {
            return View("Privacy");
        }
        public IActionResult Index()
        {
            var model = new UserViewModel
            {
                Pizzas = _context.Pizzas.ToList()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Order(int pizzaId, string userName, string userPhone)
        {
            var pizza = await _context.Pizzas.FindAsync(pizzaId);
            if (pizza != null)
            {
                var order = new Order
                {
                    PizzaId = pizzaId,
                    UserName = userName,
                    UserPhone = userPhone,
                    IsCompleted = false,
                    OrderDate = DateTime.Now
                };

                _context.Orders.Add(order);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
    }
}
