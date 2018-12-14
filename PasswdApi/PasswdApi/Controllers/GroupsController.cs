using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PasswdApi.Exceptions;
using PasswdApi.Services;

namespace PasswdApi.Controllers
{
    [Route("api/groups")]
    [EnableCors("AllowAllOrigins")]
    public class GroupsController : Controller
    {
        private readonly IGroupService _groupService;

        public GroupsController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var groups = _groupService.GetGroups();
                if (groups != null)
                {
                    return Ok("[" + string.Join(", ", groups.Select(x => x.ToString())) + "]");
                }
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }

            return BadRequest(new HttpResponseMessage(HttpStatusCode.BadRequest).ReasonPhrase);
        }

        [HttpGet("query")]
        public IActionResult Get(string name, int? gid, string[] member)
        {
            try
            {
                var groups = _groupService.GetGroups(name, gid, member);
                if (groups != null)
                    return Ok("[" + string.Join(", ", groups.Select(x => x.ToString())) + "]");
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }

            return BadRequest(new HttpResponseMessage(HttpStatusCode.BadRequest).ReasonPhrase);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var group = _groupService.GetGroupById(id);
                if (group != null)
                    return Ok(group.ToString());
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }

            return BadRequest(new HttpResponseMessage(HttpStatusCode.BadRequest).ReasonPhrase);
        }
    }
}
