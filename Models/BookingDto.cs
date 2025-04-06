using System.ComponentModel.DataAnnotations;

namespace EventEase3.Models
{
    public class BookingDto
    {
        [Required]
        public string email { get; set; } = "";

        [Required]
        public string eventName { get; set; } = "";

        [Required]
        public string venue { get; set; } = "";

        [Required]
        public string eventDate { get; set; } = "";


    }
}
