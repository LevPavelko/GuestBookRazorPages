using System.ComponentModel.DataAnnotations;

namespace GuestBookRazorPages.Models
{
    public class RegisterModel
    {
        [Required]
        public string? Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
