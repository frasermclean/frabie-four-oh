namespace WebApp.Models
{
    public class Invite : Entity
    {
        public string Name { get; set; }
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
