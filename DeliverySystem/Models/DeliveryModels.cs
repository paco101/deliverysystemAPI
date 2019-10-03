using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace DeliverySystem.Models
{
    public class DeliveryOrder
    {
        [Key] public int Id { get; set; }

        [ForeignKey("StockId")] public Stock Stock { get; set; }
        
        public int StockId { get; set; }

        [Required] public string ClientFullName { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string ClientPhoneNumber { get; set; }

        [Required] public Point Destination { get; set; }

        [DataType(DataType.DateTime)] public DateTime DateTime { get; set; }

        [DataType(DataType.MultilineText)] public string AdditionComments { get; set; }
    }

    public class Stock
    {
        [Key] public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        public Point Position { get; set; }
    }
}