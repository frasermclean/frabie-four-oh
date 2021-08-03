using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Controllers
{
    public class InviteController : WebApiController
    {
        private readonly IInviteRepository repository;

        public InviteController(IInviteRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Invite>>> GetInviteListAsync()
        {
            var list = await repository.GetInvitesAsync();
            return Ok(list);
        }

        [HttpPost]
        public async Task<ActionResult<Invite>> CreateInviteAsync([FromBody] CreateInviteRequest request)
        {
            Invite invite = await repository.CreateInviteAsync(request.Name, request.Email);
            return Ok(invite);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteInviteAsync(string id)
        {
            await repository.DeleteInviteAsync(id);
            return NoContent();
        }
    }
}
