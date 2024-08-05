using LettercubedApi.Data;
using LettercubedApi.Models;
using LettercubedApi.utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LettercubedApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles =RolesConsts.ADMIN)]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _context;
        public AdminController(UserManager<AppUser> userManager, AppDbContext context )
        {
            _userManager = userManager;
            _context = context;
        }



        [HttpPost("promote/{UserId:guid}")]
        public async Task<IActionResult> PromoteUser(Guid UserId) {
            var retUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == UserId.ToString());
            if (retUser == null) return BadRequest("No user with that id");


            if (await _userManager.IsInRoleAsync(retUser, RolesConsts.EDITOR)) return BadRequest("User is already in that role");
            await _userManager.AddToRoleAsync(retUser,RolesConsts.EDITOR);

            PromotionsLogs log = new()
            {
                Id = Guid.NewGuid(),
                AdminId = _userManager.GetUserId(User),
                UserId = retUser.Id,
                Promotion = RolesConsts.EDITOR
            };

            await _context.PromotionsLogs.AddAsync(log);
            await _context.SaveChangesAsync();
            return Ok($"user with Id {UserId.ToString()} promoted to editor");
        }

        [HttpPost("demote/{UserId:guid}")]
        public async Task<IActionResult> DemoteUser(Guid UserId)
        {
            var retUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == UserId.ToString());
            if (retUser == null) return BadRequest("No user with that id");

            if (!await _userManager.IsInRoleAsync(retUser, RolesConsts.EDITOR)) return BadRequest("User is not in the Editor role");
            await _userManager.RemoveFromRoleAsync(retUser, RolesConsts.EDITOR);

            PromotionsLogs log = new()
            {
                Id = Guid.NewGuid(),
                AdminId = _userManager.GetUserId(User),
                UserId = retUser.Id,
                Promotion = RolesConsts.USER
            };

            await _context.PromotionsLogs.AddAsync(log);
            await _context.SaveChangesAsync();
            return Ok($"user with Id {UserId.ToString()} is demoted");
        }
    }
}
