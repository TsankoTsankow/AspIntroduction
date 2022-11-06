using System.ComponentModel.DataAnnotations;

namespace AspIntroduction.Models
{
    public class AllUsersViewModel
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public string Role  { get; set; } = null!;

    }
}
