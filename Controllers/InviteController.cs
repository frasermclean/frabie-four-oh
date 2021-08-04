using AutoMapper;
using FrabieFourOh.Models;
using FrabieFourOh.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FrabieFourOh.Controllers
{
    public class InviteController : WebApiController
    {
        private readonly IInviteRepository repository;
        private readonly IMapper mapper;
        private readonly IEmailService emailService;

        public InviteController(IInviteRepository repository, IMapper mapper, IEmailService emailService)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.emailService = emailService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InviteDto>>> GetInviteListAsync()
        {
            var list = await repository.GetInvitesAsync();
            return Ok(mapper.Map<IEnumerable<InviteDto>>(list));
        }

        [HttpPost]
        public async Task<ActionResult<InviteEntity>> CreateInviteAsync([FromBody] CreateInviteRequest request)
        {
            InviteEntity invite = await repository.CreateInviteAsync(request.Name, request.Email);
            bool success = await emailService.SendInvitationAsync(invite.Name, invite.Email, invite.VerificationCode);

            if (success)
                return Ok(mapper.Map<InviteDto>(invite));
            else
            {
                await repository.DeleteInviteAsync(invite.Id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error sending email.");
            }
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
