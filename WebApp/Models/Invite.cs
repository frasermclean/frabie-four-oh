using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class Invite
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public InviteStatus InviteStatus { get; set; }
    }

    public enum InviteStatus
    {
        WaitingForResponse,
        Attending,
        NotAttending,
    }
}
