using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PasswdApi.Exceptions;
using PasswdApi.Services;

namespace PasswdApi.Controllers
{
    [Route("api/users")]
    [EnableCors("AllowAllOrigins")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly IGroupService _groupService;

        public UsersController(IUserService userService, IGroupService groupService)
        {
            _userService = userService;
            _groupService = groupService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var users = _userService.GetUsers();
                if (users != null)
                {
                    return Ok(string.Join(", ", users.Select(x => x.ToString())));
                }
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }

            return BadRequest(new HttpResponseMessage(HttpStatusCode.BadRequest).ReasonPhrase);
        }

        [HttpGet("query")]
        public IActionResult Get(string name, int? uid, int? gid, string comment, string home, string shell)
        {
            try
            {
                var users = _userService.GetUsers(name, uid, gid, comment, home, shell);
                if (users != null)
                    return Ok("[" + string.Join(", ", users.Select(x => x.ToString())) + "]");
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }

            Response.StatusCode = (int) HttpStatusCode.BadRequest;
            return BadRequest(new HttpResponseMessage(HttpStatusCode.BadRequest).ReasonPhrase);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var user = _userService.GetUserById(id);
                if (user != null)
                    return Ok(user.ToString());
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }

            return BadRequest(new HttpResponseMessage(HttpStatusCode.BadRequest).ReasonPhrase);
        }

        [HttpGet("{id}/groups")]
        public IActionResult GetGroups(int id)
        {
            try
            {
                var user = _userService.GetUserById(id);
                if (user != null)
                {
                    var members = new string[1];
                    members[0] = user.Name;

                    var groups = _groupService.GetGroups(null, null, members);
                    return Ok("[" + string.Join(", ", groups.Select(x => x.ToString())) + "]");
                }
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }

            return BadRequest(new HttpResponseMessage(HttpStatusCode.BadRequest).ReasonPhrase);
        }
    }
}
