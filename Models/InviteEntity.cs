using System;

namespace WebApp.Models
{
    public class InviteEntity : Entity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public InviteStatus InviteStatus { get; set; }
        public string VerificationCode { get; set; } = Guid.NewGuid().ToString();
    }

    public enum InviteStatus
    {
        AwaitingResponse,
        Attending,
        NotAttending,
    }
}
