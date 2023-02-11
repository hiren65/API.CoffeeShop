using CoffeeShop.API.Interfaces;
using CoffeeShop.API.Models;
using CoffeeShop.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        public IMakeCoffee _iMakeCoffee;
        

        public  HomeController(IMakeCoffee iMakeCoffee )
        {
            _iMakeCoffee = iMakeCoffee;
        }

        [HttpGet("/brew_coffee/{Select}")]
        public CoffeeOrder Brew_coffee(TypeCoffee Select)
        {


            CoffeeOrder order = new CoffeeOrder();
            

            order = _iMakeCoffee.MakeMyCoffee(Select);



            return order; 
        }


    }
}
