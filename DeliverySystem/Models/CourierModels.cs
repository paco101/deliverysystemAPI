using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query.Internal;


namespace DeliverySystem.Models
{
    public class Courier
    {
        [Key] public int Id { get; set; }

        [Required] public int StockId { get; set; }

        [ForeignKey("StockId")] public Stock Stock { get; set; }

        [Required] public string FullName { get; set; }

        [DefaultValue(1), Range(1, 3)]
        public int Status { get; set; } // 0 - not working, 1 - receives packages, 2 - delivering

        [Required] public int Capacity { get; set; }

        [Required] public string TelegramUsrName { get; set; }

        public long TelegramChatId { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
    }

    public class ActiveCourierDelivery
    {
        public int CourierId { get; set; }

        [ForeignKey("CourierId")] public Courier Courier { get; set; }

        [Key] public int Id { get; set; }

        public int DeliveryOrderId { get; set; }

        [ForeignKey("DeliveryOrderId")] public DeliveryOrder DeliveryOrder { get; set; }
    }
}