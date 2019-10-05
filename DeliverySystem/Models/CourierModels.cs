using System.ComponentModel.DataAnnotations;


namespace DeliverySystem.Models
{
    public class Courier
    {
        [Key] public int Id { get; set; }

        [Required] public string FullName { get; set; }

        [Required] public int Capacity { get; set; }

        [Required] public string TelegramUsrName { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
    }
}