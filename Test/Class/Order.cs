namespace Test.Class
{
    public class Order
    {
        public int Id { get; set; }
        public int? PizzaId { get; set; }
        public string? UserName { get; set; }
        public string? UserPhone { get; set; }
        public bool? IsCompleted { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
