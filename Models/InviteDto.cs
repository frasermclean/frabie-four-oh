namespace WebApp.Models
{
    public class InviteDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public InviteStatus InviteStatus { get; set; }
    }
}
