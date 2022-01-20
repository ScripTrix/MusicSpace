using MusicSpace.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicSpace.Domain.Repositories.Abstract
{
    public interface IChartSongsRepository
    {
        IQueryable<ChartSong> GetChartSongsByChartReleaseDate(DateTime dateTime);
        IQueryable<ChartSong> GetChartSongsByChartSpot(ushort spot);
        IQueryable<ChartSong> GetChartSongsByChartId(string id);
        IQueryable<ChartSong> GetChartSongs();
        void SaveChartSong(ChartSong entity);
        void DeleteChartSong(string id);
    }
}
