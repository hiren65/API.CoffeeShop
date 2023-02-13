/*
 * Install Microsoft.EntityFrameworkCore.SqlServer
 */
using CoffeeShop.API.Interfaces;
using CoffeeShop.API.Models;
using CoffeeShop.API.Services;
using CoffeeShop.API.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
        [HttpGet("/brew-coffee/{Select}")]
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
            // If More than 5 times Requested Status 503
            if (last >= 5)
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
                MyWeather mc = new MyWeather();
                var myTemp = mc.Getdata();
                float conTemp = (float)Convert.ToDecimal(myTemp.Result.Temp);
                //If Temperature more than 30 C Location Sydney
                if (conTemp > 30)
                {
                    order.message = "Your refreshing iced coffee is ready";
                }
                // Successfull Request 
                SaveData sd1 = new SaveData(_configuration, _db);
                order.Repeat = last+1;
                var str1 = sd1.SaveMyData(order);
                order.OrderId = lastId + 1;
            }

            

            return Ok(order); 
        }
        // Weather API
        [HttpGet("[action]")]
        public async Task<IActionResult> City()
        {
            using (var client = new HttpClient())
            {
                try
                {
                    //https://api.openweathermap.org/data/2.5/weather?lat=44.34&lon=10.99&appid=21b9eb015ca9994622a38c846372b13d
                    client.BaseAddress = new Uri("http://api.openweathermap.org");
                    var response = await client.GetAsync($"/data/2.5/weather?lat=-33.8688&lon=151.2093&appid=21b9eb015ca9994622a38c846372b13d&units=metric");
                    response.EnsureSuccessStatusCode();

                    var stringResult = await response.Content.ReadAsStringAsync();
                    var rawWeather = JsonConvert.DeserializeObject<OpenWeatherResponse>(stringResult);
                  
                    
                    return Ok(new
                    {
                        Temp = rawWeather.Main.Temp,
                        Summary = string.Join(",", rawWeather.Weather.Select(x => x.Main)),
                        City = rawWeather.Name
                    });
                }
                catch (HttpRequestException httpRequestException)
                {
                    return BadRequest($"Error getting weather from OpenWeather: {httpRequestException.Message}");
                }
            }
        }

        // get Temp


    }
}
