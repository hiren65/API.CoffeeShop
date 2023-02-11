using System.Security.AccessControl;

namespace CoffeeShop.API.Models
{
    public class CoffeeOrder
    {
        public int OrderId { get; set; }
        public string Type { get; set; } = TypeOfCoffee.TypeCoffee.Espresso.ToString();
        public string? message { get; set; }

    }
}
