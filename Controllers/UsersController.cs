using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies_API.Data.Services;
using Movies_API.Data.ViewModels;

namespace Movies_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private UserService userService;

        public UsersController(UserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("register")]
        public IActionResult RegisterUser([FromBody] UserRegisterVM request)
        {
            try
            {
                var NewUser = userService.RegisterUser(request);
                return Created(nameof(NewUser), NewUser);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public IActionResult LoginUser([FromBody] UserLoginVM request)
        {
            try
            {
                var user = userService.LoginUser(request);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("favorites"), Authorize]
        public IActionResult GetFavorites()
        {
            try
            {
                var favorites = userService.GetFavorites();
                return Ok(favorites);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete, Authorize]
        public IActionResult DeleteAccount([FromBody] DeleteAccountVM request)
        {
            try
            {
                var response = userService.DeleteAccount(request);
                return Ok(new {message = response});
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPatch("password"), Authorize]
        public IActionResult ChangePassword([FromBody] ChangePasswordVM request)
        {
            try
            {
                var response = userService.ChangePassword(request);
                return Ok(new { message = response });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        
        [HttpPatch("name"), Authorize]
        public IActionResult ChangeName([FromBody] ChangeNameVM request)
        {
            try
            {
                var response = userService.ChangeName(request);
                return Ok(new { message = response });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("verify"), Authorize]
        public IActionResult VerifyToken()
        {
            var user = userService.VerifyToken();
            return Ok(new {user});
        }
    }
}
