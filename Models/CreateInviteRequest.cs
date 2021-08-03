using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class CreateInviteRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
