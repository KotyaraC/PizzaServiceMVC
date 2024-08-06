using Test.Class;

namespace Test.ViewModels
{
    public class AdminViewModel
    {
        public List<Pizza>? Pizzas { get; set; }
        public List<Order>? Orders { get; set; }
        public Pizza NewPizza { get; set; } = new Pizza();
    }
}

