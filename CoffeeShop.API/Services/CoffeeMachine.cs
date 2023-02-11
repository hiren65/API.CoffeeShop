using CoffeeShop.API.Interfaces;
using CoffeeShop.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.API.Services
{
    public class CoffeeMachine : IMakeCoffee
    {
        // Implementing Interface 
        // Create Constructor
        //private string _name; 
        public CoffeeMachine()
        {
           // _name = name;   
        }

 
        public CoffeeOrder MakeMyCoffee(TypeCoffee name)
        {
            CoffeeOrder CO = new CoffeeOrder();
            CO.OrderId = 1;
            CO.Type = name; // TypeOfCoffee.TypeCoffee.Espresso.ToString();
            CO.message = "Your piping hot coffee is ready";
            CO.prepared = DateTime.Now;

          
            
            //throw new NotImplementedException();
            return CO;
        }




    }
}
