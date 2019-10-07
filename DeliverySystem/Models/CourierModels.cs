using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query.Internal;


namespace DeliverySystem.Models
{
    public class Courier
    {
        [Key] public int Id { get; set; }

        [Required] public string FullName { get; set; }
        
        public  bool IsWorkingNow { get; set; }

        [Required] public int Capacity { get; set; }

        [Required] public string TelegramUsrName { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
    }

    public class ActiveCourierDeliveries
    {
        public int CourierId { get; set; }

        [ForeignKey("CourierId")] public Courier Courier { get; set; }

        [Key] public int Id { get; set; }
        
        public int DeliveryOrderId { get; set; }

        [ForeignKey("DeliveryOrderId")] public DeliveryOrder DeliveryOrder { get; set; }
        
    }
}