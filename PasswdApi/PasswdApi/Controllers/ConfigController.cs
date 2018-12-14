using System.Net;
using Microsoft.AspNetCore.Mvc;
using PasswdApi.Models;
using PasswdApi.Services;

namespace PasswdApi.Controllers
{
    [Produces("application/json")]
    [Route("api/config")]
    public class ConfigController : Controller
    {
        private readonly IUserService _userService;
        private readonly IGroupService _groupService;

        public ConfigController(IUserService userService, IGroupService groupService)
        {
            _userService = userService;
            _groupService = groupService;
        }

        [HttpPost]
        public IActionResult Post([FromBody]Config config)
        {
            var userConfig = new Config(_userService, _groupService, config.Path);

            string passwdPath = userConfig.Path + config.PasswdFilePath;
            string groupsPath = userConfig.Path + config.GroupsFilePath;

            if (System.IO.File.Exists(passwdPath) && System.IO.File.Exists(groupsPath))
            {
                if (userConfig.ValidateAndSave())
                {
                    Response.StatusCode = (int)HttpStatusCode.Accepted;
                    return Ok("Configurations have been successfully set!");
                }

                return BadRequest("Error occurred while validating the contents of the file(s).");
            }

            return NotFound("File or directory not found.");
        }
    }
}