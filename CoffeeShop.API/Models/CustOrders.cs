using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CoffeeShop.API.Models
{
    public class CustOrders
    {
        [Required]
        [Key ]

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }

        public string? TypeCoffee { get; set; }   
                                                    
        [MaxLength]
        public string? message { get; set; }
        public int? Repeat { get; set; } = 0;
        public DateTime? prepared { get; set; }
    }
}
