using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicSpace.Areas.Admin.Models;
using MusicSpace.Domain;
using MusicSpace.Domain.Entities;
using MusicSpace.Service.FileService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicSpace.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AlbumsController : Controller
    {
        private readonly DataManager dataManager;
        private readonly FileService fileService;
        public AlbumsController(DataManager dataManager, IWebHostEnvironment env)
        {
            this.dataManager = dataManager;
            fileService = new FileService(env);
        }
        public async Task<IActionResult> Index()
        {
            List<Album> viewData = await dataManager.Albums.GetAlbums().OrderBy(e => e.ReleaseDate).ToListAsync();
            foreach(var item in viewData)
            {
                item.AlbumPerformers = await dataManager.AlbumPerformers.GetAlbumPerformersByAlbumId(item.Id).ToListAsync();
                foreach(var aPerformer in item.AlbumPerformers)
                {
                    aPerformer.Performer = await dataManager.Performers.GetPerformerById(aPerformer.PerformerId);
                }
            }
            return View(viewData);
        }

        public IActionResult Create()
        {
            AlbumCreateViewModel songCreateViewModel = new AlbumCreateViewModel()
            {
                Album = new Album() { Id = Guid.NewGuid().ToString() },
                Songs = dataManager.Songs.GetSongs(),
                Performers = dataManager.Performers.GetPerformers()
            };
            return View(songCreateViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Title,CoverFile,ReleaseDate")] Album album)
        {
            if (ModelState.IsValid)
            {
                album.Id = Guid.NewGuid().ToString();                
                album.CoverPath = fileService.Upload(album.CoverFile, FileType.Cover);
                dataManager.Albums.SaveAlbum(album);
                List<string> performerIDs = Request.Form["Performers"].ToString().Split(",").ToList();
                foreach (var performerId in performerIDs)
                {
                    dataManager.AlbumPerformers.SaveAlbumPerformer(
                        new AlbumPerformer()
                        { AlbumId = album.Id, PerformerId = performerId });
                }
                return RedirectToAction(nameof(Index));
            }
            AlbumCreateViewModel albumCreateViewModel = new AlbumCreateViewModel()
            {
                Album = new Album() { Id = Guid.NewGuid().ToString() },
                Songs = dataManager.Songs.GetSongs(),
                Performers = dataManager.Performers.GetPerformers()
            };
            return View(albumCreateViewModel);
        }
    }
}
