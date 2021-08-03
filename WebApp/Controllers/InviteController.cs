using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class InviteController : WebApiController
    {
        [HttpGet]
        public ActionResult<IEnumerable<Invite>> GetInviteList()
        {
            List<Invite> list = new()
            {
                new Invite()
                {
                    Name = "Fraser McLean",
                    Email = "contact@frasermclean.com",
                    InviteStatus = InviteStatus.Attending,
                }
            };

            return Ok(list);
        }

        [HttpPost]
        public ActionResult<Invite> CreateInvite([FromBody] Invite body)
        {
            Invite invite = new()
            {
                Name = body.Name,
                Email = body.Email,
            };

            return Ok(invite);
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteInvite(int id)
        {
            return NoContent();
        }
    }
}
