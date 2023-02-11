﻿/*
 * Install Microsoft.EntityFrameworkCore.SqlServer
 */
using CoffeeShop.API.Interfaces;
using CoffeeShop.API.Models;
using CoffeeShop.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//https://discoverdot.net/projects/swashbuckle-aspnetcore

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
        [ProducesResponseType(400)]
        [HttpGet("/brew_coffee/{Select}")]
        public  ActionResult< CoffeeOrder>  Brew_coffee(TypeCoffee Select)
        {

            
           
            CoffeeOrder order = new CoffeeOrder();
            

            order = _iMakeCoffee.MakeMyCoffee(Select);
            var cc = order.prepared.Value.Date;
            if (order.Type.ToString() == "Select")
            {
                order.message = "Coffee Type Not Selected";

                return NoContent();
            }

            if (cc == Convert.ToDateTime( "2023-02-12").Date )
            {
                order.message = "418 I’m a teapot";
                order.Type = null;
                //return new CoffeeOrder() { message = "myContent", OrderId = 415 };
                return NotFound(order);

            }

            return Ok(order); 
        }


    }
}
