using System.ComponentModel.DataAnnotations;

namespace ST10117268_PROG7311_TASK2.Temp
{
    public class TempRegister
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string Fullname { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
