using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicSpace.Areas.User.Models;
using MusicSpace.Domain;
using MusicSpace.Domain.Entities;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MusicSpace.Areas.User.Controllers
{
    [Area("User")]
    public class HomeController : Controller
    {
        private readonly DataManager dataManager;
        public HomeController(DataManager dataManager)
        {
            this.dataManager = dataManager;
        }
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ApplicationUser user = dataManager.ApplicationUsers.GetApplicationUserById(userId);
            var userSongs = await dataManager.Songs.GetUserSongsByUserId(userId);
            var userAlbums = dataManager.Albums.GetUserAlbumsByUserId(userId);
            return View(new UserHomeViewModel() { CurrentUser = user, UserSongs = userSongs, UserAlbums = userAlbums });
        }

        public IActionResult Adding(string id, string returnUrl)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!dataManager.UserLibrarySongs.SongExists(userId, id))
            {
                UserLibrarySong userLibrarySong = new UserLibrarySong() { UserId = userId, SongId = id, AdditionDate = DateTime.Now };
                dataManager.UserLibrarySongs.SaveUserLibrarySong(userLibrarySong);
            }
            ViewBag.returnUrl = returnUrl;
            return Redirect(returnUrl);
        }
    }
}
