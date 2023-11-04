using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public class SettingDTO
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Describtion { get; set; }
        [Required]
        public string FirstPhoneNumber { get; set; } 
        [Required]
        public string SecondPhoneNumber { get; set; } 
        [Required]
        public string ThirdPhoneNumber { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string Location { get; set; }
    }
}
