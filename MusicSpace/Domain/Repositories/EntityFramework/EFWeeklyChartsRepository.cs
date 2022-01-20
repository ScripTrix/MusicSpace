using Microsoft.EntityFrameworkCore;
using MusicSpace.Domain.Entities;
using MusicSpace.Domain.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicSpace.Domain.Repositories.EntityFramework
{
    public class EFWeeklyChartsRepository : IWeeklyChartsRepository
    {
        private readonly AppDbContext context;
        public EFWeeklyChartsRepository(AppDbContext context)
        {
            this.context = context;
        }

        public void DeleteWeeklyChart(string id)
        {
            context.WeeklyCharts.Remove(new WeeklyChart() { Id = id });
            context.SaveChanges();
        }

        public WeeklyChart GetWeeklyChartById(string id)
        {
            return context.WeeklyCharts.FirstOrDefault(e => e.Id == id);
        }

        public WeeklyChart GetWeeklyChartByReleaseDate(DateTime dateTime)
        {
            return context.WeeklyCharts.FirstOrDefault(e => e.ReleaseDate == dateTime);
        }

        public IQueryable<WeeklyChart> GetWeeklyCharts()
        {
            return context.WeeklyCharts.OrderBy(e => e.ReleaseDate);
        }

        public IQueryable<WeeklyChart> GetWeeklyChartsBySongId(string id)
        {
            return context.WeeklyCharts.Where(e => e.ChartSongs.Contains(new ChartSong() { SongId = id }));
        }

        public void SaveWeeklyChart(WeeklyChart entity)
        {
            if (!context.WeeklyCharts.Any(e => e.Id == entity.Id))
                context.Entry(entity).State = EntityState.Added;
            else
                context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
