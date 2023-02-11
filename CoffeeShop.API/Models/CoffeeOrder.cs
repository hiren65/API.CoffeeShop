using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;
using System.Text.Json.Serialization;

namespace CoffeeShop.API.Models
{
    public class CoffeeOrder
    {
        public int OrderId { get; set; }

        [Required]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        
        public  TypeCoffee ? Type { get; set; }      // = TypeOfCoffee.TypeCoffee.Espresso.ToString();
        //public SizeEnum Size { get; set; }
        public string? message { get; set; }

       // public int? Repeat { get; set; } = 0;
        public DateTime? prepared { get; set; }

    }
}
