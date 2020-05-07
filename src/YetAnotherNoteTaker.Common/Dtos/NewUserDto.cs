using System.ComponentModel.DataAnnotations;

namespace YetAnotherNoteTaker.Common.Dtos
{
    public class NewUserDto
    {
        [Required]
        [MinLength(5)]
        [MaxLength(255)]
        [EmailAddress]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        [MaxLength(30)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
