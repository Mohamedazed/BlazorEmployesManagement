using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AuthBlazer.Data
{
    public class ApplicationUser : IdentityUser
    {
        [Key]
        [Required]
        public override string Id { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public override string UserName { get; set; }

        [Required(ErrorMessage = "User name is required")]
        public override string Email { get; set; }

        public byte[] ProfileImage { get; set; }
    }
}
