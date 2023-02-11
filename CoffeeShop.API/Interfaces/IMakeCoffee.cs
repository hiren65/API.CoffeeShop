using CoffeeShop.API.Models;

namespace CoffeeShop.API.Interfaces
{
    public interface IMakeCoffee
    {
       // int Repeat { get; }
        public CoffeeOrder  MakeMyCoffee(TypeCoffee name);
        
    }
}
