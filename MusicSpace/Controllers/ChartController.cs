using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicSpace.Domain;
using MusicSpace.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicSpace.Controllers
{
    public class ChartController : Controller
    {
        private readonly DataManager dataManager;
        public ChartController(DataManager dataManager)
        {
            this.dataManager = dataManager;
        }

        public async Task<IActionResult> Index()
        {
            var weeklyCharts = await dataManager.WeeklyCharts.GetWeeklyCharts().ToListAsync();
            foreach(var wc in weeklyCharts)
            {
                wc.ChartSongs = await dataManager.ChartSongs.GetChartSongsByChartId(wc.Id).ToListAsync();
                foreach(var cs in wc.ChartSongs)
                {
                    cs.Song = await dataManager.Songs.GetSongById(cs.SongId);
                    cs.Song.SongPerformers = await dataManager.SongPerformers.GetSongPerformersBySongId(cs.SongId).ToListAsync();
                    foreach(var p in cs.Song.SongPerformers)
                    {
                        p.Performer = await dataManager.Performers.GetPerformerById(p.PerformerId);
                    }
                }
            }
            return View(weeklyCharts);
        }
    }
}
