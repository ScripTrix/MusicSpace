using MusicSpace.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicSpace.Domain.Repositories.Abstract
{
    public interface IWeeklyChartsRepository
    {
        IQueryable<WeeklyChart> GetWeeklyChartsBySongId(string id);
        IQueryable<WeeklyChart> GetWeeklyCharts();
        WeeklyChart GetWeeklyChartByReleaseDate(DateTime dateTime);
        WeeklyChart GetWeeklyChartById(string id);
        void SaveWeeklyChart(WeeklyChart entity);
        void DeleteWeeklyChart(string id);
    }
}
