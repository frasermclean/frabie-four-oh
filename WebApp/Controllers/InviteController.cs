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
        public async Task<ActionResult> DeleteInviteAsync(int id)
        {
            bool success = await repository.DeleteInviteAsync(id);
            return success ? 
                NoContent() : 
                NotFound($"Invite with ID: {id} was not found.");
        }
    }
}
