using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using MusicSpace.Areas.Admin.Models;
using MusicSpace.Domain;
using MusicSpace.Domain.Entities;
using MusicSpace.Service.FileService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace MusicSpace.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SongsController : Controller
    {
        private readonly DataManager dataManager;
        private readonly FileService fileService;
        public SongsController(DataManager dataManager, IWebHostEnvironment env)
        {
            this.dataManager = dataManager;
            fileService = new FileService(env);
        }
        public async Task<IActionResult> Index()
        {
            List<Song> viewData = await dataManager.Songs.GetSongs().ToListAsync();
            foreach (var item in viewData)
            {
                item.Album = dataManager.Albums.GetAlbumBySongId(item.Id);
                item.SongPerformers = await dataManager.SongPerformers.GetSongPerformersBySongId(item.Id).ToListAsync();
                foreach (var sPerformer in item.SongPerformers)
                {
                    sPerformer.Performer = await dataManager .Performers.GetPerformerById(sPerformer.PerformerId);
                }
            }
            return View(viewData);
        }

        public IActionResult Create()
        {
            SongCreateViewModel songCreateViewModel = new SongCreateViewModel()
            {
                Song = new Song() { Id = Guid.NewGuid().ToString() },
                Albums = dataManager.Albums.GetAlbums(),
                Performers = dataManager.Performers.GetPerformers()
            };
            return View(songCreateViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Title,CoverFile,SongFile,ReleaseDate")] Song song) 
        {
            if (ModelState.IsValid)
            {
                song.Id = Guid.NewGuid().ToString();
                song.AlbumId = Request.Form["Albums"].ToString();
                song.CoverPath = fileService.Upload(song.CoverFile, FileType.Cover);
                song.SongPath = fileService.Upload(song.SongFile, FileType.Song);
                dataManager.Songs.SaveSong(song);
                List<string> performersIDs = Request.Form["Performers"].ToString().Split(",").ToList();
                foreach (var performerId in performersIDs)
                {
                    dataManager.SongPerformers.SaveSongPerformer(
                        new SongPerformer() 
                        { SongId = song.Id, PerformerId = performerId });
                }
                return RedirectToAction(nameof(Index));
            }
            SongCreateViewModel songCreateViewModel = new SongCreateViewModel()
            {
                Song = new Song() { Id = Guid.NewGuid().ToString() },
                Albums = dataManager.Albums.GetAlbums(),
                Performers = dataManager.Performers.GetPerformers()
            };
            return View(songCreateViewModel);
        }
    }
}
