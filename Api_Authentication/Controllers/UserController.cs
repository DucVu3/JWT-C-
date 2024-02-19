using Api_Authentication.Payloads.Requests;
using Api_Authentication.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromForm]Request_Register request)
        {
            return Ok(await userService.Register(request));
        }
        [HttpPost("Login")]
        public IActionResult Login(Request_Login login)
        {
            return Ok(userService.Login(login));
        }
        [HttpGet("Get-All")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetAlls(int pageSize, int pageNumber)
        {
            return Ok(await userService.GetAlls(pageSize, pageNumber));
        }
        [HttpPost]
        [Route("/api/forgot-password")]
        public async Task<IActionResult> ForgotPassword(Request_ForgotPassword request)
        {
            return Ok(await userService.ForgotPassword(request));
        }
        [HttpPost]
        [Route("/api/create-new-password")]
        public async Task<IActionResult> CreateNewPassword(ConfirmCreateNewPassword request)
        {
            return Ok(await userService.CreateNewPassword(request));
        }
        [HttpPut]
        [Route("/api/change-password")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> ChangePassword(Request_ChangePassword request)
        {
            int id = Convert.ToInt32(HttpContext.User.FindFirst("Id").Value);
            return Ok(await userService.ChangePassword(id, request));

        }
        [HttpPost]
        [Route("/api/create-new-password")]
        public async Task<IActionResult> CreateNewPassword(ConfirmCreateNewPassword request)
        {
            return Ok(await userService.CreateNewPassword(request));
        }
    }
}
