using CoffeeShop.API.Models;

namespace CoffeeShop.API.Interfaces
{
    public interface IMakeCoffee
    {
        public CoffeeOrder  MakeMyCoffee(TypeCoffee name);
        
    }
}
