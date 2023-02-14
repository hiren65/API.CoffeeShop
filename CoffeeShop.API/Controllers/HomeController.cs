/*
 * Install Microsoft.EntityFrameworkCore.SqlServer
 */
using CoffeeShop.API.Interfaces;
using CoffeeShop.API.Models;
using CoffeeShop.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//https://discoverdot.net/projects/swashbuckle-aspnetcore

namespace CoffeeShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class HomeController : ControllerBase
    {
        public IMakeCoffee _iMakeCoffee;
        IConfiguration _configuration;
        public CoffeeContext _db;

        public  HomeController(IMakeCoffee iMakeCoffee,CoffeeContext db )
        {
            _iMakeCoffee = iMakeCoffee;
            _db = db;
        }
        [ProducesResponseType(400)]
        [HttpPost("/brew-coffee/{Select}")]
        public  ActionResult< CoffeeOrder>  Brew_coffee(TypeCoffee Select)
        {

            
           
            CoffeeOrder order = new CoffeeOrder();
            

            order = _iMakeCoffee.MakeMyCoffee(Select);
            var cc = order.prepared.Value.Date;
           
            var ccList = _db.CustOrders.Select(m => m.Repeat).ToList();
            var ccListId = _db.CustOrders.Select(m => m.OrderId).ToList();
            int last = 0;int lastId = 0;
            try
            {
                last = Convert.ToInt32(ccList.LastOrDefault());
                lastId = Convert.ToInt32(ccListId.LastOrDefault());
            }
            catch (Exception e)
            {

            }


            //If No Selection
            if (order.Type.ToString() == "Select")
            {
                order.message = "Coffee Type Not Selected";

                return NoContent();
            }
            //if 1 April Status 418
            var currntYear = DateTime.Now.Year;

            if (cc == Convert.ToDateTime($"{currntYear}-04-1").Date )
            {
                order.message = "418 I’m a teapot";
                
                //return new CoffeeOrder() { message = "myContent", OrderId = 415 };
               // SaveData sd = new SaveData(_configuration,_db);
                //var str = sd.SaveMyData(order);
                return StatusCode(StatusCodes.Status418ImATeapot, new { message = $"418 Service Unavailable {order}" });
                //return NotFound(order);

            }
            // If on 5th times Requested Status 503
            if (last >= 4)
            {
                SaveData sd = new SaveData(_configuration, _db);

               
                
                order.message = "503 Service Unavailable";
                
                order.prepared = DateTime.Now;
                order.Repeat = 0;
                var str = sd.SaveMyData(order);


                order.message = "";
                order.Type = null;
                order.prepared = null;
                order.Repeat = last;


                return   StatusCode(StatusCodes.Status503ServiceUnavailable, new { message = $"503 Service Unavailable {order}" });
               // return NotFound("Customer doesn't exist");
            }
            else
            {
                // Successfull Request 
                SaveData sd1 = new SaveData(_configuration, _db);
                var str1 = sd1.SaveMyData(order);
                order.Repeat = last + 1;
                order.OrderId = lastId + 1;
            }

           

            return Ok(order); 
        }


    }
}
