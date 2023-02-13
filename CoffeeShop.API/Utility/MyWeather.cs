using Newtonsoft.Json;

namespace CoffeeShop.API.Utility
{
    public class MyWeather
    {
        public async Task<Main> Getdata()
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://api.openweathermap.org");
                var response = await client.GetAsync($"/data/2.5/weather?lat=-33.8688&lon=151.2093&appid=21b9eb015ca9994622a38c846372b13d&units=metric");
                var stringResult = await response.Content.ReadAsStringAsync();
                var rawWeather = JsonConvert.DeserializeObject<OpenWeatherResponse>(stringResult);



                response.EnsureSuccessStatusCode();

                var body = await response.Content.ReadAsStringAsync();


                var jsoncontent = JsonConvert.DeserializeObject<OpenWeatherResponse>(body);
                var temp = jsoncontent.Main.Temp;
                Console.WriteLine("Forecast for date: " + temp);
                Main we = new Main();

                we.Temp = temp;

                return we;
            }



        }
    }
}
