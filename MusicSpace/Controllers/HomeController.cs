using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicSpace.Domain;
using MusicSpace.Models;
using System.Threading.Tasks;

namespace MusicSpace.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataManager dataManager;
        public HomeController(DataManager dataManager)
        {
            this.dataManager = dataManager;
        }
        public async Task<IActionResult> Index()
        {
            var albums = await dataManager.Albums.GetRecentlyAddedAlbums().ToListAsync();
            foreach(var item in albums)
            {
                item.AlbumPerformers = await dataManager.AlbumPerformers.GetAlbumPerformersByAlbumId(item.Id).ToListAsync();
                foreach(var aPerformer in item.AlbumPerformers)
                {
                    aPerformer.Performer = await dataManager.Performers.GetPerformerById(aPerformer.PerformerId);
                }
            }

            var songs = await dataManager.Songs.GetRecentlyAddedSongs().ToListAsync();
            foreach(var item in songs)
            {
                item.SongPerformers = await dataManager.SongPerformers.GetSongPerformersBySongId(item.Id).ToListAsync();
                foreach(var sPerformer in item.SongPerformers)
                {
                    sPerformer.Performer = await dataManager.Performers.GetPerformerById(sPerformer.PerformerId);
                }
            }
            HomeViewModel homeViewModel = new HomeViewModel() { Albums = albums, Songs = songs };
            return View(homeViewModel);
        }
    }
}
