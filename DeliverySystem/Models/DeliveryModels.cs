using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DeliverySystem.Models
{
    public class DeliveryOrder 
    {
        [Key] public int Id { get; set; }

        [DefaultValue(1),Range(1,3)]
        public int Status { get; set; } // 1- Wait for delivery, 2 - delivering, 3 - delivery finished

        [ForeignKey("StockId")] public Stock Stock { get; set; }

        public int StockId { get; set; }

        [Required] public string ClientFullName { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string ClientPhoneNumber { get; set; }

        [Required] public double Latitude { get; set; }
        [Required] public double Longitude { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DateTime { get; set; }

        [DataType(DataType.MultilineText)] public string AdditionComments { get; set; }
        
        public ActiveCourierDelivery ActiveCourierDelivery { get; set; }
    }

    public class Stock
    {
        [Key] public int Id { get; set; }

        [Required] public string Name { get; set; }
        
        [Required] public double Latitude { get; set; }
        [Required] public double Longitude { get; set; }
    }
    
    
}