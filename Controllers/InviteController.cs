using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using FrabieFourOh.Models;
using FrabieFourOh.Services;

namespace FrabieFourOh.Controllers
{
    public class InviteController : WebApiController
    {
        private readonly IInviteRepository repository;
        private readonly IMapper mapper;

        public InviteController(IInviteRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
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
            return Ok(mapper.Map<InviteDto>(invite));
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
