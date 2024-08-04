using LettercubedApi.utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;

namespace LettercubedApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        public TestController(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [Authorize(Roles = RolesConsts.ADMIN)]
        [HttpGet("adminPro")]
        public async Task<IActionResult> AdmiProtected()
        {


           

            return Ok("adminProtected");
        }
    }

}