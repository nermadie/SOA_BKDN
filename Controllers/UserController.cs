using DatingApp.API.Databases.Entities;
using DatingApp.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            // Auto Ok() response
            return _userService.GetUsers();
        }

        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            var user = _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound("User not found");
            }
            // Auto Ok() response
            return Ok(user);
        }

        [HttpPost]
        public ActionResult Post([FromBody] User user)
        {
            _userService.CreateUser(user);
            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] User user)
        {
            var currentUser = _userService.GetUserById(id);
            if (currentUser == null)
            {
                return NotFound("User not found");
            }
            currentUser.Username = user.Username;
            currentUser.Email = user.Email;
            _userService.UpdateUser(currentUser);
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var currentUser = _userService.GetUserById(id);
            if (currentUser == null)
            {
                return NotFound("User not found");
            }
            _userService.DeleteUser(currentUser);
            return Ok();
        }
    }
}