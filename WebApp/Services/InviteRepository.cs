using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Services
{
    public interface IInviteRepository
    {
        Task<IEnumerable<Invite>> GetInvitesAsync();
        Task<Invite> CreateInviteAsync(string name, string email);
        Task<bool> DeleteInviteAsync(int id);
    }

    public class InviteRepository : IInviteRepository
    {
        public Task<IEnumerable<Invite>> GetInvitesAsync()
        {
            IEnumerable<Invite> list = new List<Invite>()
            {
                new Invite()
                {
                    Id = 1,
                    Name = "Fraser McLean",
                    Email = "contact@frasermclean.com",
                    InviteStatus = InviteStatus.Attending,
                },
                new Invite()
                {
                    Id = 2,
                    Name = "Louise Young",
                    Email = "louiseyoung@frasermclean.com",
                    InviteStatus = InviteStatus.WaitingForResponse,
                }
            };

            return Task.FromResult(list);
        }

        public async Task<Invite> CreateInviteAsync(string name, string email)
        {
            List<Invite> list = new(await GetInvitesAsync());

            Invite invite = new()
            {
                Id = list.Count + 1,
                Name = name,
                Email = email,
                InviteStatus = InviteStatus.WaitingForResponse,
            };

            return invite;
        }

        public Task<bool> DeleteInviteAsync(int id)
        {
            return Task.FromResult(false);
        }
    }
}
