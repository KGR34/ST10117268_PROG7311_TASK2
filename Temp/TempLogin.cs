using System.ComponentModel.DataAnnotations;

namespace ST10117268_PROG7311_TASK2.Temp
{
    public class TempLogin
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
