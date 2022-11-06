using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AspIntroduction.Core.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(20)]
        public string? FirstName { get; set; }

        [MaxLength(20)]
        public string? LastName { get; set; }
    }
}
