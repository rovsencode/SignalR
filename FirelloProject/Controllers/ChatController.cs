using FirelloProject.Hubs;
using FirelloProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace FirelloProject.Controllers
{
    public class ChatController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IHubContext<ChatHub> _hubContext;

        public ChatController(UserManager<AppUser> userManager, IHubContext<ChatHub> hubContext)
        {
            _userManager = userManager;
            _hubContext = hubContext;
        }
        public IActionResult Chat()
        {
            ViewBag.Users = _userManager.Users.ToList();
            return View();
        }

        public async Task<IActionResult> ShowAlert(string userId)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);
            _hubContext.Clients.Client(user.ConnectionId).SendAsync("ShowAlert", "salam");

            return RedirectToAction("chat");
        }
    }
}
