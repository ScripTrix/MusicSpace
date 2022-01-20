using Microsoft.EntityFrameworkCore;
using MusicSpace.Domain.Entities;
using MusicSpace.Domain.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicSpace.Domain.Repositories.EntityFramework
{
    public class EFChartSongsRepository : IChartSongsRepository
    {
        private readonly AppDbContext context;
        public EFChartSongsRepository(AppDbContext context)
        {
            this.context = context;
        }

        public void DeleteChartSong(string id)
        {
            context.ChartSongs.Remove(new ChartSong() { ChartId = id });
            context.SaveChanges();
        }

        public IQueryable<ChartSong> GetChartSongs()
        {
            return context.ChartSongs;
        }

        public IQueryable<ChartSong> GetChartSongsByChartId(string id)
        {
            return context.ChartSongs.Where(e => e.ChartId == id);
        }

        public IQueryable<ChartSong> GetChartSongsByChartReleaseDate(DateTime dateTime)
        {
            return context.ChartSongs.Where(e => e.Chart.ReleaseDate == dateTime);
        }

        public IQueryable<ChartSong> GetChartSongsByChartSpot(ushort spot)
        {
            return context.ChartSongs.Where(e => e.Spot == spot);
        }

        public void SaveChartSong(ChartSong entity)
        {
            if (!context.ChartSongs.Any(e => e.ChartId == entity.ChartId && e.SongId == entity.SongId))
                context.Entry(entity).State = EntityState.Added;
            else
                context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
