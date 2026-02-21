using System.ComponentModel.DataAnnotations;

namespace UserValidation.Models
{
    public class User
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string FirstName { get; set; } = "";

        [MaxLength(100)]
        public string LastName { get; set; } = "";

        [MaxLength(200)]
        public string Email { get; set; } = "";

        [MaxLength(50)]
        public string? PhoneNumber { get; set; }

        public int Age { get; set; }

        [MaxLength(20)]
        public string? Username { get; set; }

        [MaxLength(300)]
        public string? Website { get; set; }

        [MaxLength(100)]
        public string Password { get; set; } = "";
    }
}
